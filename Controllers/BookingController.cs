using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatVeXemPhim.Controllers;

public class BookingController : BaseController
{
    // Thời gian giữ ghế tạm thời trước khi tự động nhả lại cho người khác.
    private const int HoldMinutes = 5;

    public BookingController(ApplicationDbContext db) : base(db) { }

    // Bất kỳ ghế nào đang "Đang giữ" nhưng đã quá hạn giữ đều được trả về "Trống".
    // Gọi ở đầu mọi action đọc/ghi trạng thái ghế của 1 suất chiếu, để không cần
    // background job riêng vẫn đảm bảo dữ liệu luôn đúng khi có người thao tác.
    private async Task ExpireStaleHoldsAsync(int showtimeId)
    {
        var now = DateTime.Now;
        var stale = await Db.ShowtimeSeats
            .Where(ss => ss.ShowtimeId == showtimeId && ss.Status == "Đang giữ" && ss.HoldExpiredAt != null && ss.HoldExpiredAt < now)
            .ToListAsync();

        if (stale.Count == 0) return;

        foreach (var s in stale)
        {
            s.Status = "Trống";
            s.HeldBySessionId = null;
            s.HoldExpiredAt = null;
        }
        await Db.SaveChangesAsync();
    }

    // GET /dat-ve/{showtimeId}
    [Route("/dat-ve/{showtimeId:int}")]
    public async Task<IActionResult> SeatSelect(int showtimeId, string? error)
    {
        var showtime = await Db.Showtimes.Include(s => s.Room).Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.ShowtimeId == showtimeId);
        if (showtime is null) return NotFound();

        await ExpireStaleHoldsAsync(showtimeId);
        var mySessionId = EnsureBrowserSessionId();
        var now = DateTime.Now;

        var seats = await Db.ShowtimeSeats
            .Include(ss => ss.Seat)
            .Where(ss => ss.ShowtimeId == showtimeId)
            .OrderBy(ss => ss.Seat.RowLabel).ThenBy(ss => ss.Seat.ColumnNumber)
            .Select(ss => new SeatCell
            {
                ShowtimeSeatId = ss.ShowtimeSeatId,
                RowLabel = ss.Seat.RowLabel,
                ColumnNumber = ss.Seat.ColumnNumber,
                SeatType = ss.Seat.SeatType,
                Status = ss.Status,
                IsHeldByMe = ss.Status == "Đang giữ" && ss.HeldBySessionId == mySessionId,
                HoldSecondsLeft = ss.Status == "Đang giữ" && ss.HoldExpiredAt != null
                    ? (int?)Math.Max(0, EF.Functions.DateDiffSecond(now, ss.HoldExpiredAt.Value))
                    : null
            })
            .ToListAsync();

        var rows = new SortedDictionary<string, List<SeatCell>>();
        foreach (var s in seats)
        {
            if (!rows.TryGetValue(s.RowLabel, out var list))
            {
                list = new List<SeatCell>();
                rows[s.RowLabel] = list;
            }
            list.Add(s);
        }

        var priceByType = new Dictionary<string, decimal>
        {
            ["Thường"] = showtime.TicketPrice,
            ["VIP"] = showtime.TicketPrice + 30000,
            ["Đôi"] = showtime.TicketPrice * 2
        };

        var combos = await Db.Combos.Where(c => c.IsActive).ToListAsync();
        var customer = await GetCurrentCustomerAsync();

        var vm = new SeatSelectVM
        {
            Title = "Chọn ghế - " + showtime.Movie.Title,
            ShowtimeId = showtime.ShowtimeId,
            RoomName = showtime.Room.RoomName,
            StartTime = showtime.StartTime,
            MovieId = showtime.MovieId,
            MovieTitle = showtime.Movie.Title,
            Rows = rows,
            PriceByType = priceByType,
            Combos = combos,
            IsLoggedIn = customer != null,
            HoldMinutes = HoldMinutes,
            ErrorMessage = error == "seat_taken" ? "Một hoặc nhiều ghế bạn chọn vừa được đặt hoặc đang được người khác giữ. Vui lòng chọn lại." : null
        };

        return View(vm);
    }

    public class HoldRequest
    {
        public List<int> SeatIds { get; set; } = new();
    }

    // POST /dat-ve/{showtimeId}/giu-ghe — giữ tạm bộ ghế người dùng vừa chọn (AJAX).
    // Gửi kèm TOÀN BỘ danh sách ghế đang chọn mỗi lần gọi: ghế nào trước đó do
    // session này giữ mà không còn trong danh sách mới sẽ được nhả ra ngay.
    [HttpPost]
    [Route("/dat-ve/{showtimeId:int}/giu-ghe")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Hold(int showtimeId, [FromBody] HoldRequest req)
    {
        var showtime = await Db.Showtimes.FindAsync(showtimeId);
        if (showtime is null) return NotFound();

        await ExpireStaleHoldsAsync(showtimeId);
        var mySessionId = EnsureBrowserSessionId();
        var now = DateTime.Now;
        var expiresAt = now.AddMinutes(HoldMinutes);

        var myCurrentHolds = await Db.ShowtimeSeats
            .Where(ss => ss.ShowtimeId == showtimeId && ss.Status == "Đang giữ" && ss.HeldBySessionId == mySessionId)
            .ToListAsync();

        // Nhả các ghế mình đang giữ nhưng không còn trong danh sách chọn mới.
        foreach (var s in myCurrentHolds.Where(s => !req.SeatIds.Contains(s.ShowtimeSeatId)))
        {
            s.Status = "Trống";
            s.HeldBySessionId = null;
            s.HoldExpiredAt = null;
        }

        var conflicts = new List<int>();
        foreach (var seatId in req.SeatIds)
        {
            var row = await Db.ShowtimeSeats.FirstOrDefaultAsync(ss => ss.ShowtimeSeatId == seatId && ss.ShowtimeId == showtimeId);
            if (row is null) { conflicts.Add(seatId); continue; }

            var isMine = row.Status == "Đang giữ" && row.HeldBySessionId == mySessionId;
            var isFree = row.Status == "Trống";

            if (!isMine && !isFree)
            {
                conflicts.Add(seatId); // "Đã đặt" hoặc đang được người khác giữ
                continue;
            }

            row.Status = "Đang giữ";
            row.HeldBySessionId = mySessionId;
            row.HoldExpiredAt = expiresAt;
        }

        await Db.SaveChangesAsync();

        return Json(new
        {
            ok = conflicts.Count == 0,
            conflicts,
            holdExpiresAt = expiresAt
        });
    }

    // POST /dat-ve/{showtimeId}/bo-giu — nhả toàn bộ ghế mình đang giữ cho suất chiếu này
    // (gọi khi đổi suất chiếu, rời trang, hoặc trước khi submit đặt vé để tránh giữ thừa).
    [HttpPost]
    [Route("/dat-ve/{showtimeId:int}/bo-giu")]
    public async Task<IActionResult> Release(int showtimeId)
    {
        var mySessionId = EnsureBrowserSessionId();
        var myHolds = await Db.ShowtimeSeats
            .Where(ss => ss.ShowtimeId == showtimeId && ss.Status == "Đang giữ" && ss.HeldBySessionId == mySessionId)
            .ToListAsync();

        foreach (var s in myHolds)
        {
            s.Status = "Trống";
            s.HeldBySessionId = null;
            s.HoldExpiredAt = null;
        }
        await Db.SaveChangesAsync();
        return Ok();
    }

    // GET /dat-ve/{showtimeId}/trang-thai-ghe — trạng thái mới nhất của toàn bộ ghế,
    // để trang chọn ghế tự làm mới định kỳ và thấy ngay khi người khác đặt/giữ ghế.
    [HttpGet]
    [Route("/dat-ve/{showtimeId:int}/trang-thai-ghe")]
    public async Task<IActionResult> SeatStatus(int showtimeId)
    {
        await ExpireStaleHoldsAsync(showtimeId);
        var mySessionId = EnsureBrowserSessionId();
        var now = DateTime.Now;

        var seats = await Db.ShowtimeSeats
            .Where(ss => ss.ShowtimeId == showtimeId)
            .Select(ss => new
            {
                id = ss.ShowtimeSeatId,
                status = ss.Status,
                isMine = ss.Status == "Đang giữ" && ss.HeldBySessionId == mySessionId,
                secondsLeft = ss.Status == "Đang giữ" && ss.HoldExpiredAt != null
                    ? (int?)Math.Max(0, EF.Functions.DateDiffSecond(now, ss.HoldExpiredAt.Value))
                    : null
            })
            .ToListAsync();

        return Json(seats);
    }

    // POST /dat-ve/{showtimeId}/xac-nhan
    [HttpPost]
    [Route("/dat-ve/{showtimeId:int}/xac-nhan")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm(int showtimeId, [FromForm] List<int> seats, [FromForm] Dictionary<string, string>? combos, [FromForm] string? voucherCode)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect($"/dang-nhap?next=/dat-ve/{showtimeId}");

        var showtime = await Db.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == showtimeId);
        if (showtime is null) return NotFound();

        if (seats is null || seats.Count == 0)
            return Redirect($"/dat-ve/{showtimeId}");

        await ExpireStaleHoldsAsync(showtimeId);
        var mySessionId = EnsureBrowserSessionId();

        await using var tx = await Db.Database.BeginTransactionAsync();
        try
        {
            var seatRows = new List<ShowtimeSeat>();
            foreach (var seatId in seats)
            {
                var row = await Db.ShowtimeSeats.Include(ss => ss.Seat)
                    .FirstOrDefaultAsync(ss => ss.ShowtimeSeatId == seatId && ss.ShowtimeId == showtimeId);

                // Ghế hợp lệ để đặt: đang "Trống", HOẶC đang "Đang giữ" bởi CHÍNH session này.
                // Ghế "Đã đặt", hoặc "Đang giữ" bởi người khác, đều không cho đặt.
                var isMine = row != null && row.Status == "Đang giữ" && row.HeldBySessionId == mySessionId;
                var isFree = row != null && row.Status == "Trống";
                if (row is null || (!isMine && !isFree))
                    throw new InvalidOperationException("SEAT_UNAVAILABLE");

                seatRows.Add(row!);
            }

            var priceByType = new Dictionary<string, decimal>
            {
                ["Thường"] = showtime.TicketPrice,
                ["VIP"] = showtime.TicketPrice + 30000,
                ["Đôi"] = showtime.TicketPrice * 2
            };

            decimal ticketTotal = seatRows.Sum(s => priceByType[s.Seat.SeatType]);

            decimal comboTotal = 0;
            var comboLines = new List<(int ComboId, int Qty, decimal Price)>();
            if (combos != null)
            {
                foreach (var (comboIdStr, qtyStr) in combos)
                {
                    if (!int.TryParse(comboIdStr, out var comboId)) continue;
                    if (!int.TryParse(qtyStr, out var qty) || qty <= 0) continue;
                    var combo = await Db.Combos.FirstOrDefaultAsync(c => c.ComboId == comboId);
                    if (combo is null) continue;
                    comboTotal += combo.Price * qty;
                    comboLines.Add((combo.ComboId, qty, combo.Price));
                }
            }

            decimal total = ticketTotal + comboTotal;
            int? voucherId = null;
            var code = (voucherCode ?? string.Empty).Trim().ToUpperInvariant();
            if (!string.IsNullOrEmpty(code))
            {
                var today = DateTime.Now.Date;
                var voucher = await Db.Vouchers.FirstOrDefaultAsync(v =>
                    v.Code == code && v.IsActive &&
                    today >= v.StartDate.Date && today <= v.EndDate.Date &&
                    v.UsedCount < v.UsageLimit);

                if (voucher != null && total >= voucher.MinOrderAmount)
                {
                    voucherId = voucher.VoucherId;
                    total -= voucher.DiscountType == "Phần trăm"
                        ? total * (voucher.DiscountValue / 100)
                        : voucher.DiscountValue;
                    total = Math.Max(0, total);
                    voucher.UsedCount += 1;
                }
            }

            var ticket = new Ticket
            {
                CustomerId = customer.CustomerId,
                ShowtimeId = showtimeId,
                VoucherId = voucherId,
                TotalAmount = total,
                Status = "Đã thanh toán",
                BookingDate = DateTime.Now,
                ConfirmedAt = DateTime.Now
            };
            Db.Tickets.Add(ticket);
            await Db.SaveChangesAsync(); // need TicketId

            foreach (var s in seatRows)
            {
                Db.TicketDetails.Add(new TicketDetail
                {
                    TicketId = ticket.TicketId,
                    ShowtimeSeatId = s.ShowtimeSeatId,
                    Price = priceByType[s.Seat.SeatType]
                });
                s.Status = "Đã đặt";
                s.HeldBySessionId = null;
                s.HoldExpiredAt = null;
            }

            foreach (var c in comboLines)
            {
                Db.TicketCombos.Add(new TicketCombo
                {
                    TicketId = ticket.TicketId,
                    ComboId = c.ComboId,
                    Quantity = c.Qty,
                    Price = c.Price
                });
            }

            // LƯU Ý: đây là thanh toán MÔ PHỎNG cho mục đích học tập/báo cáo — không có cổng
            // thanh toán thật (VNPay/Momo/ZaloPay) nào được gọi, không có giao dịch tiền thật
            // nào xảy ra. Vé được đánh dấu "Đã thanh toán" ngay lập tức, mã giao dịch bắt đầu
            // bằng tiền tố "DEMO" để phân biệt rõ với giao dịch thật khi đọc dữ liệu.
            Db.Payments.Add(new Payment
            {
                TicketId = ticket.TicketId,
                Amount = total,
                PaymentMethod = "Thanh toán online (mô phỏng)",
                PaymentStatus = "Thành công",
                TransactionCode = "DEMO" + ticket.TicketId + DateTime.Now.Ticks.ToString()[^6..],
                PaymentDate = DateTime.Now
            });

            customer.LoyaltyPoint += (int)Math.Floor(total / 10000);
            MembershipRankHelper.RecalculateRank(customer);

            await Db.SaveChangesAsync();
            await tx.CommitAsync();

            return Redirect($"/ve/{ticket.TicketId}");
        }
        catch
        {
            await tx.RollbackAsync();
            return Redirect($"/dat-ve/{showtimeId}?error=seat_taken");
        }
    }
}

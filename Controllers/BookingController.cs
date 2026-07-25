using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatVeXemPhim.Controllers;

public class BookingController : BaseController
{
    public BookingController(ApplicationDbContext db) : base(db) { }

    // GET /dat-ve/{showtimeId}
    [Route("/dat-ve/{showtimeId:int}")]
    public async Task<IActionResult> SeatSelect(int showtimeId, string? error)
    {
        var showtime = await Db.Showtimes.Include(s => s.Room).Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.ShowtimeId == showtimeId);
        if (showtime is null) return NotFound();

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
                Status = ss.Status
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
            ErrorMessage = error == "seat_taken" ? "Một hoặc nhiều ghế bạn chọn vừa được đặt bởi người khác. Vui lòng chọn lại." : null
        };

        return View(vm);
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

        await using var tx = await Db.Database.BeginTransactionAsync();
        try
        {
            var seatRows = new List<ShowtimeSeat>();
            foreach (var seatId in seats)
            {
                var row = await Db.ShowtimeSeats.Include(ss => ss.Seat)
                    .FirstOrDefaultAsync(ss => ss.ShowtimeSeatId == seatId && ss.ShowtimeId == showtimeId);
                if (row is null || row.Status == "Đã đặt")
                    throw new InvalidOperationException("SEAT_UNAVAILABLE");
                seatRows.Add(row);
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
            var code = (voucherCode ?? string.Empty).Trim();
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

            Db.Payments.Add(new Payment
            {
                TicketId = ticket.TicketId,
                Amount = total,
                PaymentMethod = "Thanh toán online",
                PaymentStatus = "Thành công",
                TransactionCode = "DEMO" + ticket.TicketId + DateTime.Now.Ticks.ToString()[^6..],
                PaymentDate = DateTime.Now
            });

            customer.LoyaltyPoint += (int)Math.Floor(total / 10000);

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

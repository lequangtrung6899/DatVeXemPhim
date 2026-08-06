using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class TicketController : BaseController
{
    // Chỉ cho phép khách hàng tự hủy vé nếu suất chiếu còn cách hiện tại tối thiểu
    // ngần này giờ — tránh trường hợp hủy sát giờ chiếu gây thất thoát cho rạp.
    private const int CancelCutoffHours = 2;

    public TicketController(ApplicationDbContext db) : base(db) { }

    // GET /ve/{ticketId}
    [Route("/ve/{ticketId:int}")]
    public async Task<IActionResult> Show(int ticketId, string? cancelled)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap");

        var ticket = await Db.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId && t.CustomerId == customer.CustomerId);
        if (ticket is null) return NotFound();

        var showtime = await Db.Showtimes.Include(s => s.Movie).Include(s => s.Room)
            .FirstAsync(s => s.ShowtimeId == ticket.ShowtimeId);

        var seats = await Db.TicketDetails
            .Include(td => td.ShowtimeSeat).ThenInclude(ss => ss.Seat)
            .Where(td => td.TicketId == ticket.TicketId)
            .Select(td => new TicketSeatLine
            {
                RowLabel = td.ShowtimeSeat.Seat.RowLabel,
                ColumnNumber = td.ShowtimeSeat.Seat.ColumnNumber,
                SeatType = td.ShowtimeSeat.Seat.SeatType,
                Price = td.Price
            })
            .ToListAsync();

        var combos = await Db.TicketCombos
            .Include(tc => tc.Combo)
            .Where(tc => tc.TicketId == ticket.TicketId)
            .Select(tc => new TicketComboLine
            {
                ComboName = tc.Combo.ComboName,
                Quantity = tc.Quantity,
                Price = tc.Price
            })
            .ToListAsync();

        var canCancel = ticket.Status == "Đã thanh toán" && showtime.StartTime > DateTime.Now.AddHours(CancelCutoffHours);

        var vm = new TicketVM
        {
            Title = "Vé #" + ticket.TicketId,
            TicketId = ticket.TicketId,
            Status = ticket.Status,
            TotalAmount = ticket.TotalAmount,
            RefundAmount = ticket.RefundAmount,
            MovieTitle = showtime.Movie.Title,
            RoomName = showtime.Room.RoomName,
            StartTime = showtime.StartTime,
            Seats = seats,
            Combos = combos,
            CanCancel = canCancel,
            CancelCutoffHours = CancelCutoffHours,
            JustCancelled = cancelled == "ok"
        };

        return View(vm);
    }

    // POST /ve/{ticketId}/huy — khách hàng tự hủy vé của chính mình.
    [HttpPost]
    [Route("/ve/{ticketId:int}/huy")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int ticketId)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap");

        var ticket = await Db.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId && t.CustomerId == customer.CustomerId);
        if (ticket is null) return NotFound();

        var showtime = await Db.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == ticket.ShowtimeId);
        if (showtime is null) return NotFound();

        // Kiểm tra lại điều kiện hủy ở server (không chỉ tin vào việc ẩn nút trên giao diện).
        if (ticket.Status != "Đã thanh toán" || showtime.StartTime <= DateTime.Now.AddHours(CancelCutoffHours))
        {
            TempData["Error"] = $"Không thể hủy vé này. Vé chỉ được hủy khi chưa quá {CancelCutoffHours} giờ trước giờ chiếu và chưa từng bị hủy.";
            return Redirect($"/ve/{ticketId}");
        }

        await using var tx = await Db.Database.BeginTransactionAsync();
        try
        {
            ticket.Status = "Đã hủy";
            ticket.CancelledAt = DateTime.Now;
            ticket.RefundAmount = ticket.TotalAmount; // hủy đúng hạn -> hoàn 100%, mô phỏng đơn giản cho báo cáo

            // Giải phóng ghế để người khác có thể đặt lại.
            var seatIds = await Db.TicketDetails.Where(td => td.TicketId == ticketId).Select(td => td.ShowtimeSeatId).ToListAsync();
            var showtimeSeats = await Db.ShowtimeSeats.Where(ss => seatIds.Contains(ss.ShowtimeSeatId)).ToListAsync();
            foreach (var ss in showtimeSeats)
            {
                ss.Status = "Trống";
                ss.HeldBySessionId = null;
                ss.HoldExpiredAt = null;
            }

            // Đánh dấu hoàn tiền trên lần thanh toán gần nhất (mô phỏng — không gọi cổng thanh toán thật).
            var payment = await Db.Payments.Where(p => p.TicketId == ticketId)
                .OrderByDescending(p => p.PaymentDate).FirstOrDefaultAsync();
            if (payment != null) payment.PaymentStatus = "Đã hoàn tiền";

            // Trả lại voucher đã dùng (nếu có) để khách có thể dùng lại mã cho lần đặt khác.
            if (ticket.VoucherId != null)
            {
                var voucher = await Db.Vouchers.FindAsync(ticket.VoucherId.Value);
                if (voucher != null && voucher.UsedCount > 0) voucher.UsedCount -= 1;
            }

            // Thu hồi điểm tích lũy đã cộng khi đặt vé này, tính lại hạng thành viên tương ứng.
            var earnedPoints = (int)Math.Floor(ticket.TotalAmount / 10000);
            customer.LoyaltyPoint = Math.Max(0, customer.LoyaltyPoint - earnedPoints);
            MembershipRankHelper.RecalculateRank(customer);

            await Db.SaveChangesAsync();
            await tx.CommitAsync();

            return Redirect($"/ve/{ticketId}?cancelled=ok");
        }
        catch
        {
            await tx.RollbackAsync();
            TempData["Error"] = "Có lỗi xảy ra khi hủy vé, vui lòng thử lại.";
            return Redirect($"/ve/{ticketId}");
        }
    }
}

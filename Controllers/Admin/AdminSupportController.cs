using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Kiểm tra thông tin vé" (2.6.26), "Kiểm tra thanh toán" (2.6.27) và
// "Hỗ trợ khách hàng" (2.6.28). Cả ba đều xoay quanh việc nhân viên tra cứu một vé
// (theo mã vé hoặc mã giao dịch thanh toán) rồi xem đầy đủ thông tin để xử lý yêu cầu
// của khách hàng, nên được gộp vào một màn hình tra cứu duy nhất.
public class AdminSupportController : AdminBaseController
{
    public AdminSupportController(ApplicationDbContext db) : base(db) { }

    // GET /quan-tri/ho-tro?ticketId=123  hoặc  ?transactionCode=DEMO123...
    [HttpGet, Route("/quan-tri/ho-tro")]
    public async Task<IActionResult> Index(int? ticketId, string? transactionCode)
    {
        var vm = new AdminSupportVM { TicketIdInput = ticketId, TransactionCodeInput = transactionCode };

        int? resolvedTicketId = ticketId;

        if (resolvedTicketId is null && !string.IsNullOrWhiteSpace(transactionCode))
        {
            var payment = await Db.Payments.FirstOrDefaultAsync(p => p.TransactionCode == transactionCode.Trim());
            if (payment is null)
            {
                vm.NotFound = true;
                return View(vm);
            }
            resolvedTicketId = payment.TicketId;
        }

        if (resolvedTicketId is null)
        {
            return View(vm);
        }

        var ticket = await Db.Tickets
            .Include(t => t.Customer)
            .Include(t => t.Showtime).ThenInclude(s => s.Movie)
            .Include(t => t.Showtime).ThenInclude(s => s.Room)
            .Include(t => t.Voucher)
            .FirstOrDefaultAsync(t => t.TicketId == resolvedTicketId);

        if (ticket is null)
        {
            vm.NotFound = true;
            return View(vm);
        }

        var seats = await Db.TicketDetails
            .Include(td => td.ShowtimeSeat).ThenInclude(ss => ss.Seat)
            .Where(td => td.TicketId == ticket.TicketId)
            .Select(td => new AdminSupportSeatLine
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
            .Select(tc => new AdminSupportComboLine
            {
                ComboName = tc.Combo.ComboName,
                Quantity = tc.Quantity,
                Price = tc.Price
            })
            .ToListAsync();

        var payments = await Db.Payments.Where(p => p.TicketId == ticket.TicketId)
            .OrderByDescending(p => p.PaymentDate)
            .ToListAsync();

        vm.Ticket = ticket;
        vm.Seats = seats;
        vm.Combos = combos;
        vm.Payments = payments;
        return View(vm);
    }

    // Hỗ trợ khách hàng: nhân viên hủy vé giúp khách và đánh dấu hoàn tiền khi cần.
    [HttpPost, Route("/quan-tri/ho-tro/{ticketId:int}/huy-ve")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CancelTicket(int ticketId, decimal? refundAmount)
    {
        var ticket = await Db.Tickets.FindAsync(ticketId);
        if (ticket is null) return NotFound();

        if (ticket.Status == "Đã hủy")
        {
            TempData["Error"] = "Vé này đã được hủy trước đó.";
            return Redirect($"/quan-tri/ho-tro?ticketId={ticketId}");
        }

        ticket.Status = "Đã hủy";
        ticket.CancelledAt = DateTime.Now;
        ticket.RefundAmount = refundAmount ?? ticket.TotalAmount;

        // Free up the seats so they can be booked again.
        var seatIds = await Db.TicketDetails.Where(td => td.TicketId == ticketId).Select(td => td.ShowtimeSeatId).ToListAsync();
        var showtimeSeats = await Db.ShowtimeSeats.Where(ss => seatIds.Contains(ss.ShowtimeSeatId)).ToListAsync();
        foreach (var ss in showtimeSeats) ss.Status = "Trống";

        var payment = await Db.Payments.Where(p => p.TicketId == ticketId).OrderByDescending(p => p.PaymentDate).FirstOrDefaultAsync();
        if (payment != null) payment.PaymentStatus = "Đã hoàn tiền";

        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã hủy vé và giải phóng ghế cho khách hàng.";
        return Redirect($"/quan-tri/ho-tro?ticketId={ticketId}");
    }
}

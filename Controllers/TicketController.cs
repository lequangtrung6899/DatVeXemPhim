using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class TicketController : BaseController
{
    // Chỉ cho phép khách hàng tự yêu cầu hoàn tiền nếu suất chiếu còn cách hiện tại
    // tối thiểu ngần này giờ — tránh trường hợp hủy sát giờ chiếu gây thất thoát cho rạp.
    private const int CancelCutoffHours = 2;

    private readonly RefundService _refundService;

    public TicketController(ApplicationDbContext db, RefundService refundService) : base(db)
    {
        _refundService = refundService;
    }

    // GET /ve/{ticketId}
    [Route("/ve/{ticketId:int}")]
    public async Task<IActionResult> Show(int ticketId, string? refunded)
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

        var latestRefundRequest = await Db.RefundRequests
            .Where(r => r.TicketId == ticket.TicketId)
            .OrderByDescending(r => r.RequestedAt)
            .FirstOrDefaultAsync();

        var hasActiveRefundRequest = latestRefundRequest != null &&
            (latestRefundRequest.Status == "Chờ nhân viên duyệt" || latestRefundRequest.Status == "Chờ admin duyệt");

        var canCancel = ticket.Status == "Đã thanh toán"
            && showtime.StartTime > DateTime.Now.AddHours(CancelCutoffHours)
            && !hasActiveRefundRequest;

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
            JustRequestedRefund = refunded == "ok",
            RefundRequestStatus = latestRefundRequest?.Status,
            RefundRejectReason = latestRefundRequest?.Status == "Từ chối" ? latestRefundRequest.RejectReason : null
        };

        return View(vm);
    }

    // POST /ve/{ticketId}/huy — khách hàng tự GỬI YÊU CẦU hoàn tiền/hủy vé của chính mình.
    // Tiền chỉ thực sự được hoàn sau khi CẢ nhân viên VÀ admin xét duyệt (xem RefundService).
    [HttpPost]
    [Route("/ve/{ticketId:int}/huy")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int ticketId, string? reason)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap");

        var ticket = await Db.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId && t.CustomerId == customer.CustomerId);
        if (ticket is null) return NotFound();

        var showtime = await Db.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == ticket.ShowtimeId);
        if (showtime is null) return NotFound();

        // Kiểm tra lại điều kiện ở server (không chỉ tin vào việc ẩn nút trên giao diện).
        if (ticket.Status != "Đã thanh toán" || showtime.StartTime <= DateTime.Now.AddHours(CancelCutoffHours))
        {
            TempData["Error"] = $"Không thể yêu cầu hoàn tiền cho vé này. Vé chỉ được yêu cầu hoàn tiền khi chưa quá {CancelCutoffHours} giờ trước giờ chiếu và chưa từng bị hủy.";
            return Redirect($"/ve/{ticketId}");
        }

        var (ok, message) = await _refundService.CreateRequestAsync(ticket, customer.CustomerId, reason);
        if (!ok)
        {
            TempData["Error"] = message;
            return Redirect($"/ve/{ticketId}");
        }

        TempData["Success"] = message;
        return Redirect($"/ve/{ticketId}?refunded=ok");
    }
}

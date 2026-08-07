using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Kiểm tra thông tin vé" (2.6.26), "Kiểm tra thanh toán" (2.6.27) và
// "Hỗ trợ khách hàng" (2.6.28). Cả ba đều xoay quanh việc nhân viên tra cứu một vé
// (theo mã vé hoặc mã giao dịch thanh toán) rồi xem đầy đủ thông tin để xử lý yêu cầu
// của khách hàng, nên được gộp vào một màn hình tra cứu duy nhất.
public class AdminSupportController : AdminBaseController
{
    private readonly RefundService _refundService;

    public AdminSupportController(ApplicationDbContext db, RefundService refundService) : base(db)
    {
        _refundService = refundService;
    }

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

        var latestRefundRequest = await Db.RefundRequests
            .Where(r => r.TicketId == ticket.TicketId)
            .OrderByDescending(r => r.RequestedAt)
            .FirstOrDefaultAsync();

        vm.Ticket = ticket;
        vm.Seats = seats;
        vm.Combos = combos;
        vm.Payments = payments;
        vm.LatestRefundRequest = latestRefundRequest;
        return View(vm);
    }

    // Hỗ trợ khách hàng: nhân viên hủy vé giúp khách. KHÔNG hủy/hoàn tiền ngay lập
    // tức nữa — hành động này tự tạo (hoặc dùng lại) một yêu cầu hoàn tiền và tự
    // động hoàn tất luôn bước duyệt "Nhân viên"; nếu người thực hiện có vai trò
    // Admin thì được hoàn tất cả bước "Admin" ngay, còn tài khoản Staff thì hồ sơ
    // sẽ chuyển sang trạng thái "Chờ admin duyệt" (xem /quan-tri/hoan-tien).
    [HttpPost, Route("/quan-tri/ho-tro/{ticketId:int}/huy-ve")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CancelTicket(int ticketId, decimal? refundAmount)
    {
        var staff = await GetCurrentStaffAsync();
        if (staff is null) return Redirect("/quan-tri/dang-nhap");

        var ticket = await Db.Tickets.FindAsync(ticketId);
        if (ticket is null) return NotFound();

        var (ok, message) = await _refundService.StaffInitiateAsync(ticket, refundAmount, staff);
        TempData[ok ? "Success" : "Error"] = message;
        return Redirect($"/quan-tri/ho-tro?ticketId={ticketId}");
    }
}

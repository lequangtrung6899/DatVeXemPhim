using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý thanh toán".
public class AdminPaymentController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminPaymentController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/thanh-toan")]
    public async Task<IActionResult> Index(string? status, string? q, int page = 1)
    {
        var query = Db.Payments
            .Include(p => p.Ticket).ThenInclude(t => t.Customer)
            .Include(p => p.Ticket).ThenInclude(t => t.Showtime).ThenInclude(s => s.Movie)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(p => p.PaymentStatus == status);
        }
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(p =>
                EF.Functions.Like(p.TransactionCode ?? "", $"%{term}%") ||
                EF.Functions.Like(p.Ticket.Customer.FullName, $"%{term}%") ||
                p.TicketId.ToString() == term);
        }

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var payments = await query.OrderByDescending(p => p.PaymentDate)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .Select(p => new AdminPaymentRow
            {
                PaymentId = p.PaymentId,
                TicketId = p.TicketId,
                CustomerName = p.Ticket.Customer.FullName,
                MovieTitle = p.Ticket.Showtime.Movie.Title,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod,
                PaymentStatus = p.PaymentStatus,
                TransactionCode = p.TransactionCode,
                PaymentDate = p.PaymentDate
            })
            .ToListAsync();

        ViewBag.Status = status;
        ViewBag.Q = q;
        var extra = (string.IsNullOrEmpty(status) ? "" : $"status={Uri.EscapeDataString(status)}&") +
                    (string.IsNullOrEmpty(q) ? "" : $"q={Uri.EscapeDataString(q)}&");
        ViewBag.Pagination = new PaginationVM { Page = page, TotalPages = totalPages, BaseUrl = "/quan-tri/thanh-toan?" + extra };
        return View(payments);
    }

    [HttpPost, Route("/quan-tri/thanh-toan/{id:int}/cap-nhat")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string paymentStatus)
    {
        var payment = await Db.Payments.FindAsync(id);
        if (payment is null) return NotFound();

        if (await IsAdminRoleAsync())
        {
            payment.PaymentStatus = paymentStatus;
            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật trạng thái thanh toán.";
        }
        else
        {
            // Đổi trạng thái thanh toán (đặc biệt là đánh dấu "Đã hoàn tiền") động chạm
            // trực tiếp đến tiền — nhân viên không được tự quyết, phải qua Admin duyệt.
            await SubmitPendingChangeAsync("Payment", payment.PaymentId, "Update",
                new PaymentStatusChangeDto { PaymentStatus = paymentStatus },
                $"Cập nhật thanh toán #{payment.PaymentId} (vé #{payment.TicketId}): {payment.PaymentStatus} → {paymentStatus}");
            TempData["Success"] = "Đã gửi yêu cầu cập nhật thanh toán — chờ Quản trị viên duyệt.";
        }
        return Redirect("/quan-tri/thanh-toan");
    }
}

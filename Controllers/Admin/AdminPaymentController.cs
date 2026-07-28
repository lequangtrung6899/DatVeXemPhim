using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý thanh toán".
public class AdminPaymentController : AdminBaseController
{
    public AdminPaymentController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/thanh-toan")]
    public async Task<IActionResult> Index(string? status, string? q)
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

        var payments = await query.OrderByDescending(p => p.PaymentDate)
            .Take(200)
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
        return View(payments);
    }

    [HttpPost, Route("/quan-tri/thanh-toan/{id:int}/cap-nhat")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string paymentStatus)
    {
        var payment = await Db.Payments.FindAsync(id);
        if (payment is null) return NotFound();

        payment.PaymentStatus = paymentStatus;
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật trạng thái thanh toán.";
        return Redirect("/quan-tri/thanh-toan");
    }
}

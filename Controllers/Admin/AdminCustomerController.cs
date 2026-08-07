using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý khách hàng".
public class AdminCustomerController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminCustomerController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/khach-hang")]
    public async Task<IActionResult> Index(string? q, int page = 1)
    {
        var query = Db.Customers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(c => EF.Functions.Like(c.FullName, $"%{q}%") || EF.Functions.Like(c.Email, $"%{q}%"));
        }

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var customers = await query.OrderByDescending(c => c.CreatedAt).Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Q = q;
        ViewBag.Pagination = new PaginationVM
        {
            Page = page,
            TotalPages = totalPages,
            BaseUrl = "/quan-tri/khach-hang?" + (string.IsNullOrEmpty(q) ? "" : $"q={Uri.EscapeDataString(q)}&")
        };
        return View(customers);
    }

    [HttpGet, Route("/quan-tri/khach-hang/{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var customer = await Db.Customers.FindAsync(id);
        if (customer is null) return NotFound();

        var tickets = await Db.Tickets
            .Include(t => t.Showtime).ThenInclude(s => s.Movie)
            .Where(t => t.CustomerId == id)
            .OrderByDescending(t => t.BookingDate)
            .Take(20)
            .ToListAsync();

        ViewBag.Tickets = tickets;
        return View(customer);
    }

    [HttpPost, Route("/quan-tri/khach-hang/{id:int}/khoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var customer = await Db.Customers.FindAsync(id);
        if (customer is null) return NotFound();

        var newIsActive = !customer.IsActive;

        if (await IsAdminRoleAsync())
        {
            customer.IsActive = newIsActive;
            await Db.SaveChangesAsync();
            TempData["Success"] = customer.IsActive ? "Đã mở khóa tài khoản." : "Đã khóa tài khoản.";
        }
        else
        {
            // Khóa/mở khóa tài khoản khách hàng ảnh hưởng trực tiếp đến quyền truy cập của
            // khách — nhân viên có thể lạm dụng để trả đũa/thiên vị, nên phải qua Admin duyệt.
            await SubmitPendingChangeAsync("Customer", customer.CustomerId, "Update",
                new CustomerLockChangeDto { IsActive = newIsActive },
                $"{(newIsActive ? "Mở khóa" : "Khóa")} tài khoản khách hàng '{customer.FullName}'");
            TempData["Success"] = $"Đã gửi yêu cầu {(newIsActive ? "mở khóa" : "khóa")} tài khoản — chờ Quản trị viên duyệt.";
        }
        return Redirect("/quan-tri/khach-hang");
    }
}

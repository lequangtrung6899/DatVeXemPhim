using DatVeXemPhim.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý khách hàng".
public class AdminCustomerController : AdminBaseController
{
    public AdminCustomerController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/khach-hang")]
    public async Task<IActionResult> Index(string? q)
    {
        var query = Db.Customers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(c => EF.Functions.Like(c.FullName, $"%{q}%") || EF.Functions.Like(c.Email, $"%{q}%"));
        }
        var customers = await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
        ViewBag.Q = q;
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

        customer.IsActive = !customer.IsActive;
        await Db.SaveChangesAsync();
        TempData["Success"] = customer.IsActive ? "Đã mở khóa tài khoản." : "Đã khóa tài khoản.";
        return Redirect("/quan-tri/khach-hang");
    }
}

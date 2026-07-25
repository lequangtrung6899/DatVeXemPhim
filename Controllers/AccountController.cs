using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class AccountController : BaseController
{
    public AccountController(ApplicationDbContext db) : base(db) { }

    // GET /dang-nhap
    [HttpGet, Route("/dang-nhap")]
    public IActionResult Login(string? next)
    {
        return View(new LoginVM { Error = null, Next = next ?? "/" });
    }

    // POST /dang-nhap
    // Demo auth, mirroring the original Express app: any password is accepted for a known email.
    [HttpPost, Route("/dang-nhap")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, string? next)
    {
        var customer = await Db.Customers.FirstOrDefaultAsync(c => c.Email == email);
        if (customer != null)
        {
            SignIn(customer.CustomerId);
            return Redirect(string.IsNullOrEmpty(next) ? "/" : next);
        }

        return View(new LoginVM
        {
            Error = "Email không tồn tại. Hãy đăng ký tài khoản mới.",
            Next = next ?? "/"
        });
    }

    // GET /dang-ky
    [HttpGet, Route("/dang-ky")]
    public IActionResult Register() => View(new RegisterVM());

    // POST /dang-ky
    [HttpPost, Route("/dang-ky")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string fullName, string email, string? phone, string password)
    {
        var existing = await Db.Customers.AnyAsync(c => c.Email == email);
        if (existing)
        {
            return View(new RegisterVM { Error = "Email đã được sử dụng." });
        }

        var customer = new Customer
        {
            FullName = fullName,
            Email = email,
            PasswordHash = "demo$" + password, // demo only — not a real hash
            Phone = phone,
            LoyaltyPoint = 0,
            MembershipRank = "Thành viên mới",
            IsActive = true,
            CreatedAt = DateTime.Now
        };
        Db.Customers.Add(customer);
        await Db.SaveChangesAsync();

        SignIn(customer.CustomerId);
        return Redirect("/");
    }

    // POST /dang-xuat
    [HttpPost, Route("/dang-xuat")]
    [ValidateAntiForgeryToken]
    public IActionResult LogoutPost()
    {
        SignOutCustomer();
        return Redirect("/");
    }

    // GET /dang-xuat
    [HttpGet, Route("/dang-xuat")]
    public IActionResult LogoutGet()
    {
        SignOutCustomer();
        return Redirect("/");
    }

    // GET /tai-khoan/ve-cua-toi
    [HttpGet, Route("/tai-khoan/ve-cua-toi")]
    public async Task<IActionResult> MyTickets()
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap?next=/tai-khoan/ve-cua-toi");

        var tickets = await Db.Tickets
            .Include(t => t.Showtime).ThenInclude(s => s.Movie)
            .Where(t => t.CustomerId == customer.CustomerId)
            .OrderByDescending(t => t.BookingDate)
            .Select(t => new MyTicketRow
            {
                TicketId = t.TicketId,
                Title = t.Showtime.Movie.Title,
                PosterUrl = t.Showtime.Movie.PosterUrl,
                StartTime = t.Showtime.StartTime,
                TotalAmount = t.TotalAmount,
                Status = t.Status
            })
            .ToListAsync();

        return View(new MyTicketsVM { Tickets = tickets });
    }
}

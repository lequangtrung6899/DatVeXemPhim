using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
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
    [HttpPost, Route("/dang-nhap")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM form, string? next)
    {
        form.Next = next ?? "/";

        if (!ModelState.IsValid)
        {
            form.Error = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View(form);
        }

        var customer = await Db.Customers.FirstOrDefaultAsync(c => c.Email == form.Email);

        // Cố tình dùng chung một thông báo lỗi cho cả hai trường hợp "email không tồn tại"
        // và "sai mật khẩu" — tránh lộ thông tin tài khoản nào đã đăng ký (user enumeration).
        if (customer is null || !customer.IsActive || !PasswordHasherHelper.Verify(customer.PasswordHash, form.Password))
        {
            form.Error = "Email hoặc mật khẩu không đúng.";
            form.Password = string.Empty;
            return View(form);
        }

        SignIn(customer.CustomerId);
        return Redirect(string.IsNullOrEmpty(form.Next) ? "/" : form.Next);
    }

    // GET /dang-ky
    [HttpGet, Route("/dang-ky")]
    public IActionResult Register() => View(new RegisterVM());

    // POST /dang-ky
    [HttpPost, Route("/dang-ky")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM form)
    {
        if (!ModelState.IsValid)
        {
            form.Error = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            form.Password = string.Empty;
            return View(form);
        }

        var existing = await Db.Customers.AnyAsync(c => c.Email == form.Email);
        if (existing)
        {
            form.Error = "Email đã được sử dụng.";
            form.Password = string.Empty;
            return View(form);
        }

        var customer = new Customer
        {
            FullName = form.FullName.Trim(),
            Email = form.Email.Trim(),
            PasswordHash = PasswordHasherHelper.Hash(form.Password),
            Phone = string.IsNullOrWhiteSpace(form.Phone) ? null : form.Phone.Trim(),
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
            .Where(t => t.CustomerId == customer.CustomerId && t.Status != "Chờ thanh toán")
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

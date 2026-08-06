using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Đăng nhập" / "Đăng xuất tài khoản" — phía Nhân viên/Quản trị viên.
public class AdminAccountController : AdminBaseController
{
    public AdminAccountController(ApplicationDbContext db) : base(db) { }

    // GET /quan-tri/dang-nhap
    [HttpGet, Route("/quan-tri/dang-nhap")]
    public IActionResult Login(string? next)
    {
        return View(new AdminLoginVM { Next = next ?? "/quan-tri" });
    }

    // POST /quan-tri/dang-nhap
    [HttpPost, Route("/quan-tri/dang-nhap")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AdminLoginVM form, string? next)
    {
        form.Next = next ?? "/quan-tri";

        if (!ModelState.IsValid)
        {
            form.Error = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View(form);
        }

        var staff = await Db.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == form.Username && u.IsActive);

        // Thông báo lỗi chung cho cả "sai tài khoản" và "sai mật khẩu" để tránh lộ
        // thông tin tài khoản nào tồn tại trong hệ thống.
        if (staff is null || !PasswordHasherHelper.Verify(staff.PasswordHash, form.Password))
        {
            form.Error = "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa.";
            form.Password = string.Empty;
            return View(form);
        }

        SignIn(staff.UserId);
        return Redirect(string.IsNullOrEmpty(form.Next) ? "/quan-tri" : form.Next);
    }

    [HttpPost, Route("/quan-tri/dang-xuat")]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        SignOutStaff();
        return Redirect("/quan-tri/dang-nhap");
    }
}

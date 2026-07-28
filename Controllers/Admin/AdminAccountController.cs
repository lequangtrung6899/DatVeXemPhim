using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
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
    // Demo auth, mirroring the customer-facing AccountController: any password is accepted
    // for a known, active username. Swap in real password hashing before going to production.
    [HttpPost, Route("/quan-tri/dang-nhap")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string username, string password, string? next)
    {
        var staff = await Db.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

        if (staff != null)
        {
            SignIn(staff.UserId);
            return Redirect(string.IsNullOrEmpty(next) ? "/quan-tri" : next);
        }

        return View(new AdminLoginVM
        {
            Error = "Tên đăng nhập không tồn tại hoặc tài khoản đã bị khóa.",
            Next = next ?? "/quan-tri"
        });
    }

    [HttpPost, Route("/quan-tri/dang-xuat")]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        SignOutStaff();
        return Redirect("/quan-tri/dang-nhap");
    }
}

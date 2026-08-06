using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý tài khoản nhân viên" — chỉ dành cho vai trò Admin.
public class AdminStaffController : AdminBaseController
{
    public AdminStaffController(ApplicationDbContext db) : base(db) { }

    private async Task<IActionResult?> GuardAdminOnlyAsync()
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền quản lý tài khoản nhân viên.";
            return Redirect("/quan-tri");
        }
        return null;
    }

    [HttpGet, Route("/quan-tri/nhan-vien")]
    public async Task<IActionResult> Index()
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var staff = await Db.Users.Include(u => u.Role).OrderBy(u => u.FullName).ToListAsync();
        return View(staff);
    }

    [HttpGet, Route("/quan-tri/nhan-vien/them")]
    public async Task<IActionResult> Create()
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var vm = new AdminStaffEditVM { User = new User(), Roles = await Db.Roles.OrderBy(r => r.RoleName).ToListAsync() };
        return View("Edit", vm);
    }

    [HttpGet, Route("/quan-tri/nhan-vien/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var user = await Db.Users.FindAsync(id);
        if (user is null) return NotFound();

        var vm = new AdminStaffEditVM { User = user, Roles = await Db.Roles.OrderBy(r => r.RoleName).ToListAsync() };
        return View(vm);
    }

    [HttpPost, Route("/quan-tri/nhan-vien/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(int userId, string username, string fullName, string email, string? phone, int roleId, bool isActive, string? password)
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var dup = await Db.Users.AnyAsync(u => u.Username == username && u.UserId != userId);
        if (dup)
        {
            TempData["Error"] = "Tên đăng nhập đã tồn tại.";
            return Redirect(userId == 0 ? "/quan-tri/nhan-vien/them" : $"/quan-tri/nhan-vien/{userId}/sua");
        }

        // Validate tối thiểu ở server: tài khoản mới bắt buộc phải có mật khẩu (>= 6 ký tự).
        if (userId == 0 && (string.IsNullOrWhiteSpace(password) || password.Length < 6))
        {
            TempData["Error"] = "Mật khẩu cho tài khoản mới phải có ít nhất 6 ký tự.";
            return Redirect("/quan-tri/nhan-vien/them");
        }
        if (!string.IsNullOrEmpty(password) && password.Length < 6)
        {
            TempData["Error"] = "Mật khẩu mới phải có ít nhất 6 ký tự.";
            return Redirect($"/quan-tri/nhan-vien/{userId}/sua");
        }

        if (userId == 0)
        {
            Db.Users.Add(new User
            {
                Username = username.Trim(),
                FullName = fullName.Trim(),
                Email = email.Trim(),
                Phone = phone,
                RoleId = roleId,
                IsActive = isActive,
                PasswordHash = PasswordHasherHelper.Hash(password!),
                CreatedAt = DateTime.Now
            });
            TempData["Success"] = "Đã thêm tài khoản nhân viên mới.";
        }
        else
        {
            var user = await Db.Users.FindAsync(userId);
            if (user is null) return NotFound();
            user.Username = username.Trim();
            user.FullName = fullName.Trim();
            user.Email = email.Trim();
            user.Phone = phone;
            user.RoleId = roleId;
            user.IsActive = isActive;
            if (!string.IsNullOrEmpty(password))
            {
                user.PasswordHash = PasswordHasherHelper.Hash(password);
            }
            TempData["Success"] = "Đã cập nhật tài khoản nhân viên.";
        }

        await Db.SaveChangesAsync();
        return Redirect("/quan-tri/nhan-vien");
    }

    [HttpPost, Route("/quan-tri/nhan-vien/{id:int}/khoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var user = await Db.Users.FindAsync(id);
        if (user is null) return NotFound();

        var currentStaff = await GetCurrentStaffAsync();
        if (currentStaff != null && currentStaff.UserId == id)
        {
            TempData["Error"] = "Bạn không thể tự khóa tài khoản của chính mình.";
            return Redirect("/quan-tri/nhan-vien");
        }

        user.IsActive = !user.IsActive;
        await Db.SaveChangesAsync();
        TempData["Success"] = user.IsActive ? "Đã mở khóa tài khoản." : "Đã khóa tài khoản.";
        return Redirect("/quan-tri/nhan-vien");
    }
}

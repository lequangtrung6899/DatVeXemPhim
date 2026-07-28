using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý vai trò" — chỉ dành cho vai trò Admin.
public class AdminRoleController : AdminBaseController
{
    public AdminRoleController(ApplicationDbContext db) : base(db) { }

    private async Task<IActionResult?> GuardAdminOnlyAsync()
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền quản lý vai trò.";
            return Redirect("/quan-tri");
        }
        return null;
    }

    [HttpGet, Route("/quan-tri/vai-tro")]
    public async Task<IActionResult> Index()
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var roles = await Db.Roles.OrderBy(r => r.RoleName).ToListAsync();
        var counts = await Db.Users.GroupBy(u => u.RoleId).Select(g => new { g.Key, Count = g.Count() }).ToListAsync();
        ViewBag.UserCounts = counts.ToDictionary(x => x.Key, x => x.Count);
        return View(roles);
    }

    [HttpGet, Route("/quan-tri/vai-tro/them")]
    public async Task<IActionResult> Create()
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;
        return View("Edit", new Role());
    }

    [HttpGet, Route("/quan-tri/vai-tro/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var role = await Db.Roles.FindAsync(id);
        if (role is null) return NotFound();
        return View(role);
    }

    [HttpPost, Route("/quan-tri/vai-tro/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(int roleId, string roleName)
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        if (string.IsNullOrWhiteSpace(roleName))
        {
            TempData["Error"] = "Tên vai trò không được để trống.";
            return Redirect("/quan-tri/vai-tro");
        }

        var dup = await Db.Roles.AnyAsync(r => r.RoleName == roleName && r.RoleId != roleId);
        if (dup)
        {
            TempData["Error"] = "Tên vai trò đã tồn tại.";
            return Redirect("/quan-tri/vai-tro");
        }

        if (roleId == 0)
        {
            Db.Roles.Add(new Role { RoleName = roleName.Trim() });
            TempData["Success"] = "Đã thêm vai trò mới.";
        }
        else
        {
            var role = await Db.Roles.FindAsync(roleId);
            if (role is null) return NotFound();
            role.RoleName = roleName.Trim();
            TempData["Success"] = "Đã cập nhật vai trò.";
        }

        await Db.SaveChangesAsync();
        return Redirect("/quan-tri/vai-tro");
    }

    [HttpPost, Route("/quan-tri/vai-tro/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (await GuardAdminOnlyAsync() is { } guard) return guard;

        var role = await Db.Roles.FindAsync(id);
        if (role is null) return NotFound();

        var inUse = await Db.Users.AnyAsync(u => u.RoleId == id);
        if (inUse)
        {
            TempData["Error"] = "Không thể xóa: vai trò đang được gán cho ít nhất một nhân viên.";
            return Redirect("/quan-tri/vai-tro");
        }

        Db.Roles.Remove(role);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa vai trò.";
        return Redirect("/quan-tri/vai-tro");
    }
}

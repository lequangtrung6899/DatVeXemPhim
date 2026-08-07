using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý Combo".
public class AdminComboController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminComboController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/combo")]
    public async Task<IActionResult> Index(int page = 1)
    {
        var query = Db.Combos.OrderBy(c => c.ComboName);
        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var combos = await query.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Pagination = new PaginationVM { Page = page, TotalPages = totalPages, BaseUrl = "/quan-tri/combo?" };
        return View(combos);
    }

    [HttpGet, Route("/quan-tri/combo/them")]
    public IActionResult Create() => View("Edit", new Combo { IsActive = true });

    [HttpGet, Route("/quan-tri/combo/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        var combo = await Db.Combos.FindAsync(id);
        if (combo is null) return NotFound();
        return View(combo);
    }

    [HttpPost, Route("/quan-tri/combo/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(int comboId, string comboName, string? description, decimal price, bool isActive)
    {
        var isAdmin = await IsAdminRoleAsync();
        comboName = comboName.Trim();

        if (comboId == 0)
        {
            if (isAdmin)
            {
                Db.Combos.Add(new Combo { ComboName = comboName, Description = description, Price = price, IsActive = isActive });
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã thêm combo mới.";
            }
            else
            {
                await SubmitPendingChangeAsync("Combo", null, "Create",
                    new ComboChangeDto { ComboName = comboName, Description = description, Price = price, IsActive = isActive },
                    $"Thêm combo mới '{comboName}' — giá {FormatVND(price)}");
                TempData["Success"] = "Đã gửi yêu cầu thêm combo mới — chờ Quản trị viên duyệt.";
            }
        }
        else
        {
            var combo = await Db.Combos.FindAsync(comboId);
            if (combo is null) return NotFound();

            if (isAdmin)
            {
                combo.ComboName = comboName;
                combo.Description = description;
                combo.Price = price;
                combo.IsActive = isActive;
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật combo.";
            }
            else
            {
                await SubmitPendingChangeAsync("Combo", combo.ComboId, "Update",
                    new ComboChangeDto { ComboName = comboName, Description = description, Price = price, IsActive = isActive },
                    $"Sửa combo '{combo.ComboName}': giá {FormatVND(combo.Price)} → {FormatVND(price)}");
                TempData["Success"] = "Đã gửi yêu cầu sửa combo — chờ Quản trị viên duyệt.";
            }
        }
        return Redirect("/quan-tri/combo");
    }

    [HttpPost, Route("/quan-tri/combo/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var combo = await Db.Combos.FindAsync(id);
        if (combo is null) return NotFound();

        var inUse = await Db.TicketCombos.AnyAsync(tc => tc.ComboId == id);
        if (inUse)
        {
            TempData["Error"] = "Không thể xóa: combo đã từng được đặt mua. Hãy tắt \"Đang bán\" thay vì xóa.";
            return Redirect("/quan-tri/combo");
        }

        if (await IsAdminRoleAsync())
        {
            Db.Combos.Remove(combo);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã xóa combo.";
        }
        else
        {
            await SubmitPendingChangeAsync("Combo", combo.ComboId, "Delete", (object?)null, $"Xóa combo '{combo.ComboName}'");
            TempData["Success"] = "Đã gửi yêu cầu xóa combo — chờ Quản trị viên duyệt.";
        }
        return Redirect("/quan-tri/combo");
    }
}

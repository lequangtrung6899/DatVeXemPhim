using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý Combo".
public class AdminComboController : AdminBaseController
{
    public AdminComboController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/combo")]
    public async Task<IActionResult> Index()
    {
        var combos = await Db.Combos.OrderBy(c => c.ComboName).ToListAsync();
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
        if (comboId == 0)
        {
            Db.Combos.Add(new Combo { ComboName = comboName.Trim(), Description = description, Price = price, IsActive = isActive });
            TempData["Success"] = "Đã thêm combo mới.";
        }
        else
        {
            var combo = await Db.Combos.FindAsync(comboId);
            if (combo is null) return NotFound();
            combo.ComboName = comboName.Trim();
            combo.Description = description;
            combo.Price = price;
            combo.IsActive = isActive;
            TempData["Success"] = "Đã cập nhật combo.";
        }
        await Db.SaveChangesAsync();
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

        Db.Combos.Remove(combo);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa combo.";
        return Redirect("/quan-tri/combo");
    }
}

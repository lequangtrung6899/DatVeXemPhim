using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý Voucher".
public class AdminVoucherController : AdminBaseController
{
    public AdminVoucherController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/voucher")]
    public async Task<IActionResult> Index()
    {
        var vouchers = await Db.Vouchers.OrderByDescending(v => v.StartDate).ToListAsync();
        return View(vouchers);
    }

    [HttpGet, Route("/quan-tri/voucher/them")]
    public IActionResult Create() => View("Edit", new Voucher
    {
        IsActive = true,
        StartDate = DateTime.Now.Date,
        EndDate = DateTime.Now.Date.AddMonths(1),
        DiscountType = "Phần trăm"
    });

    [HttpGet, Route("/quan-tri/voucher/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        var voucher = await Db.Vouchers.FindAsync(id);
        if (voucher is null) return NotFound();
        return View(voucher);
    }

    [HttpPost, Route("/quan-tri/voucher/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        int voucherId, string code, string discountType, decimal discountValue, decimal minOrderAmount,
        DateTime startDate, DateTime endDate, int usageLimit, bool isActive)
    {
        code = code.Trim().ToUpperInvariant();

        var dup = await Db.Vouchers.AnyAsync(v => v.Code == code && v.VoucherId != voucherId);
        if (dup)
        {
            TempData["Error"] = "Mã voucher đã tồn tại.";
            return Redirect(voucherId == 0 ? "/quan-tri/voucher/them" : $"/quan-tri/voucher/{voucherId}/sua");
        }

        if (voucherId == 0)
        {
            Db.Vouchers.Add(new Voucher
            {
                Code = code,
                DiscountType = discountType,
                DiscountValue = discountValue,
                MinOrderAmount = minOrderAmount,
                StartDate = startDate,
                EndDate = endDate,
                UsageLimit = usageLimit,
                UsedCount = 0,
                IsActive = isActive
            });
            TempData["Success"] = "Đã thêm voucher mới.";
        }
        else
        {
            var voucher = await Db.Vouchers.FindAsync(voucherId);
            if (voucher is null) return NotFound();
            voucher.Code = code;
            voucher.DiscountType = discountType;
            voucher.DiscountValue = discountValue;
            voucher.MinOrderAmount = minOrderAmount;
            voucher.StartDate = startDate;
            voucher.EndDate = endDate;
            voucher.UsageLimit = usageLimit;
            voucher.IsActive = isActive;
            TempData["Success"] = "Đã cập nhật voucher.";
        }
        await Db.SaveChangesAsync();
        return Redirect("/quan-tri/voucher");
    }

    [HttpPost, Route("/quan-tri/voucher/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var voucher = await Db.Vouchers.FindAsync(id);
        if (voucher is null) return NotFound();

        var inUse = await Db.Tickets.AnyAsync(t => t.VoucherId == id);
        if (inUse)
        {
            TempData["Error"] = "Không thể xóa: voucher đã được sử dụng trong ít nhất một đơn vé. Hãy tắt \"Kích hoạt\" thay vì xóa.";
            return Redirect("/quan-tri/voucher");
        }

        Db.Vouchers.Remove(voucher);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa voucher.";
        return Redirect("/quan-tri/voucher");
    }
}

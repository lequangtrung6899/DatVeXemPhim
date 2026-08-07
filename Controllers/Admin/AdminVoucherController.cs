using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý Voucher".
public class AdminVoucherController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminVoucherController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/voucher")]
    public async Task<IActionResult> Index(int page = 1)
    {
        var query = Db.Vouchers.OrderByDescending(v => v.StartDate);
        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var vouchers = await query.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Pagination = new PaginationVM { Page = page, TotalPages = totalPages, BaseUrl = "/quan-tri/voucher?" };
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

        var isAdmin = await IsAdminRoleAsync();
        var dto = new VoucherChangeDto
        {
            Code = code, DiscountType = discountType, DiscountValue = discountValue, MinOrderAmount = minOrderAmount,
            StartDate = startDate, EndDate = endDate, UsageLimit = usageLimit, IsActive = isActive
        };

        if (voucherId == 0)
        {
            if (isAdmin)
            {
                Db.Vouchers.Add(new Voucher
                {
                    Code = code, DiscountType = discountType, DiscountValue = discountValue, MinOrderAmount = minOrderAmount,
                    StartDate = startDate, EndDate = endDate, UsageLimit = usageLimit, UsedCount = 0, IsActive = isActive
                });
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã thêm voucher mới.";
            }
            else
            {
                // Voucher là nơi dễ bị lạm quyền nhất (nhân viên có thể tự tạo mã giảm giá
                // cho bản thân/người quen) nên LUÔN phải qua Admin duyệt, không có ngoại lệ.
                await SubmitPendingChangeAsync("Voucher", null, "Create", dto,
                    $"Thêm voucher mới '{code}' — giảm {(discountType == "Phần trăm" ? discountValue + "%" : FormatVND(discountValue))}");
                TempData["Success"] = "Đã gửi yêu cầu thêm voucher mới — chờ Quản trị viên duyệt.";
            }
        }
        else
        {
            var voucher = await Db.Vouchers.FindAsync(voucherId);
            if (voucher is null) return NotFound();

            if (isAdmin)
            {
                voucher.Code = code;
                voucher.DiscountType = discountType;
                voucher.DiscountValue = discountValue;
                voucher.MinOrderAmount = minOrderAmount;
                voucher.StartDate = startDate;
                voucher.EndDate = endDate;
                voucher.UsageLimit = usageLimit;
                voucher.IsActive = isActive;
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật voucher.";
            }
            else
            {
                await SubmitPendingChangeAsync("Voucher", voucher.VoucherId, "Update", dto,
                    $"Sửa voucher '{voucher.Code}': giảm {(voucher.DiscountType == "Phần trăm" ? voucher.DiscountValue + "%" : FormatVND(voucher.DiscountValue))} → {(discountType == "Phần trăm" ? discountValue + "%" : FormatVND(discountValue))}");
                TempData["Success"] = "Đã gửi yêu cầu sửa voucher — chờ Quản trị viên duyệt.";
            }
        }
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

        if (await IsAdminRoleAsync())
        {
            Db.Vouchers.Remove(voucher);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã xóa voucher.";
        }
        else
        {
            await SubmitPendingChangeAsync("Voucher", voucher.VoucherId, "Delete", (object?)null, $"Xóa voucher '{voucher.Code}'");
            TempData["Success"] = "Đã gửi yêu cầu xóa voucher — chờ Quản trị viên duyệt.";
        }
        return Redirect("/quan-tri/voucher");
    }
}

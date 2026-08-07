using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Hỗ trợ khách hàng" (form Liên hệ ở footer): nhân viên xem các yêu cầu
// khách hàng gửi qua /ho-tro/lien-he và đánh dấu đã xử lý.
public class AdminContactController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminContactController(ApplicationDbContext db) : base(db) { }

    // GET /quan-tri/lien-he?status=Chờ xử lý|Đã xử lý|Tất cả
    [HttpGet, Route("/quan-tri/lien-he")]
    public async Task<IActionResult> Index(string? status, int page = 1)
    {
        var effectiveStatus = string.IsNullOrWhiteSpace(status) ? "Chờ xử lý" : status;

        var query = Db.ContactMessages.AsQueryable();
        query = effectiveStatus switch
        {
            "Chờ xử lý" => query.Where(m => !m.IsResolved),
            "Đã xử lý" => query.Where(m => m.IsResolved),
            _ => query
        };

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var messages = await query.OrderByDescending(m => m.CreatedAt)
            .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Status = effectiveStatus;
        ViewBag.Pagination = new PaginationVM
        {
            Page = page,
            TotalPages = totalPages,
            BaseUrl = $"/quan-tri/lien-he?status={Uri.EscapeDataString(effectiveStatus)}&"
        };
        return View(messages);
    }

    // POST /quan-tri/lien-he/{id}/da-xu-ly
    [HttpPost, Route("/quan-tri/lien-he/{id:int}/da-xu-ly")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkResolved(int id)
    {
        var message = await Db.ContactMessages.FindAsync(id);
        if (message is null) return NotFound();

        message.IsResolved = true;
        message.ResolvedAt = DateTime.Now;
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã đánh dấu yêu cầu là đã xử lý.";
        return Redirect("/quan-tri/lien-he");
    }

    // POST /quan-tri/lien-he/{id}/chua-xu-ly — cho phép mở lại nếu đánh dấu nhầm.
    [HttpPost, Route("/quan-tri/lien-he/{id:int}/chua-xu-ly")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkUnresolved(int id)
    {
        var message = await Db.ContactMessages.FindAsync(id);
        if (message is null) return NotFound();

        message.IsResolved = false;
        message.ResolvedAt = null;
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã chuyển yêu cầu về trạng thái chờ xử lý.";
        return Redirect("/quan-tri/lien-he");
    }

    // POST /quan-tri/lien-he/{id}/xoa
    [HttpPost, Route("/quan-tri/lien-he/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var message = await Db.ContactMessages.FindAsync(id);
        if (message is null) return NotFound();

        Db.ContactMessages.Remove(message);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa yêu cầu liên hệ.";
        return Redirect("/quan-tri/lien-he");
    }
}

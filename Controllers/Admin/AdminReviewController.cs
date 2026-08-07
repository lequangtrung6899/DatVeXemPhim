using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý đánh giá phim" (2.6.12) / "Quản lý đánh giá" (2.6.24) —
// hai mục trong báo cáo mô tả cùng một chức năng: duyệt/từ chối/xóa đánh giá của khách hàng.
public class AdminReviewController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminReviewController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/danh-gia")]
    public async Task<IActionResult> Index(string? status, int page = 1)
    {
        var effectiveStatus = string.IsNullOrWhiteSpace(status) ? "Chờ duyệt" : status;

        var query = Db.Reviews.Include(r => r.Customer).Include(r => r.Movie).AsQueryable();
        if (effectiveStatus != "Tất cả")
        {
            query = query.Where(r => r.Status == effectiveStatus);
        }

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var reviews = await query.OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .Select(r => new AdminReviewRow
            {
                ReviewId = r.ReviewId,
                MovieTitle = r.Movie.Title,
                CustomerName = r.Customer.FullName,
                Rating = r.Rating,
                Comment = r.Comment,
                Status = r.Status,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();

        ViewBag.Status = effectiveStatus;
        ViewBag.Pagination = new PaginationVM
        {
            Page = page,
            TotalPages = totalPages,
            BaseUrl = $"/quan-tri/danh-gia?status={Uri.EscapeDataString(effectiveStatus)}&"
        };
        return View(reviews);
    }

    [HttpPost, Route("/quan-tri/danh-gia/{id:int}/duyet")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        var staff = await GetCurrentStaffAsync();
        var review = await Db.Reviews.FindAsync(id);
        if (review is null) return NotFound();

        review.Status = "Đã duyệt";
        review.ApprovedBy = staff?.UserId;
        review.ApprovedAt = DateTime.Now;
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã duyệt đánh giá.";
        return Redirect("/quan-tri/danh-gia");
    }

    [HttpPost, Route("/quan-tri/danh-gia/{id:int}/tu-choi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int id)
    {
        var staff = await GetCurrentStaffAsync();
        var review = await Db.Reviews.FindAsync(id);
        if (review is null) return NotFound();

        review.Status = "Từ chối";
        review.ApprovedBy = staff?.UserId;
        review.ApprovedAt = DateTime.Now;
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã từ chối đánh giá.";
        return Redirect("/quan-tri/danh-gia");
    }

    [HttpPost, Route("/quan-tri/danh-gia/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await Db.Reviews.FindAsync(id);
        if (review is null) return NotFound();

        Db.Reviews.Remove(review);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa đánh giá.";
        return Redirect("/quan-tri/danh-gia");
    }
}

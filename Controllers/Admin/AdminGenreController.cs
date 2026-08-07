using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý thể loại".
public class AdminGenreController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminGenreController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/the-loai")]
    public async Task<IActionResult> Index(int page = 1)
    {
        var query = Db.Genres.OrderBy(g => g.GenreName);
        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var genres = await query.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Pagination = new PaginationVM { Page = page, TotalPages = totalPages, BaseUrl = "/quan-tri/the-loai?" };
        return View(genres);
    }

    [HttpGet, Route("/quan-tri/the-loai/them")]
    public IActionResult Create() => View("Edit", new Genre());

    [HttpGet, Route("/quan-tri/the-loai/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        var genre = await Db.Genres.FindAsync(id);
        if (genre is null) return NotFound();
        return View(genre);
    }

    [HttpPost, Route("/quan-tri/the-loai/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(int genreId, string genreName)
    {
        if (string.IsNullOrWhiteSpace(genreName))
        {
            TempData["Error"] = "Tên thể loại không được để trống.";
            return Redirect(genreId == 0 ? "/quan-tri/the-loai/them" : $"/quan-tri/the-loai/{genreId}/sua");
        }
        genreName = genreName.Trim();
        var isAdmin = await IsAdminRoleAsync();

        if (genreId == 0)
        {
            if (isAdmin)
            {
                Db.Genres.Add(new Genre { GenreName = genreName });
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã thêm thể loại mới.";
            }
            else
            {
                await SubmitPendingChangeAsync("Genre", null, "Create",
                    new GenreChangeDto { GenreName = genreName }, $"Thêm thể loại mới '{genreName}'");
                TempData["Success"] = "Đã gửi yêu cầu thêm thể loại mới — chờ Quản trị viên duyệt.";
            }
        }
        else
        {
            var genre = await Db.Genres.FindAsync(genreId);
            if (genre is null) return NotFound();

            if (isAdmin)
            {
                genre.GenreName = genreName;
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật thể loại.";
            }
            else
            {
                await SubmitPendingChangeAsync("Genre", genre.GenreId, "Update",
                    new GenreChangeDto { GenreName = genreName }, $"Sửa thể loại '{genre.GenreName}' → '{genreName}'");
                TempData["Success"] = "Đã gửi yêu cầu sửa thể loại — chờ Quản trị viên duyệt.";
            }
        }
        return Redirect("/quan-tri/the-loai");
    }

    [HttpPost, Route("/quan-tri/the-loai/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var genre = await Db.Genres.FindAsync(id);
        if (genre is null) return NotFound();

        var inUse = await Db.MovieGenres.AnyAsync(mg => mg.GenreId == id);
        if (inUse)
        {
            TempData["Error"] = "Không thể xóa: thể loại đang được gán cho ít nhất một phim.";
            return Redirect("/quan-tri/the-loai");
        }

        if (await IsAdminRoleAsync())
        {
            Db.Genres.Remove(genre);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã xóa thể loại.";
        }
        else
        {
            await SubmitPendingChangeAsync("Genre", genre.GenreId, "Delete", (object?)null, $"Xóa thể loại '{genre.GenreName}'");
            TempData["Success"] = "Đã gửi yêu cầu xóa thể loại — chờ Quản trị viên duyệt.";
        }
        return Redirect("/quan-tri/the-loai");
    }
}

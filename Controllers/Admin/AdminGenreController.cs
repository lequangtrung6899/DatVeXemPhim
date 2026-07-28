using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý thể loại".
public class AdminGenreController : AdminBaseController
{
    public AdminGenreController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/the-loai")]
    public async Task<IActionResult> Index()
    {
        var genres = await Db.Genres.OrderBy(g => g.GenreName).ToListAsync();
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

        if (genreId == 0)
        {
            Db.Genres.Add(new Genre { GenreName = genreName.Trim() });
            TempData["Success"] = "Đã thêm thể loại mới.";
        }
        else
        {
            var genre = await Db.Genres.FindAsync(genreId);
            if (genre is null) return NotFound();
            genre.GenreName = genreName.Trim();
            TempData["Success"] = "Đã cập nhật thể loại.";
        }

        await Db.SaveChangesAsync();
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

        Db.Genres.Remove(genre);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa thể loại.";
        return Redirect("/quan-tri/the-loai");
    }
}

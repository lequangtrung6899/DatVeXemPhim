using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class SearchController : BaseController
{
    public SearchController(ApplicationDbContext db) : base(db) { }

    // GET /tim-kiem?q=...
    [Route("/tim-kiem")]
    public async Task<IActionResult> Index(string? q)
    {
        var query = (q ?? string.Empty).Trim();
        var vm = new SearchVM { Q = query };

        if (!string.IsNullOrEmpty(query))
        {
            var movies = await Db.Movies
                .Where(m => EF.Functions.Like(m.Title, $"%{query}%") && m.ApprovalStatus == "Đã duyệt")
                .OrderByDescending(m => m.ReleaseDate)
                .ToListAsync();

            foreach (var m in movies)
                vm.Movies.Add(await MovieQueryHelper.AttachGenresAndRatingAsync(Db, m));
        }

        // Gợi ý tìm kiếm: hiển thị khi khách chưa nhập từ khóa nào, hoặc không tìm thấy
        // kết quả phù hợp — tránh trang trắng, đồng thời gợi ý luôn các phim đang hot.
        if (string.IsNullOrEmpty(query) || vm.Movies.Count == 0)
        {
            var suggested = await Db.Movies
                .Where(m => m.Status == "Đang chiếu" && m.ApprovalStatus == "Đã duyệt")
                .OrderByDescending(m => m.ReleaseDate)
                .Take(6)
                .ToListAsync();

            foreach (var m in suggested)
                vm.SuggestedMovies.Add(await MovieQueryHelper.AttachGenresAndRatingAsync(Db, m));

            vm.PopularKeywords = suggested.Select(m => m.Title).Distinct().Take(6).ToList();
        }

        return View(vm);
    }

    // GET /tim-kiem/goi-y?q=... — trả JSON gợi ý phim khi khách đang gõ (autocomplete).
    [HttpGet, Route("/tim-kiem/goi-y")]
    public async Task<IActionResult> Suggest(string? q)
    {
        var query = (q ?? string.Empty).Trim();
        if (query.Length < 2) return Json(Array.Empty<object>());

        var results = await Db.Movies
            .Where(m => EF.Functions.Like(m.Title, $"%{query}%") && m.ApprovalStatus == "Đã duyệt")
            .OrderByDescending(m => m.Status == "Đang chiếu")
            .ThenByDescending(m => m.ReleaseDate)
            .Take(8)
            .Select(m => new
            {
                id = m.MovieId,
                title = m.Title,
                posterUrl = m.PosterUrl,
                status = m.Status,
                duration = m.Duration
            })
            .ToListAsync();

        return Json(results);
    }
}

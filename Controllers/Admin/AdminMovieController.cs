using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý phim".
public class AdminMovieController : AdminBaseController
{
    public AdminMovieController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/phim")]
    public async Task<IActionResult> Index(string? q)
    {
        IQueryable<Movie> query = Db.Movies.OrderByDescending(m => m.CreatedAt);
        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(m => EF.Functions.Like(m.Title, $"%{q}%"));
        }
        var movies = await query.ToListAsync();
        ViewBag.Q = q;
        return View(movies);
    }

    [HttpGet, Route("/quan-tri/phim/them")]
    public async Task<IActionResult> Create()
    {
        var vm = new AdminMovieEditVM
        {
            Movie = new Movie(),
            AllGenres = await Db.Genres.OrderBy(g => g.GenreName).ToListAsync(),
            SelectedGenreIds = new List<int>()
        };
        return View("Edit", vm);
    }

    [HttpGet, Route("/quan-tri/phim/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await Db.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        var selectedGenreIds = await Db.MovieGenres.Where(mg => mg.MovieId == id).Select(mg => mg.GenreId).ToListAsync();

        var vm = new AdminMovieEditVM
        {
            Movie = movie,
            AllGenres = await Db.Genres.OrderBy(g => g.GenreName).ToListAsync(),
            SelectedGenreIds = selectedGenreIds
        };
        return View(vm);
    }

    [HttpPost, Route("/quan-tri/phim/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        int movieId, string title, string? description, int duration,
        string? posterUrl, string? bannerUrl, DateTime releaseDate, DateTime? endDate, string status,
        [FromForm(Name = "genreIds")] List<int>? genreIds)
    {
        genreIds ??= new List<int>();

        Movie movie;
        if (movieId == 0)
        {
            movie = new Movie { CreatedAt = DateTime.Now };
            Db.Movies.Add(movie);
        }
        else
        {
            var existing = await Db.Movies.FindAsync(movieId);
            if (existing is null) return NotFound();
            movie = existing;
        }

        movie.Title = title.Trim();
        movie.Description = description;
        movie.Duration = duration;
        movie.PosterUrl = posterUrl;
        movie.BannerUrl = bannerUrl;
        movie.ReleaseDate = releaseDate;
        movie.EndDate = endDate;
        movie.Status = status;

        await Db.SaveChangesAsync(); // ensure MovieId exists for new movies

        var currentLinks = await Db.MovieGenres.Where(mg => mg.MovieId == movie.MovieId).ToListAsync();
        Db.MovieGenres.RemoveRange(currentLinks.Where(l => !genreIds.Contains(l.GenreId)));
        foreach (var gId in genreIds.Except(currentLinks.Select(l => l.GenreId)))
        {
            Db.MovieGenres.Add(new MovieGenre { MovieId = movie.MovieId, GenreId = gId });
        }

        await Db.SaveChangesAsync();
        TempData["Success"] = movieId == 0 ? "Đã thêm phim mới." : "Đã cập nhật phim.";
        return Redirect("/quan-tri/phim");
    }

    [HttpPost, Route("/quan-tri/phim/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await Db.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        var hasShowtimes = await Db.Showtimes.AnyAsync(s => s.MovieId == id);
        if (hasShowtimes)
        {
            TempData["Error"] = "Không thể xóa: phim đang có suất chiếu liên kết. Hãy chuyển trạng thái sang \"Ngừng chiếu\" thay vì xóa.";
            return Redirect("/quan-tri/phim");
        }

        var genreLinks = Db.MovieGenres.Where(mg => mg.MovieId == id);
        Db.MovieGenres.RemoveRange(genreLinks);
        Db.Movies.Remove(movie);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa phim.";
        return Redirect("/quan-tri/phim");
    }
}

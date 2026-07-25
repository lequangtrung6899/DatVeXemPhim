using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class MoviesController : BaseController
{
    private readonly OmdbService _omdb;

    public MoviesController(ApplicationDbContext db, OmdbService omdb) : base(db)
    {
        _omdb = omdb;
    }

    // GET /phim?status=...&genre=...
    [Route("/phim")]
    public async Task<IActionResult> Index(string? status, int? genre)
    {
        var effectiveStatus = string.IsNullOrWhiteSpace(status) ? "Đang chiếu" : status;

        IQueryable<Movie> query = Db.Movies.Where(m => m.Status == effectiveStatus);

        if (genre.HasValue)
        {
            query = query.Where(m => Db.MovieGenres.Any(mg => mg.MovieId == m.MovieId && mg.GenreId == genre.Value));
        }

        query = effectiveStatus == "Sắp chiếu"
            ? query.OrderBy(m => m.ReleaseDate)
            : query.OrderByDescending(m => m.ReleaseDate);

        var movies = await MovieQueryHelper.GetMoviesWithGenresAsync(Db, query);
        var genres = await Db.Genres.OrderBy(g => g.GenreName).ToListAsync();

        var vm = new MoviesListVM
        {
            Movies = movies,
            Genres = genres,
            Status = effectiveStatus,
            GenreId = genre,
            Title = effectiveStatus == "Đang chiếu" ? "Phim đang chiếu" : "Phim sắp chiếu"
        };

        return View(vm);
    }

    // GET /phim/{id}
    [Route("/phim/{id:int}")]
    public async Task<IActionResult> Detail(int id, string? review)
    {
        var movie = await Db.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
        if (movie is null) return NotFound();

        var full = await MovieQueryHelper.AttachGenresAndRatingAsync(Db, movie);

        var now = DateTime.Now.AddHours(-2);
        var showtimes = await Db.Showtimes
            .Include(s => s.Room)
            .Where(s => s.MovieId == movie.MovieId && s.Status != "Đã hủy" && s.StartTime >= now)
            .OrderBy(s => s.StartTime)
            .ToListAsync();

        var byDate = new Dictionary<string, Dictionary<string, List<ShowtimeRow>>>();
        foreach (var st in showtimes)
        {
            var dateKey = st.StartTime.ToString("yyyy-MM-dd");
            if (!byDate.TryGetValue(dateKey, out var rooms))
            {
                rooms = new Dictionary<string, List<ShowtimeRow>>();
                byDate[dateKey] = rooms;
            }
            if (!rooms.TryGetValue(st.Room.RoomName, out var list))
            {
                list = new List<ShowtimeRow>();
                rooms[st.Room.RoomName] = list;
            }
            list.Add(new ShowtimeRow
            {
                ShowtimeId = st.ShowtimeId,
                StartTime = st.StartTime,
                TicketPrice = st.TicketPrice,
                RoomName = st.Room.RoomName
            });
        }

        var reviews = await Db.Reviews
            .Include(r => r.Customer)
            .Where(r => r.MovieId == movie.MovieId && r.Status == "Đã duyệt")
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewRow
            {
                Rating = r.Rating,
                Comment = r.Comment,
                FullName = r.Customer.FullName,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();

        var relatedGenreIds = await Db.MovieGenres.Where(mg => mg.MovieId == movie.MovieId).Select(mg => mg.GenreId).ToListAsync();
        var relatedMovies = await Db.Movies
            .Where(m => m.MovieId != movie.MovieId && Db.MovieGenres.Any(mg => mg.MovieId == m.MovieId && relatedGenreIds.Contains(mg.GenreId)))
            .Distinct()
            .Take(6)
            .ToListAsync();
        var related = new List<MovieCardVM>();
        foreach (var m in relatedMovies)
            related.Add(await MovieQueryHelper.AttachGenresAndRatingAsync(Db, m));

        var omdbInfo = await _omdb.GetByTitleAsync(movie.Title);

        var vm = new MovieDetailVM
        {
            Title = movie.Title,
            Movie = full,
            ByDate = byDate,
            Reviews = reviews,
            Related = related,
            ReviewSubmitted = review == "ok",
            Omdb = omdbInfo
        };

        return View(vm);
    }

    // POST /phim/{id}/review
    [HttpPost]
    [Route("/phim/{id:int}/review")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(int id, int rating, string comment)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect($"/dang-nhap?next=/phim/{id}");

        Db.Reviews.Add(new Review
        {
            MovieId = id,
            CustomerId = customer.CustomerId,
            Rating = rating,
            Comment = comment,
            Status = "Chờ duyệt",
            CreatedAt = DateTime.Now
        });
        await Db.SaveChangesAsync();

        return Redirect($"/phim/{id}?review=ok");
    }
}

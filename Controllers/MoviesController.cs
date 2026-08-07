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
    private readonly TranslationService _translator;

    public MoviesController(ApplicationDbContext db, OmdbService omdb, TranslationService translator) : base(db)
    {
        _omdb = omdb;
        _translator = translator;
    }

    // GET /phim?status=...&genre=...
    [Route("/phim")]
    public async Task<IActionResult> Index(string? status, int? genre)
    {
        var effectiveStatus = string.IsNullOrWhiteSpace(status) ? "Đang chiếu" : status;

        // Chỉ hiển thị phim đã được Admin duyệt (ẩn phim "Chờ duyệt"/"Từ chối" do Nhân viên gửi).
        IQueryable<Movie> query = Db.Movies.Where(m => m.Status == effectiveStatus && m.ApprovalStatus == "Đã duyệt");

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
        // Phim đang "Chờ duyệt"/"Từ chối" (do Nhân viên thêm/sửa, chưa được Admin duyệt)
        // không được phép xem ở trang khách hàng, kể cả khi có link trực tiếp.
        var movie = await Db.Movies.FirstOrDefaultAsync(m => m.MovieId == id && m.ApprovalStatus == "Đã duyệt");
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
            .Where(m => m.MovieId != movie.MovieId && m.ApprovalStatus == "Đã duyệt"
                && Db.MovieGenres.Any(mg => mg.MovieId == m.MovieId && relatedGenreIds.Contains(mg.GenreId)))
            .Distinct()
            .Take(6)
            .ToListAsync();
        var related = new List<MovieCardVM>();
        foreach (var m in relatedMovies)
            related.Add(await MovieQueryHelper.AttachGenresAndRatingAsync(Db, m));

        var omdbInfo = await _omdb.GetByTitleAsync(movie.Title);

        string? translatedPlot = null, translatedGenre = null, translatedAwards = null;
        if (omdbInfo != null)
        {
            translatedPlot = await _translator.ToVietnameseAsync(omdbInfo.Plot);
            translatedGenre = await _translator.ToVietnameseAsync(omdbInfo.Genre);
            translatedAwards = await _translator.ToVietnameseAsync(omdbInfo.Awards);
        }

        var vm = new MovieDetailVM
        {
            Title = movie.Title,
            Movie = full,
            ByDate = byDate,
            Reviews = reviews,
            Related = related,
            ReviewSubmitted = review == "ok",
            Omdb = omdbInfo,
            TranslatedPlot = translatedPlot,
            TranslatedGenre = translatedGenre,
            TranslatedAwards = translatedAwards
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

        // Validate ở server — không tin dữ liệu người dùng gửi lên (điểm đánh giá phải 1–5,
        // nội dung không được rỗng/quá dài, phim phải tồn tại) dù giao diện có ràng buộc sẵn.
        var movieExists = await Db.Movies.AnyAsync(m => m.MovieId == id);
        if (!movieExists) return NotFound();

        if (rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(comment) || comment.Trim().Length > 1000)
        {
            TempData["Error"] = "Đánh giá không hợp lệ: điểm phải từ 1–5 sao và nội dung không được để trống (tối đa 1000 ký tự).";
            return Redirect($"/phim/{id}");
        }

        Db.Reviews.Add(new Review
        {
            MovieId = id,
            CustomerId = customer.CustomerId,
            Rating = rating,
            Comment = comment.Trim(),
            Status = "Chờ duyệt",
            CreatedAt = DateTime.Now
        });
        await Db.SaveChangesAsync();

        return Redirect($"/phim/{id}?review=ok");
    }
}

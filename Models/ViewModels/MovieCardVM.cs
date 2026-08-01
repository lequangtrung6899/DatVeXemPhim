using DatVeXemPhim.Models;

namespace DatVeXemPhim.Models.ViewModels;

// Mirrors the Node app's movieWithGenres() helper: a movie plus computed genre/rating info.
public class MovieCardVM
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string? PosterUrl { get; set; }
    public string? BannerUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public List<string> Genres { get; set; } = new();
    public double? AvgRating { get; set; }
    public int ReviewCount { get; set; }

    public static MovieCardVM FromMovie(Movie m, List<string> genres, double? avgRating, int reviewCount) => new()
    {
        MovieId = m.MovieId,
        Title = m.Title,
        Description = m.Description,
        Duration = m.Duration,
        PosterUrl = m.PosterUrl,
        BannerUrl = m.BannerUrl,
        ReleaseDate = m.ReleaseDate,
        EndDate = m.EndDate,
        Status = m.Status,
        Genres = genres,
        AvgRating = avgRating,
        ReviewCount = reviewCount
    };
}

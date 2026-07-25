using DatVeXemPhim.Models;
using DatVeXemPhim.Services;

namespace DatVeXemPhim.Models.ViewModels;

public class ShowtimeRow
{
    public int ShowtimeId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal TicketPrice { get; set; }
    public string RoomName { get; set; } = string.Empty;
}

public class ReviewRow
{
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class MovieDetailVM
{
    public string Title { get; set; } = string.Empty;
    public MovieCardVM Movie { get; set; } = null!;

    // date (yyyy-MM-dd) -> room name -> showtimes
    public Dictionary<string, Dictionary<string, List<ShowtimeRow>>> ByDate { get; set; } = new();

    public List<ReviewRow> Reviews { get; set; } = new();
    public List<MovieCardVM> Related { get; set; } = new();
    public bool ReviewSubmitted { get; set; }

    // IMDb-style extra info (director, cast, country, IMDb rating, ...) fetched
    // live from OMDb by title. Null if lookup failed or no API key is configured.
    public OmdbInfo? Omdb { get; set; }
}

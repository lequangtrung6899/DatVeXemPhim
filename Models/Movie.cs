using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class Movie
{
    public int MovieId { get; set; }

    [Required, MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Duration { get; set; }

    [MaxLength(500)]
    public string? PosterUrl { get; set; }

    public DateTime ReleaseDate { get; set; }

    public DateTime? EndDate { get; set; }

    // 'Đang chiếu' | 'Sắp chiếu' | 'Ngừng chiếu'
    [Required, MaxLength(50)]
    public string Status { get; set; } = "Sắp chiếu";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class Genre
{
    public int GenreId { get; set; }

    [Required, MaxLength(100)]
    public string GenreName { get; set; } = string.Empty;

    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
}

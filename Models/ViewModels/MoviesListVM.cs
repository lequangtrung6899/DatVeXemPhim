using DatVeXemPhim.Models;

namespace DatVeXemPhim.Models.ViewModels;

public class MoviesListVM
{
    public string Title { get; set; } = string.Empty;
    public List<MovieCardVM> Movies { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public string Status { get; set; } = string.Empty;
    public int? GenreId { get; set; }
}

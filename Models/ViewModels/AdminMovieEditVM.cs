namespace DatVeXemPhim.Models.ViewModels;

public class AdminMovieEditVM
{
    public Movie Movie { get; set; } = null!;
    public List<Genre> AllGenres { get; set; } = new();
    public List<int> SelectedGenreIds { get; set; } = new();
}

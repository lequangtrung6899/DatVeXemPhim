namespace DatVeXemPhim.Models.ViewModels;

public class AdminShowtimeEditVM
{
    public Showtime Showtime { get; set; } = null!;
    public List<Movie> Movies { get; set; } = new();
    public List<Room> Rooms { get; set; } = new();
}

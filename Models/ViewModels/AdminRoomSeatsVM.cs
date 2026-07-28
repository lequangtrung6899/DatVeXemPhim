namespace DatVeXemPhim.Models.ViewModels;

public class AdminRoomSeatsVM
{
    public Room Room { get; set; } = null!;
    public List<Seat> Seats { get; set; } = new();
}

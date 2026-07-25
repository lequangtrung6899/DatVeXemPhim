using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class Room
{
    public int RoomId { get; set; }

    [Required, MaxLength(100)]
    public string RoomName { get; set; } = string.Empty;

    public int TotalSeats { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}

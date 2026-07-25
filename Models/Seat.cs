using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class Seat
{
    public int SeatId { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    [Required, MaxLength(5)]
    public string RowLabel { get; set; } = string.Empty;

    public int ColumnNumber { get; set; }

    // 'Thường' | 'VIP' | 'Đôi'
    [Required, MaxLength(50)]
    public string SeatType { get; set; } = "Thường";
}

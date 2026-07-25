using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class ShowtimeSeat
{
    public int ShowtimeSeatId { get; set; }

    public int ShowtimeId { get; set; }
    public Showtime Showtime { get; set; } = null!;

    public int SeatId { get; set; }
    public Seat Seat { get; set; } = null!;

    // 'Trống' | 'Đang giữ' | 'Đã đặt'
    [Required, MaxLength(50)]
    public string Status { get; set; } = "Trống";

    public DateTime? HoldExpiredAt { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}

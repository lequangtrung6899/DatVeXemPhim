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

    // Session ID của người đang tạm giữ ghế (null khi ghế "Trống" hoặc "Đã đặt").
    // Dùng để phân biệt "tôi đang giữ ghế này" với "người khác đang giữ ghế này".
    [MaxLength(100)]
    public string? HeldBySessionId { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}

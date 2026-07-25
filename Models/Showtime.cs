using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

public class Showtime
{
    public int ShowtimeId { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TicketPrice { get; set; }

    // 'Sắp chiếu' | 'Đang chiếu' | 'Đã hủy' | 'Đã kết thúc'
    [Required, MaxLength(50)]
    public string Status { get; set; } = "Sắp chiếu";

    public ICollection<ShowtimeSeat> ShowtimeSeats { get; set; } = new List<ShowtimeSeat>();
}

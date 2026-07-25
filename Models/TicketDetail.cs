using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

public class TicketDetail
{
    public int TicketDetailId { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public int ShowtimeSeatId { get; set; }
    public ShowtimeSeat ShowtimeSeat { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

// Composite key (TicketId, ComboId) configured in ApplicationDbContext
public class TicketCombo
{
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public int ComboId { get; set; }
    public Combo Combo { get; set; } = null!;

    public int Quantity { get; set; } = 1;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}

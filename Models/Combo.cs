using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

public class Combo
{
    public int ComboId { get; set; }

    [Required, MaxLength(255)]
    public string ComboName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<TicketCombo> TicketCombos { get; set; } = new List<TicketCombo>();
}

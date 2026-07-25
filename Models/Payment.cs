using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

public class Payment
{
    public int PaymentId { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required, MaxLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string PaymentStatus { get; set; } = "Chờ xử lý";

    [MaxLength(100)]
    public string? TransactionCode { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.Now;
}

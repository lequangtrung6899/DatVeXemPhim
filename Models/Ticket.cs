using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

public class Ticket
{
    public int TicketId { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int ShowtimeId { get; set; }
    public Showtime Showtime { get; set; } = null!;

    public int? VoucherId { get; set; }
    public Voucher? Voucher { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    // 'Chờ thanh toán' | 'Đã thanh toán' | 'Đã hủy'
    [Required, MaxLength(50)]
    public string Status { get; set; } = "Chờ thanh toán";

    public DateTime BookingDate { get; set; } = DateTime.Now;
    public DateTime? ConfirmedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? RefundAmount { get; set; }

    public ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    public ICollection<TicketCombo> TicketCombos { get; set; } = new List<TicketCombo>();
}

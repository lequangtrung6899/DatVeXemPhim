namespace DatVeXemPhim.Models.ViewModels;

public class AdminPaymentRow
{
    public int PaymentId { get; set; }
    public int TicketId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string MovieTitle { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string? TransactionCode { get; set; }
    public DateTime PaymentDate { get; set; }
}

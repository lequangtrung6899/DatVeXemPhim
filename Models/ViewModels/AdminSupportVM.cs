namespace DatVeXemPhim.Models.ViewModels;

public class AdminSupportSeatLine
{
    public string RowLabel { get; set; } = string.Empty;
    public int ColumnNumber { get; set; }
    public string SeatType { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class AdminSupportComboLine
{
    public string ComboName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class AdminSupportVM
{
    public int? TicketIdInput { get; set; }
    public string? TransactionCodeInput { get; set; }
    public bool NotFound { get; set; }

    public Ticket? Ticket { get; set; }
    public List<AdminSupportSeatLine> Seats { get; set; } = new();
    public List<AdminSupportComboLine> Combos { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();
}

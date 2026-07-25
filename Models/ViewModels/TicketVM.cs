namespace DatVeXemPhim.Models.ViewModels;

public class TicketSeatLine
{
    public string RowLabel { get; set; } = string.Empty;
    public int ColumnNumber { get; set; }
    public string SeatType { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class TicketComboLine
{
    public string ComboName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class TicketVM
{
    public string Title { get; set; } = string.Empty;
    public int TicketId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }

    public string MovieTitle { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }

    public List<TicketSeatLine> Seats { get; set; } = new();
    public List<TicketComboLine> Combos { get; set; } = new();
}

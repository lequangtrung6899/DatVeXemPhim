namespace DatVeXemPhim.Models.ViewModels;

public class MyTicketRow
{
    public int TicketId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? PosterUrl { get; set; }
    public DateTime StartTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class MyTicketsVM
{
    public string Title { get; set; } = "Vé của tôi";
    public List<MyTicketRow> Tickets { get; set; } = new();
}

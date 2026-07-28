namespace DatVeXemPhim.Models.ViewModels;

public class TopMovieRow
{
    public string Title { get; set; } = string.Empty;
    public int TicketsSold { get; set; }
    public decimal Revenue { get; set; }
}

public class AdminDashboardVM
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalTickets { get; set; }
    public int CancelledTickets { get; set; }
    public int NewCustomers { get; set; }
    public int PendingReviews { get; set; }
    public List<TopMovieRow> TopMovies { get; set; } = new();
}

namespace DatVeXemPhim.Models.ViewModels;

public class AdminReviewRow
{
    public int ReviewId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

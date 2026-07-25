using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class Review
{
    public int ReviewId { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int Rating { get; set; }

    public string? Comment { get; set; }

    // 'Chờ duyệt' | 'Đã duyệt' | 'Từ chối'
    [Required, MaxLength(50)]
    public string Status { get; set; } = "Chờ duyệt";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required, MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    public int LoyaltyPoint { get; set; }

    [Required, MaxLength(50)]
    public string MembershipRank { get; set; } = "Thành viên mới";

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

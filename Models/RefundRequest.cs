using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

// Yêu cầu hoàn tiền cho 1 vé. Tiền CHỈ thực sự được hoàn (Ticket -> "Đã hủy",
// giải phóng ghế, cập nhật Payment) khi yêu cầu đi qua đủ 2 bước duyệt:
// Nhân viên duyệt trước, sau đó Admin duyệt lần cuối (xem Services/RefundService.cs).
public class RefundRequest
{
    public int RefundRequestId { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [MaxLength(500)]
    public string? Reason { get; set; }

    public DateTime RequestedAt { get; set; } = DateTime.Now;

    // 'Chờ nhân viên duyệt' | 'Chờ admin duyệt' | 'Đã hoàn tiền' | 'Từ chối'
    [Required, MaxLength(50)]
    public string Status { get; set; } = "Chờ nhân viên duyệt";

    public int? StaffApprovedBy { get; set; }
    public User? StaffApprover { get; set; }
    public DateTime? StaffApprovedAt { get; set; }

    public int? AdminApprovedBy { get; set; }
    public User? AdminApprover { get; set; }
    public DateTime? AdminApprovedAt { get; set; }

    [MaxLength(500)]
    public string? RejectReason { get; set; }
    public int? RejectedBy { get; set; }
    public User? Rejecter { get; set; }
    public DateTime? RejectedAt { get; set; }
}

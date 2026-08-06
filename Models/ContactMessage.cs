using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

// Ca sử dụng "Hỗ trợ khách hàng" (form Liên hệ ở footer): khách hàng (kể cả chưa đăng
// nhập) gửi câu hỏi/khiếu nại, nhân viên vào trang quản trị để xem và đánh dấu đã xử lý.
// Bảng độc lập, không có khóa ngoại, để tối giản rủi ro khi merge với schema hiện có.
public class ContactMessage
{
    public int ContactMessageId { get; set; }

    [Required, MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [Required, MaxLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsResolved { get; set; } = false;
    public DateTime? ResolvedAt { get; set; }
}

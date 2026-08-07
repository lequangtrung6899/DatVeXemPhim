namespace DatVeXemPhim.Models.ViewModels;

public class AdminRefundRow
{
    public int RefundRequestId { get; set; }
    public int TicketId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string MovieTitle { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Reason { get; set; }
    public DateTime RequestedAt { get; set; }
    public string Status { get; set; } = string.Empty;

    public string? StaffApproverName { get; set; }
    public DateTime? StaffApprovedAt { get; set; }
    public string? AdminApproverName { get; set; }
    public DateTime? AdminApprovedAt { get; set; }
    public string? RejectReason { get; set; }

    // Tài khoản đang đăng nhập có được phép Duyệt/Từ chối dòng này không, tính theo
    // vai trò (Staff chỉ xử lý được "Chờ nhân viên duyệt"; Admin xử lý được cả hai).
    public bool CanAct { get; set; }
}

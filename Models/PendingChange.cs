using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

// Hàng đợi "chờ Admin duyệt" DÙNG CHUNG cho các trang quản lý mà Nhân viên có quyền
// thao tác, nhằm chống lạm quyền: khi Nhân viên (không phải Admin) thêm/sửa/xóa dữ liệu
// ở các trang này, thao tác KHÔNG áp dụng ngay vào dữ liệu thật — được lưu tạm ở đây,
// và chỉ thực sự thay đổi sau khi Admin bấm "Duyệt" (xem AdminApprovalController).
// Admin thao tác thì luôn áp dụng ngay lập tức, không qua hàng đợi này
// (xem AdminBaseController.IsAdminRoleAsync()).
//
// Dùng 1 bảng chung cho mọi loại dữ liệu (thay vì thêm cột ApprovalStatus/HasPendingEdit/
// PendingChangesJson riêng cho từng bảng như đã làm ở Movie) để: (1) không phải ALTER
// nhiều bảng nghiệp vụ đang có dữ liệu thật, và (2) Admin có MỘT màn hình "Chờ duyệt"
// tổng hợp duy nhất thay vì phải kiểm tra rải rác ở từng trang quản lý.
public class PendingChange
{
    public int PendingChangeId { get; set; }

    // "Combo" | "Genre" | "Voucher" | "Room" | "Showtime" | "Payment" | "Customer" | "Ticket"
    // Dùng để AdminApprovalController biết cách deserialize ChangesJson và áp dụng
    // đúng vào bảng tương ứng khi được duyệt.
    [Required, MaxLength(30)]
    public string EntityType { get; set; } = string.Empty;

    // null nếu đây là đề xuất THÊM MỚI (chưa có bản ghi thật để trỏ tới).
    public int? EntityId { get; set; }

    // "Create" | "Update" | "Delete" | "Cancel" (riêng Suất chiếu/Vé dùng "Cancel" cho
    // hành động hủy vì đó không phải xóa bản ghi mà chỉ đổi trạng thái).
    [Required, MaxLength(10)]
    public string ActionType { get; set; } = string.Empty;

    // Dữ liệu đề xuất, JSON hoá theo DTO tương ứng (xem PendingChangeDtos.cs).
    // Để trống với ActionType = "Delete"/"Cancel" đơn giản (không có field nào cần đổi).
    public string? ChangesJson { get; set; }

    // Mô tả ngắn, dễ đọc, hiển thị trực tiếp trong danh sách "Chờ duyệt" của Admin,
    // vd: "Sửa Combo 'Combo lớn': giá 89.000₫ → 79.000₫".
    [Required, MaxLength(500)]
    public string Summary { get; set; } = string.Empty;

    public int SubmittedBy { get; set; }
    public User? SubmittedByUser { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.Now;

    // "Chờ duyệt" | "Đã duyệt" | "Từ chối"
    [Required, MaxLength(20)]
    public string Status { get; set; } = "Chờ duyệt";

    public int? ReviewedBy { get; set; }
    public User? ReviewedByUser { get; set; }
    public DateTime? ReviewedAt { get; set; }

    [MaxLength(500)]
    public string? RejectReason { get; set; }
}

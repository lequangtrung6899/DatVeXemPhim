namespace DatVeXemPhim.Models.ViewModels;

// DTO chứa dữ liệu Nhân viên đề xuất, được JSON hoá vào PendingChange.ChangesJson và
// deserialize lại trong AdminApprovalController khi Admin bấm Duyệt. Mỗi entity type
// (Combo/Genre/Voucher/Room/Showtime/Payment/Customer) có 1 DTO tương ứng ở dưới.
// (Hủy vé không nằm ở đây — xem RefundRequest/Services/RefundService.cs.)

public class ComboChangeDto
{
    public string ComboName { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}

public class GenreChangeDto
{
    public string GenreName { get; set; } = "";
}

public class VoucherChangeDto
{
    public string Code { get; set; } = "";
    public string DiscountType { get; set; } = "";
    public decimal DiscountValue { get; set; }
    public decimal MinOrderAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int UsageLimit { get; set; }
    public bool IsActive { get; set; }
}

public class RoomChangeDto
{
    public string RoomName { get; set; } = "";
    public int TotalSeats { get; set; }
    public bool IsActive { get; set; }
}

public class ShowtimeChangeDto
{
    public int MovieId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal TicketPrice { get; set; }
    public string Status { get; set; } = "";
}

public class PaymentStatusChangeDto
{
    public string PaymentStatus { get; set; } = "";
}

public class CustomerLockChangeDto
{
    public bool IsActive { get; set; }
}

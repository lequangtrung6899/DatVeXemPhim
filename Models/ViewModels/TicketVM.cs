namespace DatVeXemPhim.Models.ViewModels;

public class TicketSeatLine
{
    public string RowLabel { get; set; } = string.Empty;
    public int ColumnNumber { get; set; }
    public string SeatType { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class TicketComboLine
{
    public string ComboName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class TicketVM
{
    public string Title { get; set; } = string.Empty;
    public int TicketId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal? RefundAmount { get; set; }

    public string MovieTitle { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }

    public List<TicketSeatLine> Seats { get; set; } = new();
    public List<TicketComboLine> Combos { get; set; } = new();

    // Vé chỉ được khách hàng tự yêu cầu hoàn tiền khi còn ở trạng thái "Đã thanh toán",
    // suất chiếu chưa bắt đầu trong vòng CancelCutoffHours giờ tới, và chưa có yêu cầu
    // hoàn tiền nào khác đang chờ xử lý (xem TicketController + Services/RefundService.cs).
    public bool CanCancel { get; set; }
    public int CancelCutoffHours { get; set; }
    public bool JustRequestedRefund { get; set; }

    // Trạng thái yêu cầu hoàn tiền gần nhất của vé (nếu có): 'Chờ nhân viên duyệt' |
    // 'Chờ admin duyệt' | 'Đã hoàn tiền' | 'Từ chối'. Null = chưa từng yêu cầu.
    public string? RefundRequestStatus { get; set; }
    public string? RefundRejectReason { get; set; }

    // Ghi chú rõ ràng: cổng thanh toán trong dự án là MÔ PHỎNG, không phải giao dịch thật.
    public string PaymentMethod { get; set; } = "Chuyển khoản ngân hàng (QR VietQR - mô phỏng)";
}

namespace DatVeXemPhim.Models.ViewModels;

public class PaymentVM
{
    public List<CartItemVM> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public string? AppliedVoucherCode { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }

    // Thông tin tài khoản ngân hàng + QR VietQR (mã QR đã nhúng sẵn số tiền,
    // ứng dụng ngân hàng của khách khi quét sẽ TỰ ĐỘNG điền đúng số tiền cần chuyển).
    public string QrImageUrl { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string AccountNo { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string TransferContent { get; set; } = string.Empty;
}

public class PaymentSuccessVM
{
    public List<int> TicketIds { get; set; } = new();
    public decimal Total { get; set; }
}

namespace DatVeXemPhim.Models.ViewModels;

public class CartItemVM
{
    public int TicketId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public string? PosterUrl { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public List<TicketSeatLine> Seats { get; set; } = new();
    public List<TicketComboLine> Combos { get; set; } = new();
    public decimal Subtotal { get; set; }
}

public class VoucherOptionVM
{
    public string Code { get; set; } = string.Empty;
    public string DiscountType { get; set; } = string.Empty; // 'Phần trăm' | 'Tiền mặt'
    public decimal DiscountValue { get; set; }
    public decimal MinOrderAmount { get; set; }
    public DateTime EndDate { get; set; }

    // Giỏ hàng hiện tại có đạt tối thiểu đơn hàng để dùng mã này không.
    public bool Eligible { get; set; }
}

public class CartVM
{
    public List<CartItemVM> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public string? AppliedVoucherCode { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public List<VoucherOptionVM> AvailableVouchers { get; set; } = new();
}

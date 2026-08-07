using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Services;

public class CartPricingResult
{
    public List<Ticket> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public Voucher? AppliedVoucher { get; set; }
    public string? VoucherError { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}

// Dùng chung cho CartController (Giỏ hàng) và PaymentController (Thanh toán) để cả
// 2 trang luôn tính ra đúng một con số — không bị lệch giá giữa lúc xem giỏ hàng
// và lúc thanh toán thật, và để việc áp voucher cập nhật tiền ngay lập tức thay vì
// phải chờ thanh toán xong mới biết (yêu cầu chính của phần giỏ hàng).
public static class CartPricing
{
    // Session key lưu mã voucher khách đang áp dụng cho giỏ hàng hiện tại.
    public const string VoucherSessionKey = "CartVoucherCode";

    public static async Task<List<Ticket>> GetCartItemsAsync(ApplicationDbContext db, int customerId)
    {
        return await db.Tickets
            .Include(t => t.Showtime).ThenInclude(s => s.Movie)
            .Include(t => t.Showtime).ThenInclude(s => s.Room)
            .Include(t => t.TicketDetails).ThenInclude(td => td.ShowtimeSeat).ThenInclude(ss => ss.Seat)
            .Include(t => t.TicketCombos).ThenInclude(tc => tc.Combo)
            .Where(t => t.CustomerId == customerId && t.Status == "Chờ thanh toán")
            .OrderBy(t => t.BookingDate)
            .ToListAsync();
    }

    // Vé trong giỏ hàng bị bỏ quên quá lâu sẽ tự động bị hủy để nhả ghế lại cho
    // người khác đặt (giỏ hàng không có cổng thanh toán thật giữ chỗ vô thời hạn).
    public static async Task ExpireStaleCartAsync(ApplicationDbContext db, int customerId, int expireMinutes = 20)
    {
        var cutoff = DateTime.Now.AddMinutes(-expireMinutes);
        var stale = await db.Tickets
            .Where(t => t.CustomerId == customerId && t.Status == "Chờ thanh toán" && t.BookingDate < cutoff)
            .ToListAsync();
        if (stale.Count == 0) return;

        foreach (var ticket in stale)
        {
            await RemoveCartItemAsync(db, ticket);
        }
        await db.SaveChangesAsync();
    }

    // Xóa hẳn 1 vé còn trong giỏ hàng (chưa thanh toán nên không cần giữ lịch sử)
    // và giải phóng ghế đã giữ cho vé đó. Gọi SaveChangesAsync ở ngoài sau khi xong.
    public static async Task RemoveCartItemAsync(ApplicationDbContext db, Ticket ticket)
    {
        var seatIds = await db.TicketDetails.Where(td => td.TicketId == ticket.TicketId)
            .Select(td => td.ShowtimeSeatId).ToListAsync();
        var seats = await db.ShowtimeSeats.Where(s => seatIds.Contains(s.ShowtimeSeatId)).ToListAsync();
        foreach (var s in seats)
        {
            s.Status = "Trống";
            s.HeldBySessionId = null;
            s.HoldExpiredAt = null;
        }

        var details = await db.TicketDetails.Where(td => td.TicketId == ticket.TicketId).ToListAsync();
        db.TicketDetails.RemoveRange(details);
        var combos = await db.TicketCombos.Where(tc => tc.TicketId == ticket.TicketId).ToListAsync();
        db.TicketCombos.RemoveRange(combos);
        db.Tickets.Remove(ticket);
    }

    public static async Task<CartPricingResult> ComputeAsync(ApplicationDbContext db, List<Ticket> items, string? voucherCode)
    {
        var result = new CartPricingResult { Items = items };
        result.Subtotal = items.Sum(t => t.TotalAmount);
        result.Total = result.Subtotal;

        var code = (voucherCode ?? string.Empty).Trim().ToUpperInvariant();
        if (string.IsNullOrEmpty(code)) return result;

        var today = DateTime.Now.Date;
        var voucher = await db.Vouchers.FirstOrDefaultAsync(v => v.Code == code);

        if (voucher is null) { result.VoucherError = "Mã giảm giá không tồn tại."; return result; }
        if (!voucher.IsActive) { result.VoucherError = "Mã giảm giá này đã bị vô hiệu hóa."; return result; }
        if (today < voucher.StartDate.Date || today > voucher.EndDate.Date) { result.VoucherError = "Mã giảm giá đã hết hạn hoặc chưa đến ngày áp dụng."; return result; }
        if (voucher.UsedCount >= voucher.UsageLimit) { result.VoucherError = "Mã giảm giá đã hết lượt sử dụng."; return result; }
        if (result.Subtotal < voucher.MinOrderAmount)
        {
            result.VoucherError = $"Đơn hàng cần tối thiểu {FormatVND(voucher.MinOrderAmount)} để áp dụng mã này.";
            return result;
        }

        result.AppliedVoucher = voucher;
        result.Discount = voucher.DiscountType == "Phần trăm"
            ? result.Subtotal * (voucher.DiscountValue / 100)
            : voucher.DiscountValue;
        result.Discount = Math.Min(result.Discount, result.Subtotal);
        result.Total = result.Subtotal - result.Discount;
        return result;
    }

    public static string FormatVND(decimal n) => Math.Round(n).ToString("N0", new System.Globalization.CultureInfo("vi-VN")) + "\u20ab";
}

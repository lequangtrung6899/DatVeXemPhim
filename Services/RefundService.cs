using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Services;

// Trung tâm xử lý luồng duyệt hoàn tiền 2 cấp: Nhân viên duyệt trước, rồi Admin
// duyệt lần cuối thì tiền mới thực sự được hoàn (Ticket -> "Đã hủy", giải phóng
// ghế, cập nhật Payment, trừ điểm tích lũy...). Tài khoản vai trò "Staff" chỉ được
// thực hiện bước duyệt đầu; tài khoản vai trò "Admin" có thể tự hoàn tất cả 2 bước
// trong 1 lần bấm (Admin có đủ thẩm quyền của cả Nhân viên lẫn Admin), nhưng một
// yêu cầu do Staff duyệt bước 1 thì bắt buộc phải có Admin duyệt bước 2 mới xong.
public class RefundService
{
    private readonly ApplicationDbContext _db;

    public RefundService(ApplicationDbContext db)
    {
        _db = db;
    }

    // Khách hàng tự gửi yêu cầu hoàn tiền/hủy vé từ trang "Vé của tôi".
    public async Task<(bool Ok, string Message)> CreateRequestAsync(Ticket ticket, int customerId, string? reason)
    {
        var hasActiveRequest = await _db.RefundRequests.AnyAsync(r =>
            r.TicketId == ticket.TicketId && (r.Status == "Chờ nhân viên duyệt" || r.Status == "Chờ admin duyệt"));
        if (hasActiveRequest)
            return (false, "Vé này đã có một yêu cầu hoàn tiền đang chờ xử lý.");

        if (ticket.Status != "Đã thanh toán")
            return (false, "Chỉ có thể yêu cầu hoàn tiền cho vé đang ở trạng thái \"Đã thanh toán\".");

        _db.RefundRequests.Add(new RefundRequest
        {
            TicketId = ticket.TicketId,
            CustomerId = customerId,
            Amount = ticket.TotalAmount,
            Reason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim(),
            Status = "Chờ nhân viên duyệt"
        });
        ticket.Status = "Đang chờ hoàn tiền";

        await _db.SaveChangesAsync();
        return (true, "Đã gửi yêu cầu hoàn tiền. Vé sẽ chỉ thực sự được hủy và hoàn tiền sau khi cả nhân viên và quản trị viên xét duyệt.");
    }

    // Nhân viên hỗ trợ (Ca sử dụng "Hỗ trợ khách hàng") chủ động hủy vé giúp khách:
    // tự tạo yêu cầu (nếu chưa có) rồi tự động hoàn tất luôn bước duyệt Nhân viên,
    // hồ sơ sẽ chuyển sang chờ Admin duyệt lần cuối trước khi tiền thực sự được hoàn.
    public async Task<(bool Ok, string Message)> StaffInitiateAsync(Ticket ticket, decimal? amount, User staff)
    {
        if (ticket.Status == "Đã hủy")
            return (false, "Vé này đã được hủy trước đó.");

        var existing = await _db.RefundRequests.FirstOrDefaultAsync(r =>
            r.TicketId == ticket.TicketId && (r.Status == "Chờ nhân viên duyệt" || r.Status == "Chờ admin duyệt"));

        int refundRequestId;
        if (existing != null)
        {
            refundRequestId = existing.RefundRequestId;
        }
        else
        {
            var req = new RefundRequest
            {
                TicketId = ticket.TicketId,
                CustomerId = ticket.CustomerId,
                Amount = amount ?? ticket.TotalAmount,
                Reason = "Nhân viên hỗ trợ hủy vé giúp khách hàng qua màn hình \"Hỗ trợ khách hàng\".",
                Status = "Chờ nhân viên duyệt"
            };
            _db.RefundRequests.Add(req);
            ticket.Status = "Đang chờ hoàn tiền";
            await _db.SaveChangesAsync();
            refundRequestId = req.RefundRequestId;
        }

        return await ApproveAsync(refundRequestId, staff);
    }

    // Duyệt một yêu cầu hoàn tiền. Trả về (Ok=false) nếu tài khoản hiện tại không
    // có thẩm quyền cho bước đang chờ (vd. Staff cố duyệt bước Admin).
    public async Task<(bool Ok, string Message)> ApproveAsync(int refundRequestId, User staff)
    {
        var req = await _db.RefundRequests.Include(r => r.Ticket)
            .FirstOrDefaultAsync(r => r.RefundRequestId == refundRequestId);
        if (req is null) return (false, "Không tìm thấy yêu cầu hoàn tiền.");

        var isAdmin = staff.Role?.RoleName == "Admin";

        if (req.Status == "Chờ nhân viên duyệt")
        {
            req.StaffApprovedBy = staff.UserId;
            req.StaffApprovedAt = DateTime.Now;
            req.Status = "Chờ admin duyệt";
            await _db.SaveChangesAsync();

            if (!isAdmin)
                return (true, "Đã duyệt bước Nhân viên. Yêu cầu đang chờ Admin duyệt lần cuối trước khi tiền được hoàn.");

            // Tài khoản đang thao tác là Admin -> có đủ thẩm quyền, thực hiện luôn bước duyệt Admin bên dưới.
        }
        else if (req.Status != "Chờ admin duyệt")
        {
            return (false, "Yêu cầu này không còn ở trạng thái chờ duyệt.");
        }

        // ---- Bước duyệt Admin: chỉ Admin mới được thực hiện, và đây là bước hoàn tất hoàn tiền ----
        if (!isAdmin)
            return (false, "Yêu cầu đang chờ Admin duyệt lần cuối — tài khoản Nhân viên không có quyền thực hiện bước này.");

        req.AdminApprovedBy = staff.UserId;
        req.AdminApprovedAt = DateTime.Now;
        req.Status = "Đã hoàn tiền";

        var ticket = req.Ticket;
        ticket.Status = "Đã hủy";
        ticket.CancelledAt = DateTime.Now;
        ticket.RefundAmount = req.Amount;

        // Giải phóng ghế để người khác có thể đặt lại.
        var seatIds = await _db.TicketDetails.Where(td => td.TicketId == ticket.TicketId)
            .Select(td => td.ShowtimeSeatId).ToListAsync();
        var seats = await _db.ShowtimeSeats.Where(s => seatIds.Contains(s.ShowtimeSeatId)).ToListAsync();
        foreach (var s in seats)
        {
            s.Status = "Trống";
            s.HeldBySessionId = null;
            s.HoldExpiredAt = null;
        }

        // Đánh dấu hoàn tiền trên (các) lần thanh toán của vé — mô phỏng, không gọi cổng thanh toán thật.
        var payments = await _db.Payments.Where(p => p.TicketId == ticket.TicketId).ToListAsync();
        foreach (var p in payments) p.PaymentStatus = "Đã hoàn tiền";

        // Trả lại lượt dùng voucher (nếu vé có áp dụng) để khách có thể dùng lại mã cho lần đặt khác.
        if (ticket.VoucherId != null)
        {
            var voucher = await _db.Vouchers.FindAsync(ticket.VoucherId.Value);
            if (voucher != null && voucher.UsedCount > 0) voucher.UsedCount -= 1;
        }

        // Thu hồi điểm tích lũy đã cộng khi đặt vé này, tính lại hạng thành viên tương ứng.
        var customer = await _db.Customers.FindAsync(ticket.CustomerId);
        if (customer != null)
        {
            var earnedPoints = (int)Math.Floor(ticket.TotalAmount / 10000);
            customer.LoyaltyPoint = Math.Max(0, customer.LoyaltyPoint - earnedPoints);
            MembershipRankHelper.RecalculateRank(customer);
        }

        await _db.SaveChangesAsync();
        return (true, "Đã duyệt bước Admin — hoàn tất hoàn tiền và hủy vé cho khách hàng.");
    }

    public async Task<(bool Ok, string Message)> RejectAsync(int refundRequestId, User staff, string? reason)
    {
        var req = await _db.RefundRequests.Include(r => r.Ticket)
            .FirstOrDefaultAsync(r => r.RefundRequestId == refundRequestId);
        if (req is null) return (false, "Không tìm thấy yêu cầu hoàn tiền.");

        var isAdmin = staff.Role?.RoleName == "Admin";
        if (req.Status == "Chờ admin duyệt" && !isAdmin)
            return (false, "Yêu cầu đã qua bước duyệt Nhân viên — chỉ Admin mới có thể từ chối ở bước này.");
        if (req.Status != "Chờ nhân viên duyệt" && req.Status != "Chờ admin duyệt")
            return (false, "Yêu cầu này không còn ở trạng thái chờ duyệt.");

        req.Status = "Từ chối";
        req.RejectedBy = staff.UserId;
        req.RejectedAt = DateTime.Now;
        req.RejectReason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim();

        if (req.Ticket.Status == "Đang chờ hoàn tiền")
            req.Ticket.Status = "Đã thanh toán";

        await _db.SaveChangesAsync();
        return (true, "Đã từ chối yêu cầu hoàn tiền. Vé trở lại trạng thái \"Đã thanh toán\".");
    }
}

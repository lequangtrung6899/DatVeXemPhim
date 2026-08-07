using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

// Ca sử dụng "Thanh toán": kết hợp tài khoản ngân hàng + QR (chuẩn VietQR) — mã QR
// được sinh kèm sẵn SỐ TIỀN cần thanh toán, nên khi khách quét bằng app ngân hàng,
// số tiền được điền tự động, khách không phải tự gõ lại.
//
// LƯU Ý: đây vẫn là mô phỏng cho mục đích học tập/báo cáo — ảnh QR lấy từ dịch vụ
// công khai img.vietqr.io (không cần khóa API), nhưng KHÔNG có webhook đối soát
// giao dịch ngân hàng thật nào được gọi. Khách bấm "Tôi đã chuyển khoản" để xác
// nhận, tương tự cách hệ thống demo trước đây đánh dấu "Đã thanh toán" ngay lập tức.
public class PaymentController : BaseController
{
    private readonly IConfiguration _config;

    public PaymentController(ApplicationDbContext db, IConfiguration config) : base(db)
    {
        _config = config;
    }

    private static CartItemVM ToItemVM(Ticket t) => new()
    {
        TicketId = t.TicketId,
        MovieTitle = t.Showtime.Movie.Title,
        PosterUrl = t.Showtime.Movie.PosterUrl,
        RoomName = t.Showtime.Room.RoomName,
        StartTime = t.Showtime.StartTime,
        Seats = t.TicketDetails.Select(td => new TicketSeatLine
        {
            RowLabel = td.ShowtimeSeat.Seat.RowLabel,
            ColumnNumber = td.ShowtimeSeat.Seat.ColumnNumber,
            SeatType = td.ShowtimeSeat.Seat.SeatType,
            Price = td.Price
        }).OrderBy(s => s.RowLabel).ThenBy(s => s.ColumnNumber).ToList(),
        Combos = t.TicketCombos.Select(tc => new TicketComboLine
        {
            ComboName = tc.Combo.ComboName,
            Quantity = tc.Quantity,
            Price = tc.Price
        }).ToList(),
        Subtotal = t.TotalAmount
    };

    // GET /thanh-toan
    [HttpGet, Route("/thanh-toan")]
    public async Task<IActionResult> Index()
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap?next=/thanh-toan");

        var items = await CartPricing.GetCartItemsAsync(Db, customer.CustomerId);
        if (items.Count == 0)
        {
            TempData["Error"] = "Giỏ hàng của bạn đang trống.";
            return Redirect("/gio-hang");
        }

        var voucherCode = HttpContext.Session.GetString(CartPricing.VoucherSessionKey);
        var pricing = await CartPricing.ComputeAsync(Db, items, voucherCode);

        var content = "DatVe" + items.First().TicketId + (items.Count > 1 ? $"+{items.Count - 1}" : "");

        var bankBin = _config["Bank:BankBin"] ?? "970422";
        var accountNo = _config["Bank:AccountNo"] ?? "0123456789";
        var accountName = _config["Bank:AccountName"] ?? "PHIMHAY";
        var bankName = _config["Bank:BankName"] ?? "Ngân hàng";

        var amount = (long)Math.Round(pricing.Total, MidpointRounding.AwayFromZero);
        var qrUrl = $"https://img.vietqr.io/image/{bankBin}-{accountNo}-compact2.png" +
                    $"?amount={amount}&addInfo={Uri.EscapeDataString(content)}&accountName={Uri.EscapeDataString(accountName)}";

        var vm = new PaymentVM
        {
            Items = items.Select(ToItemVM).ToList(),
            Subtotal = pricing.Subtotal,
            AppliedVoucherCode = pricing.AppliedVoucher?.Code,
            Discount = pricing.Discount,
            Total = pricing.Total,
            QrImageUrl = qrUrl,
            BankName = bankName,
            AccountNo = accountNo,
            AccountName = accountName,
            TransferContent = content
        };
        return View(vm);
    }

    // POST /thanh-toan/xac-nhan — khách xác nhận đã chuyển khoản.
    [HttpPost, Route("/thanh-toan/xac-nhan")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm()
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap?next=/thanh-toan");

        var items = await CartPricing.GetCartItemsAsync(Db, customer.CustomerId);
        if (items.Count == 0)
        {
            TempData["Error"] = "Giỏ hàng của bạn đang trống.";
            return Redirect("/gio-hang");
        }

        var voucherCode = HttpContext.Session.GetString(CartPricing.VoucherSessionKey);

        await using var tx = await Db.Database.BeginTransactionAsync();
        try
        {
            // Tính lại lần cuối trong transaction để tránh trường hợp voucher vừa hết
            // lượt dùng bởi người khác trong lúc khách đang xem trang thanh toán.
            var pricing = await CartPricing.ComputeAsync(Db, items, voucherCode);

            // Mã đơn hàng demo — bắt đầu bằng "DH" để dễ phân biệt với dữ liệu thật khi đọc.
            var orderCode = "DH" + DateTime.Now.Ticks.ToString()[^8..];
            var ticketIds = new List<int>();

            foreach (var item in items)
            {
                var share = pricing.Subtotal > 0
                    ? Math.Round(item.TotalAmount / pricing.Subtotal * pricing.Discount, 2)
                    : 0;
                var finalAmount = Math.Max(0, item.TotalAmount - share);

                item.TotalAmount = finalAmount;
                item.Status = "Đã thanh toán";
                item.ConfirmedAt = DateTime.Now;
                item.VoucherId = pricing.AppliedVoucher?.VoucherId;

                Db.Payments.Add(new Payment
                {
                    TicketId = item.TicketId,
                    Amount = finalAmount,
                    PaymentMethod = "Chuyển khoản ngân hàng (QR VietQR - mô phỏng)",
                    PaymentStatus = "Thành công",
                    TransactionCode = orderCode + "-" + item.TicketId,
                    PaymentDate = DateTime.Now
                });

                customer.LoyaltyPoint += (int)Math.Floor(finalAmount / 10000);
                ticketIds.Add(item.TicketId);
            }

            if (pricing.AppliedVoucher != null)
            {
                pricing.AppliedVoucher.UsedCount += 1;
            }

            MembershipRankHelper.RecalculateRank(customer);

            await Db.SaveChangesAsync();
            await tx.CommitAsync();

            HttpContext.Session.Remove(CartPricing.VoucherSessionKey);

            return Redirect("/thanh-toan/thanh-cong?ids=" + string.Join(",", ticketIds));
        }
        catch
        {
            await tx.RollbackAsync();
            TempData["Error"] = "Có lỗi xảy ra khi xác nhận thanh toán, vui lòng thử lại.";
            return Redirect("/thanh-toan");
        }
    }

    // GET /thanh-toan/thanh-cong?ids=1,2,3
    [HttpGet, Route("/thanh-toan/thanh-cong")]
    public async Task<IActionResult> Success(string? ids)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap");

        var ticketIds = (ids ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.TryParse(s, out var id) ? id : (int?)null)
            .Where(id => id != null)
            .Select(id => id!.Value)
            .ToList();

        var tickets = await Db.Tickets
            .Where(t => ticketIds.Contains(t.TicketId) && t.CustomerId == customer.CustomerId && t.Status == "Đã thanh toán")
            .ToListAsync();

        var vm = new PaymentSuccessVM
        {
            TicketIds = tickets.Select(t => t.TicketId).OrderBy(id => id).ToList(),
            Total = tickets.Sum(t => t.TotalAmount)
        };
        return View(vm);
    }
}

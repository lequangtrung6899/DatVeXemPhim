using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

// Ca sử dụng "Giỏ hàng": khách đặt vé xong sẽ vào đây trước khi thanh toán thật.
// Có thể nhập/​chọn voucher — tiền được tính lại NGAY LẬP TỨC bằng AJAX, không cần
// đợi thanh toán xong mới biết như luồng trước đây.
public class CartController : BaseController
{
    public CartController(ApplicationDbContext db) : base(db) { }

    private static CartItemVM ToItemVM(DatVeXemPhim.Models.Ticket t) => new()
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

    // GET /gio-hang
    [HttpGet, Route("/gio-hang")]
    public async Task<IActionResult> Index()
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap?next=/gio-hang");

        await CartPricing.ExpireStaleCartAsync(Db, customer.CustomerId);

        var items = await CartPricing.GetCartItemsAsync(Db, customer.CustomerId);
        var voucherCode = HttpContext.Session.GetString(CartPricing.VoucherSessionKey);
        var pricing = await CartPricing.ComputeAsync(Db, items, voucherCode);

        // Voucher đã lưu trong session không còn hợp lệ nữa (vd. giỏ hàng thay đổi
        // khiến không còn đạt tối thiểu đơn hàng) -> tự động bỏ áp dụng.
        if (!string.IsNullOrEmpty(voucherCode) && pricing.AppliedVoucher is null)
        {
            HttpContext.Session.Remove(CartPricing.VoucherSessionKey);
        }

        var today = DateTime.Now.Date;
        var availableVouchers = await Db.Vouchers
            .Where(v => v.IsActive && today >= v.StartDate.Date && today <= v.EndDate.Date && v.UsedCount < v.UsageLimit)
            .OrderBy(v => v.MinOrderAmount)
            .Select(v => new VoucherOptionVM
            {
                Code = v.Code,
                DiscountType = v.DiscountType,
                DiscountValue = v.DiscountValue,
                MinOrderAmount = v.MinOrderAmount,
                EndDate = v.EndDate,
                Eligible = pricing.Subtotal >= v.MinOrderAmount
            })
            .ToListAsync();

        var vm = new CartVM
        {
            Items = items.Select(ToItemVM).ToList(),
            Subtotal = pricing.Subtotal,
            AppliedVoucherCode = pricing.AppliedVoucher?.Code,
            Discount = pricing.Discount,
            Total = pricing.Total,
            AvailableVouchers = availableVouchers
        };
        return View(vm);
    }

    // POST /gio-hang/xoa/{ticketId} — bỏ 1 vé khỏi giỏ hàng, giải phóng ghế đã giữ.
    [HttpPost, Route("/gio-hang/xoa/{ticketId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int ticketId)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap?next=/gio-hang");

        var ticket = await Db.Tickets.FirstOrDefaultAsync(t =>
            t.TicketId == ticketId && t.CustomerId == customer.CustomerId && t.Status == "Chờ thanh toán");
        if (ticket != null)
        {
            await CartPricing.RemoveCartItemAsync(Db, ticket);
            await Db.SaveChangesAsync();
        }
        return Redirect("/gio-hang");
    }

    public class VoucherRequest
    {
        public string? Code { get; set; }
    }

    // POST /gio-hang/ap-dung-voucher (AJAX) — nhập mã HOẶC chọn từ danh sách đều gọi endpoint này.
    [HttpPost, Route("/gio-hang/ap-dung-voucher")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApplyVoucher([FromBody] VoucherRequest req)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Unauthorized();

        var items = await CartPricing.GetCartItemsAsync(Db, customer.CustomerId);
        var pricing = await CartPricing.ComputeAsync(Db, items, req.Code);

        if (pricing.AppliedVoucher is null)
        {
            return Json(new
            {
                ok = false,
                message = pricing.VoucherError ?? "Mã giảm giá không hợp lệ.",
                subtotal = pricing.Subtotal,
                subtotalText = CartPricing.FormatVND(pricing.Subtotal),
                discount = 0,
                discountText = CartPricing.FormatVND(0),
                total = pricing.Subtotal,
                totalText = CartPricing.FormatVND(pricing.Subtotal)
            });
        }

        HttpContext.Session.SetString(CartPricing.VoucherSessionKey, pricing.AppliedVoucher.Code);
        return Json(new
        {
            ok = true,
            message = $"Đã áp dụng mã \"{pricing.AppliedVoucher.Code}\".",
            code = pricing.AppliedVoucher.Code,
            subtotal = pricing.Subtotal,
            subtotalText = CartPricing.FormatVND(pricing.Subtotal),
            discount = pricing.Discount,
            discountText = "-" + CartPricing.FormatVND(pricing.Discount),
            total = pricing.Total,
            totalText = CartPricing.FormatVND(pricing.Total)
        });
    }

    // POST /gio-hang/bo-voucher (AJAX)
    [HttpPost, Route("/gio-hang/bo-voucher")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveVoucher()
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Unauthorized();

        HttpContext.Session.Remove(CartPricing.VoucherSessionKey);
        var items = await CartPricing.GetCartItemsAsync(Db, customer.CustomerId);
        var pricing = await CartPricing.ComputeAsync(Db, items, null);

        return Json(new
        {
            ok = true,
            subtotal = pricing.Subtotal,
            subtotalText = CartPricing.FormatVND(pricing.Subtotal),
            discount = 0,
            discountText = CartPricing.FormatVND(0),
            total = pricing.Subtotal,
            totalText = CartPricing.FormatVND(pricing.Subtotal)
        });
    }
}

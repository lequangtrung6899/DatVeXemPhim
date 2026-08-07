using System.Text.Json;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Duyệt thay đổi của Nhân viên" — màn hình TỔNG HỢP, chỉ Admin mới vào được,
// liệt kê mọi đề xuất thêm/sửa/xóa mà Nhân viên gửi lên từ các trang quản lý khác
// (Combo, Thể loại, Voucher, Phòng chiếu, Suất chiếu, Thanh toán, Khách hàng, Hủy vé hỗ
// trợ khách hàng — xem PendingChange.cs). Nhân viên thao tác ở các trang đó KHÔNG áp
// dụng ngay vào dữ liệu thật; Admin duyệt ở đây mới thực sự ghi vào dữ liệu. Cùng tinh
// thần chống lạm quyền như "Quản lý phim" (AdminMovieController) nhưng dùng chung MỘT
// hàng đợi cho mọi loại dữ liệu thay vì thêm cột riêng cho từng bảng.
public class AdminApprovalController : AdminBaseController
{
    private const int PageSize = 10;

    public AdminApprovalController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/cho-duyet")]
    public async Task<IActionResult> Index(string? status, int page = 1)
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền truy cập trang duyệt thay đổi.";
            return Redirect("/quan-tri");
        }

        var effectiveStatus = string.IsNullOrWhiteSpace(status) ? "Chờ duyệt" : status;
        var query = Db.PendingChanges.Include(pc => pc.SubmittedByUser).Include(pc => pc.ReviewedByUser).AsQueryable();
        if (effectiveStatus != "Tất cả")
        {
            query = query.Where(pc => pc.Status == effectiveStatus);
        }

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var items = await query.OrderByDescending(pc => pc.SubmittedAt)
            .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Status = effectiveStatus;
        ViewBag.Pagination = new PaginationVM
        {
            Page = page,
            TotalPages = totalPages,
            BaseUrl = $"/quan-tri/cho-duyet?status={Uri.EscapeDataString(effectiveStatus)}&"
        };
        return View(items);
    }

    [HttpPost, Route("/quan-tri/cho-duyet/{id:int}/duyet")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền duyệt.";
            return Redirect("/quan-tri/cho-duyet");
        }

        var pc = await Db.PendingChanges.FindAsync(id);
        if (pc is null) return NotFound();
        if (pc.Status != "Chờ duyệt")
        {
            TempData["Error"] = "Đề xuất này đã được xử lý trước đó.";
            return Redirect("/quan-tri/cho-duyet");
        }

        try
        {
            await ApplyChangeAsync(pc);
        }
        catch (InvalidOperationException ex)
        {
            // Dữ liệu liên quan đã đổi từ lúc Nhân viên gửi đề xuất (vd: voucher đã được
            // dùng, phòng đã có suất chiếu khác chồng giờ...) — báo lỗi rõ ràng thay vì
            // âm thầm ghi đè, KHÔNG đánh dấu "Đã duyệt" để Admin có thể thử lại sau.
            TempData["Error"] = $"Không thể áp dụng đề xuất: {ex.Message}";
            return Redirect("/quan-tri/cho-duyet");
        }

        var staff = await GetCurrentStaffAsync();
        pc.Status = "Đã duyệt";
        pc.ReviewedBy = staff?.UserId;
        pc.ReviewedAt = DateTime.Now;
        await Db.SaveChangesAsync();

        TempData["Success"] = "Đã duyệt và áp dụng thay đổi.";
        return Redirect("/quan-tri/cho-duyet");
    }

    [HttpPost, Route("/quan-tri/cho-duyet/{id:int}/tu-choi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int id, string? reason)
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền từ chối.";
            return Redirect("/quan-tri/cho-duyet");
        }

        var pc = await Db.PendingChanges.FindAsync(id);
        if (pc is null) return NotFound();
        if (pc.Status != "Chờ duyệt")
        {
            TempData["Error"] = "Đề xuất này đã được xử lý trước đó.";
            return Redirect("/quan-tri/cho-duyet");
        }

        var staff = await GetCurrentStaffAsync();
        pc.Status = "Từ chối";
        pc.ReviewedBy = staff?.UserId;
        pc.ReviewedAt = DateTime.Now;
        pc.RejectReason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim();
        await Db.SaveChangesAsync();

        TempData["Success"] = "Đã từ chối đề xuất.";
        return Redirect("/quan-tri/cho-duyet");
    }

    // ---- Áp dụng 1 đề xuất đã duyệt vào dữ liệu thật ----
    // Mỗi hàm Apply* dưới đây CỐ Ý lặp lại các quy tắc kiểm tra (trùng mã, đang được dùng,
    // chồng giờ...) giống hệt controller gốc, vì dữ liệu có thể đã thay đổi trong khoảng
    // thời gian chờ duyệt (vd: Nhân viên A đề xuất xóa combo, nhưng combo đó vừa được đặt
    // mua trước khi Admin kịp duyệt) — không thể tin tưởng đề xuất là still-valid.

    private async Task ApplyChangeAsync(PendingChange pc)
    {
        switch (pc.EntityType)
        {
            case "Combo": await ApplyComboAsync(pc); break;
            case "Genre": await ApplyGenreAsync(pc); break;
            case "Voucher": await ApplyVoucherAsync(pc); break;
            case "Room": await ApplyRoomAsync(pc); break;
            case "Showtime": await ApplyShowtimeAsync(pc); break;
            case "Payment": await ApplyPaymentAsync(pc); break;
            case "Customer": await ApplyCustomerAsync(pc); break;
            // Lưu ý: Hủy vé / hoàn tiền KHÔNG đi qua hàng đợi PendingChange này — vé có
            // luồng duyệt 2 cấp chuyên biệt của riêng nó (xem RefundRequest + Services/RefundService.cs,
            // dùng bởi TicketController và AdminSupportController).
            default: throw new InvalidOperationException($"Không hỗ trợ loại dữ liệu '{pc.EntityType}'.");
        }
    }

    private async Task ApplyComboAsync(PendingChange pc)
    {
        if (pc.ActionType == "Create")
        {
            var dto = JsonSerializer.Deserialize<ComboChangeDto>(pc.ChangesJson!)!;
            Db.Combos.Add(new Combo { ComboName = dto.ComboName, Description = dto.Description, Price = dto.Price, IsActive = dto.IsActive });
        }
        else if (pc.ActionType == "Update")
        {
            var combo = await Db.Combos.FindAsync(pc.EntityId);
            if (combo is null) throw new InvalidOperationException("Combo không còn tồn tại (có thể đã bị xóa).");
            var dto = JsonSerializer.Deserialize<ComboChangeDto>(pc.ChangesJson!)!;
            combo.ComboName = dto.ComboName;
            combo.Description = dto.Description;
            combo.Price = dto.Price;
            combo.IsActive = dto.IsActive;
        }
        else if (pc.ActionType == "Delete")
        {
            var combo = await Db.Combos.FindAsync(pc.EntityId);
            if (combo is null) return; // đã bị xóa từ trước, coi như đề xuất đã hoàn tất
            var inUse = await Db.TicketCombos.AnyAsync(tc => tc.ComboId == combo.ComboId);
            if (inUse) throw new InvalidOperationException("Combo đã từng được đặt mua, không thể xóa.");
            Db.Combos.Remove(combo);
        }
        await Db.SaveChangesAsync();
    }

    private async Task ApplyGenreAsync(PendingChange pc)
    {
        if (pc.ActionType == "Create")
        {
            var dto = JsonSerializer.Deserialize<GenreChangeDto>(pc.ChangesJson!)!;
            Db.Genres.Add(new Genre { GenreName = dto.GenreName });
        }
        else if (pc.ActionType == "Update")
        {
            var genre = await Db.Genres.FindAsync(pc.EntityId);
            if (genre is null) throw new InvalidOperationException("Thể loại không còn tồn tại.");
            var dto = JsonSerializer.Deserialize<GenreChangeDto>(pc.ChangesJson!)!;
            genre.GenreName = dto.GenreName;
        }
        else if (pc.ActionType == "Delete")
        {
            var genre = await Db.Genres.FindAsync(pc.EntityId);
            if (genre is null) return;
            var inUse = await Db.MovieGenres.AnyAsync(mg => mg.GenreId == genre.GenreId);
            if (inUse) throw new InvalidOperationException("Thể loại đang được gán cho ít nhất một phim, không thể xóa.");
            Db.Genres.Remove(genre);
        }
        await Db.SaveChangesAsync();
    }

    private async Task ApplyVoucherAsync(PendingChange pc)
    {
        if (pc.ActionType == "Create")
        {
            var dto = JsonSerializer.Deserialize<VoucherChangeDto>(pc.ChangesJson!)!;
            var dup = await Db.Vouchers.AnyAsync(v => v.Code == dto.Code);
            if (dup) throw new InvalidOperationException($"Mã voucher '{dto.Code}' đã tồn tại.");
            Db.Vouchers.Add(new Voucher
            {
                Code = dto.Code,
                DiscountType = dto.DiscountType,
                DiscountValue = dto.DiscountValue,
                MinOrderAmount = dto.MinOrderAmount,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                UsageLimit = dto.UsageLimit,
                UsedCount = 0,
                IsActive = dto.IsActive
            });
        }
        else if (pc.ActionType == "Update")
        {
            var voucher = await Db.Vouchers.FindAsync(pc.EntityId);
            if (voucher is null) throw new InvalidOperationException("Voucher không còn tồn tại.");
            var dto = JsonSerializer.Deserialize<VoucherChangeDto>(pc.ChangesJson!)!;
            var dup = await Db.Vouchers.AnyAsync(v => v.Code == dto.Code && v.VoucherId != voucher.VoucherId);
            if (dup) throw new InvalidOperationException($"Mã voucher '{dto.Code}' đã tồn tại.");
            voucher.Code = dto.Code;
            voucher.DiscountType = dto.DiscountType;
            voucher.DiscountValue = dto.DiscountValue;
            voucher.MinOrderAmount = dto.MinOrderAmount;
            voucher.StartDate = dto.StartDate;
            voucher.EndDate = dto.EndDate;
            voucher.UsageLimit = dto.UsageLimit;
            voucher.IsActive = dto.IsActive;
        }
        else if (pc.ActionType == "Delete")
        {
            var voucher = await Db.Vouchers.FindAsync(pc.EntityId);
            if (voucher is null) return;
            var inUse = await Db.Tickets.AnyAsync(t => t.VoucherId == voucher.VoucherId);
            if (inUse) throw new InvalidOperationException("Voucher đã được sử dụng trong ít nhất một đơn vé, không thể xóa.");
            Db.Vouchers.Remove(voucher);
        }
        await Db.SaveChangesAsync();
    }

    private async Task ApplyRoomAsync(PendingChange pc)
    {
        if (pc.ActionType == "Create")
        {
            var dto = JsonSerializer.Deserialize<RoomChangeDto>(pc.ChangesJson!)!;
            Db.Rooms.Add(new Room { RoomName = dto.RoomName, TotalSeats = dto.TotalSeats, IsActive = dto.IsActive });
        }
        else if (pc.ActionType == "Update")
        {
            var room = await Db.Rooms.FindAsync(pc.EntityId);
            if (room is null) throw new InvalidOperationException("Phòng chiếu không còn tồn tại.");
            var dto = JsonSerializer.Deserialize<RoomChangeDto>(pc.ChangesJson!)!;
            room.RoomName = dto.RoomName;
            room.TotalSeats = dto.TotalSeats;
            room.IsActive = dto.IsActive;
        }
        else if (pc.ActionType == "Delete")
        {
            var room = await Db.Rooms.FindAsync(pc.EntityId);
            if (room is null) return;
            var hasShowtimes = await Db.Showtimes.AnyAsync(s => s.RoomId == room.RoomId);
            if (hasShowtimes) throw new InvalidOperationException("Phòng đang có suất chiếu liên kết, không thể xóa.");
            var seats = Db.Seats.Where(s => s.RoomId == room.RoomId);
            Db.Seats.RemoveRange(seats);
            Db.Rooms.Remove(room);
        }
        await Db.SaveChangesAsync();
    }

    private async Task ApplyShowtimeAsync(PendingChange pc)
    {
        if (pc.ActionType == "Create")
        {
            var dto = JsonSerializer.Deserialize<ShowtimeChangeDto>(pc.ChangesJson!)!;
            var overlap = await Db.Showtimes.AnyAsync(s => s.RoomId == dto.RoomId && s.Status != "Đã hủy" &&
                dto.StartTime < s.EndTime && dto.EndTime > s.StartTime);
            if (overlap) throw new InvalidOperationException("Phòng chiếu đã có suất chiếu khác trong khung giờ này.");

            var showtime = new Showtime
            {
                MovieId = dto.MovieId,
                RoomId = dto.RoomId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                TicketPrice = dto.TicketPrice,
                Status = dto.Status
            };
            Db.Showtimes.Add(showtime);
            await Db.SaveChangesAsync(); // cần ShowtimeId trước khi tạo ShowtimeSeat

            var seatIds = await Db.Seats.Where(s => s.RoomId == dto.RoomId).Select(s => s.SeatId).ToListAsync();
            foreach (var seatId in seatIds)
                Db.ShowtimeSeats.Add(new ShowtimeSeat { ShowtimeId = showtime.ShowtimeId, SeatId = seatId, Status = "Trống" });
        }
        else if (pc.ActionType == "Update")
        {
            var showtime = await Db.Showtimes.FindAsync(pc.EntityId);
            if (showtime is null) throw new InvalidOperationException("Suất chiếu không còn tồn tại.");
            var dto = JsonSerializer.Deserialize<ShowtimeChangeDto>(pc.ChangesJson!)!;

            var hasBookedSeats = await Db.ShowtimeSeats.AnyAsync(ss => ss.ShowtimeId == showtime.ShowtimeId && ss.Status == "Đã đặt");
            if (hasBookedSeats && showtime.RoomId != dto.RoomId)
                throw new InvalidOperationException("Không thể đổi phòng: suất chiếu đã có ghế được đặt.");

            var overlap = await Db.Showtimes.AnyAsync(s => s.RoomId == dto.RoomId && s.ShowtimeId != showtime.ShowtimeId &&
                s.Status != "Đã hủy" && dto.StartTime < s.EndTime && dto.EndTime > s.StartTime);
            if (overlap) throw new InvalidOperationException("Phòng chiếu đã có suất chiếu khác trong khung giờ này.");

            showtime.MovieId = dto.MovieId;
            showtime.RoomId = dto.RoomId;
            showtime.StartTime = dto.StartTime;
            showtime.EndTime = dto.EndTime;
            showtime.TicketPrice = dto.TicketPrice;
            showtime.Status = dto.Status;
        }
        else if (pc.ActionType == "Cancel")
        {
            var showtime = await Db.Showtimes.FindAsync(pc.EntityId);
            if (showtime is null) return;
            if (showtime.Status == "Đã hủy") return;
            var hasBookedSeats = await Db.ShowtimeSeats.AnyAsync(ss => ss.ShowtimeId == showtime.ShowtimeId && ss.Status == "Đã đặt");
            if (hasBookedSeats)
                throw new InvalidOperationException("Suất chiếu đã có vé được đặt — cần xử lý hoàn vé cho khách trước khi hủy.");
            showtime.Status = "Đã hủy";
        }
        await Db.SaveChangesAsync();
    }

    private async Task ApplyPaymentAsync(PendingChange pc)
    {
        var payment = await Db.Payments.FindAsync(pc.EntityId);
        if (payment is null) throw new InvalidOperationException("Giao dịch thanh toán không còn tồn tại.");
        var dto = JsonSerializer.Deserialize<PaymentStatusChangeDto>(pc.ChangesJson!)!;
        payment.PaymentStatus = dto.PaymentStatus;
        await Db.SaveChangesAsync();
    }

    private async Task ApplyCustomerAsync(PendingChange pc)
    {
        var customer = await Db.Customers.FindAsync(pc.EntityId);
        if (customer is null) throw new InvalidOperationException("Khách hàng không còn tồn tại.");
        var dto = JsonSerializer.Deserialize<CustomerLockChangeDto>(pc.ChangesJson!)!;
        customer.IsActive = dto.IsActive;
        await Db.SaveChangesAsync();
    }

}

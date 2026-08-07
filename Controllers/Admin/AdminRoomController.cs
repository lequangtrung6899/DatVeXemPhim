using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý phòng chiếu" và "Quản lý ghế".
public class AdminRoomController : AdminBaseController
{
    private const int PageSize = 6;

    public AdminRoomController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/phong-chieu")]
    public async Task<IActionResult> Index(int page = 1)
    {
        var query = Db.Rooms.OrderBy(r => r.RoomName);
        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var rooms = await query.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Pagination = new PaginationVM { Page = page, TotalPages = totalPages, BaseUrl = "/quan-tri/phong-chieu?" };
        return View(rooms);
    }

    [HttpGet, Route("/quan-tri/phong-chieu/them")]
    public IActionResult Create() => View("Edit", new Room());

    [HttpGet, Route("/quan-tri/phong-chieu/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        var room = await Db.Rooms.FindAsync(id);
        if (room is null) return NotFound();
        return View(room);
    }

    [HttpPost, Route("/quan-tri/phong-chieu/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(int roomId, string roomName, int totalSeats, bool isActive)
    {
        roomName = roomName.Trim();
        var isAdmin = await IsAdminRoleAsync();

        if (roomId == 0)
        {
            if (isAdmin)
            {
                Db.Rooms.Add(new Room { RoomName = roomName, TotalSeats = totalSeats, IsActive = isActive });
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã thêm phòng chiếu mới. Hãy thêm ghế cho phòng ở bước tiếp theo.";
            }
            else
            {
                await SubmitPendingChangeAsync("Room", null, "Create",
                    new RoomChangeDto { RoomName = roomName, TotalSeats = totalSeats, IsActive = isActive },
                    $"Thêm phòng chiếu mới '{roomName}'");
                TempData["Success"] = "Đã gửi yêu cầu thêm phòng chiếu mới — chờ Quản trị viên duyệt.";
            }
        }
        else
        {
            var room = await Db.Rooms.FindAsync(roomId);
            if (room is null) return NotFound();

            if (isAdmin)
            {
                room.RoomName = roomName;
                room.TotalSeats = totalSeats;
                room.IsActive = isActive;
                await Db.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật phòng chiếu.";
            }
            else
            {
                await SubmitPendingChangeAsync("Room", room.RoomId, "Update",
                    new RoomChangeDto { RoomName = roomName, TotalSeats = totalSeats, IsActive = isActive },
                    $"Sửa phòng chiếu '{room.RoomName}' → '{roomName}'");
                TempData["Success"] = "Đã gửi yêu cầu sửa phòng chiếu — chờ Quản trị viên duyệt.";
            }
        }
        return Redirect("/quan-tri/phong-chieu");
    }

    [HttpPost, Route("/quan-tri/phong-chieu/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var room = await Db.Rooms.FindAsync(id);
        if (room is null) return NotFound();

        var hasShowtimes = await Db.Showtimes.AnyAsync(s => s.RoomId == id);
        if (hasShowtimes)
        {
            TempData["Error"] = "Không thể xóa: phòng đang có suất chiếu liên kết.";
            return Redirect("/quan-tri/phong-chieu");
        }

        if (await IsAdminRoleAsync())
        {
            var seats = Db.Seats.Where(s => s.RoomId == id);
            Db.Seats.RemoveRange(seats);
            Db.Rooms.Remove(room);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã xóa phòng chiếu.";
        }
        else
        {
            await SubmitPendingChangeAsync("Room", room.RoomId, "Delete", (object?)null, $"Xóa phòng chiếu '{room.RoomName}'");
            TempData["Success"] = "Đã gửi yêu cầu xóa phòng chiếu — chờ Quản trị viên duyệt.";
        }
        return Redirect("/quan-tri/phong-chieu");
    }

    // Ghi chú: Quản lý GHẾ (bên dưới — GenerateGrid/AddSeat/DeleteSeat) CHỦ Ý không qua
    // hàng đợi duyệt. Đây là thao tác kỹ thuật/cấu hình vật lý (không ảnh hưởng tiền bạc
    // hay dữ liệu khách hàng như Combo/Voucher), thường làm 1 lần lúc khởi tạo phòng, và
    // nếu phải chờ Admin duyệt từng ghế một sẽ rất bất tiện khi tạo lưới hàng chục ghế.
    // Rủi ro lạm quyền ở đây thấp — hậu quả tệ nhất là sơ đồ ghế sai, dễ phát hiện và sửa
    // ngay lập tức khi xem lại phòng, không âm thầm gây hại như voucher giả hay hủy vé khống.

    // ---- Seats management (Ca sử dụng "Quản lý ghế") ----

    [HttpGet, Route("/quan-tri/phong-chieu/{roomId:int}/ghe")]
    public async Task<IActionResult> Seats(int roomId)
    {
        var room = await Db.Rooms.FindAsync(roomId);
        if (room is null) return NotFound();

        var seats = await Db.Seats.Where(s => s.RoomId == roomId)
            .OrderBy(s => s.RowLabel).ThenBy(s => s.ColumnNumber)
            .ToListAsync();

        var vm = new AdminRoomSeatsVM { Room = room, Seats = seats };
        return View(vm);
    }

    // Bulk-generate a grid of seats, e.g. rows A-F x 10 columns, all "Thường" by default.
    [HttpPost, Route("/quan-tri/phong-chieu/{roomId:int}/ghe/tao-luoi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerateGrid(int roomId, string rows, int columns, string seatType)
    {
        var room = await Db.Rooms.FindAsync(roomId);
        if (room is null) return NotFound();

        var existing = await Db.Seats.AnyAsync(s => s.RoomId == roomId);
        if (existing)
        {
            TempData["Error"] = "Phòng đã có ghế. Hãy xóa hết ghế cũ trước khi tạo lưới mới.";
            return Redirect($"/quan-tri/phong-chieu/{roomId}/ghe");
        }

        var rowLabels = (rows ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        int count = 0;
        foreach (var row in rowLabels)
        {
            for (int col = 1; col <= columns; col++)
            {
                Db.Seats.Add(new Seat { RoomId = roomId, RowLabel = row, ColumnNumber = col, SeatType = seatType });
                count++;
            }
        }
        room.TotalSeats = count;
        await Db.SaveChangesAsync();
        TempData["Success"] = $"Đã tạo {count} ghế.";
        return Redirect($"/quan-tri/phong-chieu/{roomId}/ghe");
    }

    [HttpPost, Route("/quan-tri/phong-chieu/{roomId:int}/ghe/them")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSeat(int roomId, string rowLabel, int columnNumber, string seatType)
    {
        var room = await Db.Rooms.FindAsync(roomId);
        if (room is null) return NotFound();

        var dup = await Db.Seats.AnyAsync(s => s.RoomId == roomId && s.RowLabel == rowLabel && s.ColumnNumber == columnNumber);
        if (dup)
        {
            TempData["Error"] = "Ghế này đã tồn tại.";
            return Redirect($"/quan-tri/phong-chieu/{roomId}/ghe");
        }

        Db.Seats.Add(new Seat { RoomId = roomId, RowLabel = rowLabel.Trim(), ColumnNumber = columnNumber, SeatType = seatType });
        room.TotalSeats += 1;
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm ghế.";
        return Redirect($"/quan-tri/phong-chieu/{roomId}/ghe");
    }

    [HttpPost, Route("/quan-tri/phong-chieu/{roomId:int}/ghe/{seatId:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSeat(int roomId, int seatId)
    {
        var seat = await Db.Seats.FindAsync(seatId);
        if (seat is null || seat.RoomId != roomId) return NotFound();

        // Chỉ chặn xóa nếu ghế đã thực sự được đặt trong ít nhất 1 vé — không tính việc
        // ghế chỉ đơn thuần có mặt trong lịch của 1 suất chiếu (mọi ghế đều tự động có
        // dòng ShowtimeSeat khi suất chiếu được tạo, kể cả khi chưa ai đặt ghế đó).
        var inUse = await Db.ShowtimeSeats
            .Where(ss => ss.SeatId == seatId)
            .AnyAsync(ss => Db.TicketDetails.Any(td => td.ShowtimeSeatId == ss.ShowtimeSeatId));
        if (inUse)
        {
            TempData["Error"] = "Không thể xóa: ghế này đã được khách đặt trong ít nhất một vé.";
            return Redirect($"/quan-tri/phong-chieu/{roomId}/ghe");
        }

        // Xóa các dòng lịch ghế (ShowtimeSeat) gắn với ghế này trước — đã xác nhận ở
        // trên là chưa có vé nào đặt các dòng này, nên xóa an toàn, tránh lỗi khóa ngoại.
        var relatedShowtimeSeats = Db.ShowtimeSeats.Where(ss => ss.SeatId == seatId);
        Db.ShowtimeSeats.RemoveRange(relatedShowtimeSeats);

        Db.Seats.Remove(seat);
        var room = await Db.Rooms.FindAsync(roomId);
        if (room != null) room.TotalSeats = Math.Max(0, room.TotalSeats - 1);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa ghế.";
        return Redirect($"/quan-tri/phong-chieu/{roomId}/ghe");
    }
}

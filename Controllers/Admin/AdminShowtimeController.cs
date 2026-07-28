using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý suất chiếu".
public class AdminShowtimeController : AdminBaseController
{
    public AdminShowtimeController(ApplicationDbContext db) : base(db) { }

    [HttpGet, Route("/quan-tri/suat-chieu")]
    public async Task<IActionResult> Index(int? movieId, DateTime? date)
    {
        IQueryable<Showtime> query = Db.Showtimes.Include(s => s.Movie).Include(s => s.Room);

        if (movieId.HasValue) query = query.Where(s => s.MovieId == movieId.Value);
        if (date.HasValue)
        {
            var d0 = date.Value.Date;
            var d1 = d0.AddDays(1);
            query = query.Where(s => s.StartTime >= d0 && s.StartTime < d1);
        }

        var showtimes = await query.OrderByDescending(s => s.StartTime).Take(200).ToListAsync();

        ViewBag.Movies = await Db.Movies.OrderBy(m => m.Title).ToListAsync();
        ViewBag.MovieId = movieId;
        ViewBag.Date = date;
        return View(showtimes);
    }

    [HttpGet, Route("/quan-tri/suat-chieu/them")]
    public async Task<IActionResult> Create()
    {
        var vm = new AdminShowtimeEditVM
        {
            Showtime = new Showtime { StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(3) },
            Movies = await Db.Movies.Where(m => m.Status != "Ngừng chiếu").OrderBy(m => m.Title).ToListAsync(),
            Rooms = await Db.Rooms.Where(r => r.IsActive).OrderBy(r => r.RoomName).ToListAsync()
        };
        return View("Edit", vm);
    }

    [HttpGet, Route("/quan-tri/suat-chieu/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        var showtime = await Db.Showtimes.FindAsync(id);
        if (showtime is null) return NotFound();

        var vm = new AdminShowtimeEditVM
        {
            Showtime = showtime,
            Movies = await Db.Movies.OrderBy(m => m.Title).ToListAsync(),
            Rooms = await Db.Rooms.OrderBy(r => r.RoomName).ToListAsync()
        };
        return View(vm);
    }

    [HttpPost, Route("/quan-tri/suat-chieu/luu")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(int showtimeId, int movieId, int roomId, DateTime startTime, DateTime endTime, decimal ticketPrice, string status)
    {
        if (endTime <= startTime)
        {
            TempData["Error"] = "Giờ kết thúc phải sau giờ bắt đầu.";
            return Redirect(showtimeId == 0 ? "/quan-tri/suat-chieu/them" : $"/quan-tri/suat-chieu/{showtimeId}/sua");
        }

        // Prevent double-booking the same room for an overlapping time range.
        var overlap = await Db.Showtimes.AnyAsync(s =>
            s.RoomId == roomId && s.ShowtimeId != showtimeId && s.Status != "Đã hủy" &&
            startTime < s.EndTime && endTime > s.StartTime);
        if (overlap)
        {
            TempData["Error"] = "Phòng chiếu đã có suất chiếu khác trong khung giờ này.";
            return Redirect(showtimeId == 0 ? "/quan-tri/suat-chieu/them" : $"/quan-tri/suat-chieu/{showtimeId}/sua");
        }

        if (showtimeId == 0)
        {
            var showtime = new Showtime
            {
                MovieId = movieId,
                RoomId = roomId,
                StartTime = startTime,
                EndTime = endTime,
                TicketPrice = ticketPrice,
                Status = status
            };
            Db.Showtimes.Add(showtime);
            await Db.SaveChangesAsync(); // need ShowtimeId

            // Auto-provision one ShowtimeSeat per physical seat in the room (Trống by default).
            var seatIds = await Db.Seats.Where(s => s.RoomId == roomId).Select(s => s.SeatId).ToListAsync();
            foreach (var seatId in seatIds)
            {
                Db.ShowtimeSeats.Add(new ShowtimeSeat { ShowtimeId = showtime.ShowtimeId, SeatId = seatId, Status = "Trống" });
            }
            await Db.SaveChangesAsync();
            TempData["Success"] = $"Đã tạo suất chiếu mới với {seatIds.Count} ghế.";
        }
        else
        {
            var showtime = await Db.Showtimes.FindAsync(showtimeId);
            if (showtime is null) return NotFound();

            var hasBookedSeats = await Db.ShowtimeSeats.AnyAsync(ss => ss.ShowtimeId == showtimeId && ss.Status == "Đã đặt");
            if (hasBookedSeats && showtime.RoomId != roomId)
            {
                TempData["Error"] = "Không thể đổi phòng: suất chiếu đã có ghế được đặt.";
                return Redirect($"/quan-tri/suat-chieu/{showtimeId}/sua");
            }

            showtime.MovieId = movieId;
            showtime.RoomId = roomId;
            showtime.StartTime = startTime;
            showtime.EndTime = endTime;
            showtime.TicketPrice = ticketPrice;
            showtime.Status = status;
            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật suất chiếu.";
        }

        return Redirect("/quan-tri/suat-chieu");
    }

    [HttpPost, Route("/quan-tri/suat-chieu/{id:int}/huy")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var showtime = await Db.Showtimes.FindAsync(id);
        if (showtime is null) return NotFound();

        var hasBookedSeats = await Db.ShowtimeSeats.AnyAsync(ss => ss.ShowtimeId == id && ss.Status == "Đã đặt");
        if (hasBookedSeats)
        {
            TempData["Error"] = "Suất chiếu đã có vé được đặt — vui lòng xử lý hoàn vé/hủy vé cho khách trước khi hủy suất chiếu.";
            return Redirect("/quan-tri/suat-chieu");
        }

        showtime.Status = "Đã hủy";
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã hủy suất chiếu.";
        return Redirect("/quan-tri/suat-chieu");
    }
}

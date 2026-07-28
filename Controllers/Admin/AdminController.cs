using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Thống kê báo cáo".
public class AdminController : AdminBaseController
{
    public AdminController(ApplicationDbContext db) : base(db) { }

    // GET /quan-tri
    [HttpGet, Route("/quan-tri")]
    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        var fromDate = (from ?? DateTime.Now.AddDays(-30)).Date;
        var toDate = (to ?? DateTime.Now).Date.AddDays(1).AddSeconds(-1);

        var ticketsInRange = Db.Tickets.Where(t => t.BookingDate >= fromDate && t.BookingDate <= toDate);

        var paidTickets = ticketsInRange.Where(t => t.Status == "Đã thanh toán");

        var totalRevenue = await paidTickets.SumAsync(t => (decimal?)t.TotalAmount) ?? 0;
        var totalTickets = await paidTickets.CountAsync();
        var cancelledTickets = await ticketsInRange.CountAsync(t => t.Status == "Đã hủy");
        var newCustomers = await Db.Customers.CountAsync(c => c.CreatedAt >= fromDate && c.CreatedAt <= toDate);

        var topMovies = await Db.TicketDetails
            .Include(td => td.Ticket)
            .Include(td => td.ShowtimeSeat).ThenInclude(ss => ss.Showtime).ThenInclude(s => s.Movie)
            .Where(td => td.Ticket.Status == "Đã thanh toán" && td.Ticket.BookingDate >= fromDate && td.Ticket.BookingDate <= toDate)
            .GroupBy(td => td.ShowtimeSeat.Showtime.Movie.Title)
            .Select(g => new TopMovieRow
            {
                Title = g.Key,
                TicketsSold = g.Count(),
                Revenue = g.Sum(x => x.Price)
            })
            .OrderByDescending(x => x.TicketsSold)
            .Take(10)
            .ToListAsync();

        var pendingReviews = await Db.Reviews.CountAsync(r => r.Status == "Chờ duyệt");

        var vm = new AdminDashboardVM
        {
            From = fromDate,
            To = to ?? DateTime.Now,
            TotalRevenue = totalRevenue,
            TotalTickets = totalTickets,
            CancelledTickets = cancelledTickets,
            NewCustomers = newCustomers,
            PendingReviews = pendingReviews,
            TopMovies = topMovies
        };

        return View(vm);
    }
}

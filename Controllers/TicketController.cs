using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class TicketController : BaseController
{
    public TicketController(ApplicationDbContext db) : base(db) { }

    // GET /ve/{ticketId}
    [Route("/ve/{ticketId:int}")]
    public async Task<IActionResult> Show(int ticketId)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer is null) return Redirect("/dang-nhap");

        var ticket = await Db.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId && t.CustomerId == customer.CustomerId);
        if (ticket is null) return NotFound();

        var showtime = await Db.Showtimes.Include(s => s.Movie).Include(s => s.Room)
            .FirstAsync(s => s.ShowtimeId == ticket.ShowtimeId);

        var seats = await Db.TicketDetails
            .Include(td => td.ShowtimeSeat).ThenInclude(ss => ss.Seat)
            .Where(td => td.TicketId == ticket.TicketId)
            .Select(td => new TicketSeatLine
            {
                RowLabel = td.ShowtimeSeat.Seat.RowLabel,
                ColumnNumber = td.ShowtimeSeat.Seat.ColumnNumber,
                SeatType = td.ShowtimeSeat.Seat.SeatType,
                Price = td.Price
            })
            .ToListAsync();

        var combos = await Db.TicketCombos
            .Include(tc => tc.Combo)
            .Where(tc => tc.TicketId == ticket.TicketId)
            .Select(tc => new TicketComboLine
            {
                ComboName = tc.Combo.ComboName,
                Quantity = tc.Quantity,
                Price = tc.Price
            })
            .ToListAsync();

        var vm = new TicketVM
        {
            Title = "Vé #" + ticket.TicketId,
            TicketId = ticket.TicketId,
            Status = ticket.Status,
            TotalAmount = ticket.TotalAmount,
            MovieTitle = showtime.Movie.Title,
            RoomName = showtime.Room.RoomName,
            StartTime = showtime.StartTime,
            Seats = seats,
            Combos = combos
        };

        return View(vm);
    }
}

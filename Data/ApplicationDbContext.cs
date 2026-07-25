using DatVeXemPhim.Models;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Combo> Combos => Set<Combo>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Showtime> Showtimes => Set<Showtime>();
    public DbSet<ShowtimeSeat> ShowtimeSeats => Set<ShowtimeSeat>();
    public DbSet<TicketCombo> TicketCombos => Set<TicketCombo>();
    public DbSet<TicketDetail> TicketDetails => Set<TicketDetail>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ---- Composite keys ----
        modelBuilder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<TicketCombo>()
            .HasKey(tc => new { tc.TicketId, tc.ComboId });

        // ---- MovieGenre relationships ----
        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.MovieGenres)
            .HasForeignKey(mg => mg.MovieId);

        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(mg => mg.GenreId);

        // ---- Reviews ----
        modelBuilder.Entity<Review>()
            .HasIndex(r => new { r.MovieId, r.CustomerId })
            .IsUnique();

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.Reviews)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- Seats ----
        modelBuilder.Entity<Seat>()
            .HasIndex(s => new { s.RoomId, s.RowLabel, s.ColumnNumber })
            .IsUnique();

        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Room)
            .WithMany(r => r.Seats)
            .HasForeignKey(s => s.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- Showtimes ----
        modelBuilder.Entity<Showtime>()
            .HasOne(s => s.Movie)
            .WithMany(m => m.Showtimes)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Showtime>()
            .HasOne(s => s.Room)
            .WithMany(r => r.Showtimes)
            .HasForeignKey(s => s.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- ShowtimeSeats ----
        modelBuilder.Entity<ShowtimeSeat>()
            .HasIndex(ss => new { ss.ShowtimeId, ss.SeatId })
            .IsUnique();

        modelBuilder.Entity<ShowtimeSeat>()
            .HasOne(ss => ss.Showtime)
            .WithMany(s => s.ShowtimeSeats)
            .HasForeignKey(ss => ss.ShowtimeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ShowtimeSeat>()
            .HasOne(ss => ss.Seat)
            .WithMany()
            .HasForeignKey(ss => ss.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- TicketCombos ----
        modelBuilder.Entity<TicketCombo>()
            .HasOne(tc => tc.Ticket)
            .WithMany(t => t.TicketCombos)
            .HasForeignKey(tc => tc.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TicketCombo>()
            .HasOne(tc => tc.Combo)
            .WithMany(c => c.TicketCombos)
            .HasForeignKey(tc => tc.ComboId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- TicketDetails ----
        modelBuilder.Entity<TicketDetail>()
            .HasIndex(td => td.ShowtimeSeatId)
            .IsUnique();

        modelBuilder.Entity<TicketDetail>()
            .HasOne(td => td.Ticket)
            .WithMany(t => t.TicketDetails)
            .HasForeignKey(td => td.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TicketDetail>()
            .HasOne(td => td.ShowtimeSeat)
            .WithMany()
            .HasForeignKey(td => td.ShowtimeSeatId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- Tickets ----
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Customer)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Showtime)
            .WithMany()
            .HasForeignKey(t => t.ShowtimeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Voucher)
            .WithMany()
            .HasForeignKey(t => t.VoucherId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- Payments ----
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Ticket)
            .WithMany()
            .HasForeignKey(p => p.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- Users ----
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- Vouchers ----
        modelBuilder.Entity<Voucher>()
            .HasIndex(v => v.Code)
            .IsUnique();
    }
}

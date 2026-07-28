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

        // ==========================================================================
        // Seed data (Code First): reproduces the same starting dataset that used to
        // live in Database/DatVeXemPhim.sql, so `dotnet ef database update` alone is
        // enough to stand up a fully working demo database from scratch.
        // ==========================================================================
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 2, RoleName = "Admin" },
            new Role { RoleId = 1, RoleName = "Staff" }
        );

        modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 5, GenreName = "Hài hước" },
            new Genre { GenreId = 1, GenreName = "Hành động" },
            new Genre { GenreId = 4, GenreName = "Hoạt hình" },
            new Genre { GenreId = 3, GenreName = "Kinh dị" },
            new Genre { GenreId = 7, GenreName = "Tài liệu" },
            new Genre { GenreId = 2, GenreName = "Tình cảm" },
            new Genre { GenreId = 6, GenreName = "Viễn tưởng" }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { RoomId = 1, RoomName = "Phòng chiếu 1", TotalSeats = 15, IsActive = true },
            new Room { RoomId = 2, RoomName = "Phòng chiếu 2", TotalSeats = 8, IsActive = true }
        );

        modelBuilder.Entity<Seat>().HasData(
            new Seat { SeatId = 1, RoomId = 1, RowLabel = "A", ColumnNumber = 1, SeatType = "Thường" },
            new Seat { SeatId = 2, RoomId = 1, RowLabel = "A", ColumnNumber = 2, SeatType = "Thường" },
            new Seat { SeatId = 3, RoomId = 1, RowLabel = "A", ColumnNumber = 3, SeatType = "Thường" },
            new Seat { SeatId = 4, RoomId = 1, RowLabel = "A", ColumnNumber = 4, SeatType = "Thường" },
            new Seat { SeatId = 5, RoomId = 1, RowLabel = "A", ColumnNumber = 5, SeatType = "Thường" },
            new Seat { SeatId = 6, RoomId = 1, RowLabel = "B", ColumnNumber = 1, SeatType = "Thường" },
            new Seat { SeatId = 7, RoomId = 1, RowLabel = "B", ColumnNumber = 2, SeatType = "Thường" },
            new Seat { SeatId = 8, RoomId = 1, RowLabel = "B", ColumnNumber = 3, SeatType = "Thường" },
            new Seat { SeatId = 9, RoomId = 1, RowLabel = "B", ColumnNumber = 4, SeatType = "Thường" },
            new Seat { SeatId = 10, RoomId = 1, RowLabel = "B", ColumnNumber = 5, SeatType = "Thường" },
            new Seat { SeatId = 11, RoomId = 1, RowLabel = "C", ColumnNumber = 1, SeatType = "VIP" },
            new Seat { SeatId = 12, RoomId = 1, RowLabel = "C", ColumnNumber = 2, SeatType = "VIP" },
            new Seat { SeatId = 13, RoomId = 1, RowLabel = "C", ColumnNumber = 3, SeatType = "VIP" },
            new Seat { SeatId = 14, RoomId = 1, RowLabel = "C", ColumnNumber = 4, SeatType = "VIP" },
            new Seat { SeatId = 15, RoomId = 1, RowLabel = "C", ColumnNumber = 5, SeatType = "VIP" },
            new Seat { SeatId = 16, RoomId = 2, RowLabel = "A", ColumnNumber = 1, SeatType = "Thường" },
            new Seat { SeatId = 17, RoomId = 2, RowLabel = "A", ColumnNumber = 2, SeatType = "Thường" },
            new Seat { SeatId = 18, RoomId = 2, RowLabel = "A", ColumnNumber = 3, SeatType = "Thường" },
            new Seat { SeatId = 19, RoomId = 2, RowLabel = "A", ColumnNumber = 4, SeatType = "Thường" },
            new Seat { SeatId = 20, RoomId = 2, RowLabel = "B", ColumnNumber = 1, SeatType = "Đôi" },
            new Seat { SeatId = 21, RoomId = 2, RowLabel = "B", ColumnNumber = 2, SeatType = "Đôi" },
            new Seat { SeatId = 22, RoomId = 2, RowLabel = "B", ColumnNumber = 3, SeatType = "Đôi" },
            new Seat { SeatId = 23, RoomId = 2, RowLabel = "B", ColumnNumber = 4, SeatType = "Đôi" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Username = "admin01", PasswordHash = "$2a$hash_admin01", FullName = "Nguyễn Văn Quản", Email = "admin01@rapphim.vn", Phone = "0900000001", RoleId = 2, IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) },
            new User { UserId = 2, Username = "staff01", PasswordHash = "$2a$hash_staff01", FullName = "Trần Thị Nhân Viên", Email = "staff01@rapphim.vn", Phone = "0900000002", RoleId = 1, IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) }
        );

        modelBuilder.Entity<Voucher>().HasData(
            new Voucher { VoucherId = 1, Code = "SUMMER10", DiscountType = "Phần trăm", DiscountValue = 10.00m, MinOrderAmount = 100000.00m, StartDate = new DateTime(2026,6,1,0,0,0), EndDate = new DateTime(2026,8,31,0,0,0), UsageLimit = 100, UsedCount = 5, IsActive = true },
            new Voucher { VoucherId = 2, Code = "GIAM20K", DiscountType = "Số tiền cố định", DiscountValue = 20000.00m, MinOrderAmount = 150000.00m, StartDate = new DateTime(2026,7,1,0,0,0), EndDate = new DateTime(2026,7,31,0,0,0), UsageLimit = 50, UsedCount = 0, IsActive = true }
        );

        modelBuilder.Entity<Combo>().HasData(
            new Combo { ComboId = 1, ComboName = "Combo 1: Bắp lớn + Nước lớn", Description = "1 bắp rang bơ lớn + 1 nước ngọt lớn", Price = 89000.00m, IsActive = true },
            new Combo { ComboId = 2, ComboName = "Combo 2: Bắp nhỏ + 2 Nước", Description = "1 bắp rang bơ nhỏ + 2 nước ngọt vừa", Price = 79000.00m, IsActive = true }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, FullName = "Lê Văn An", Email = "levanan@gmail.com", PasswordHash = "$2a$hash_cust01", Phone = "0911111111", LoyaltyPoint = 150, MembershipRank = "Thành viên Bạc", IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) },
            new Customer { CustomerId = 2, FullName = "Phạm Thị Bình", Email = "phambinh@gmail.com", PasswordHash = "$2a$hash_cust02", Phone = "0922222222", LoyaltyPoint = 0, MembershipRank = "Thành viên mới", IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) },
            new Customer { CustomerId = 3, FullName = "Hoàng Minh Châu", Email = "hoangchau@gmail.com", PasswordHash = "$2a$hash_cust03", Phone = "0933333333", LoyaltyPoint = 500, MembershipRank = "Thành viên Vàng", IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { MovieId = 1, Title = "Mission: Impossible – The Final Reckoning", Description = "Ethan Hunt và đội IMF đối mặt nhiệm vụ nguy hiểm nhất sự nghiệp trong phần cuối của loạt phim gián điệp hành động kinh điển.", Duration = 170, PosterUrl = "/posters/mission-impossible-the-final-reckoning.jpg", ReleaseDate = new DateTime(2026,8,7,0,0,0), EndDate = new DateTime(2026,9,10,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 2, Title = "Bad Boys: Ride or Die", Description = "Hai cảnh sát Miami Mike Lowrey và Marcus Burnett phải chạy đua để minh oan cho người chỉ huy quá cố của mình.", Duration = 115, PosterUrl = "/posters/bad-boys-ride-or-die.jpg", ReleaseDate = new DateTime(2026,8,15,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 3, Title = "The Fall Guy", Description = "Một diễn viên đóng thế phải điều tra vụ mất tích của ngôi sao điện ảnh trong lúc cố gắng hàn gắn chuyện tình cũ.", Duration = 126, PosterUrl = "/posters/the-fall-guy.jpg", ReleaseDate = new DateTime(2026,7,4,0,0,0), EndDate = new DateTime(2026,8,13,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 4, Title = "Furiosa: A Mad Max Saga", Description = "Câu chuyện về tuổi trẻ của Furiosa trong thế giới hậu tận thế khắc nghiệt của vũ trụ Mad Max.", Duration = 148, PosterUrl = "/posters/furiosa-a-mad-max-saga.jpg", ReleaseDate = new DateTime(2026,8,28,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 5, Title = "Twisters", Description = "Một nhóm thợ săn bão liều lĩnh đối đầu với những cơn lốc xoáy ngày càng khốc liệt ở vùng Trung Tây nước Mỹ.", Duration = 122, PosterUrl = "/posters/twisters.jpg", ReleaseDate = new DateTime(2026,6,23,0,0,0), EndDate = new DateTime(2026,7,22,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 6, Title = "Anyone but You", Description = "Hai người từng có một đêm hẹn hò tuyệt vời rồi trở mặt bất ngờ, buộc phải giả vờ yêu nhau tại một đám cưới ở Úc.", Duration = 103, PosterUrl = "/posters/anyone-but-you.jpg", ReleaseDate = new DateTime(2026,6,29,0,0,0), EndDate = new DateTime(2026,8,15,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 7, Title = "It Ends with Us", Description = "Một người phụ nữ trẻ phải đối mặt với những lựa chọn khó khăn khi tình yêu và quá khứ đau buồn đan xen.", Duration = 130, PosterUrl = "/posters/it-ends-with-us.jpg", ReleaseDate = new DateTime(2026,9,22,0,0,0), EndDate = new DateTime(2026,11,10,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 8, Title = "We Live in Time", Description = "Một cặp đôi cùng nhau trải qua những cột mốc vui buồn của cuộc sống, tình yêu và bệnh tật.", Duration = 108, PosterUrl = "/posters/we-live-in-time.jpg", ReleaseDate = new DateTime(2026,7,31,0,0,0), EndDate = new DateTime(2026,9,9,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 9, Title = "The Idea of You", Description = "Một người mẹ đơn thân bất ngờ nảy sinh tình cảm với chàng ca sĩ trẻ của một ban nhạc nổi tiếng.", Duration = 115, PosterUrl = "/posters/the-idea-of-you.jpg", ReleaseDate = new DateTime(2026,9,13,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 10, Title = "Past Lives", Description = "Hai người bạn thời thơ ấu tái ngộ sau nhiều năm xa cách, đối diện với những gì có thể đã xảy ra.", Duration = 106, PosterUrl = "/posters/past-lives.jpg", ReleaseDate = new DateTime(2026,8,29,0,0,0), EndDate = new DateTime(2026,10,24,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 11, Title = "The Substance", Description = "Một ngôi sao đang lụi tàn sử dụng loại thuốc bí ẩn để tạo ra phiên bản trẻ trung hơn của chính mình, với cái giá khủng khiếp.", Duration = 141, PosterUrl = "/posters/the-substance.jpg", ReleaseDate = new DateTime(2026,8,17,0,0,0), EndDate = new DateTime(2026,9,17,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 12, Title = "Smile 2", Description = "Một ngôi sao nhạc pop phải đối mặt với những sự kiện ngày càng đáng sợ khi thực tại bắt đầu sụp đổ quanh cô.", Duration = 127, PosterUrl = "/posters/smile-2.jpg", ReleaseDate = new DateTime(2026,6,15,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 13, Title = "Terrifier 3", Description = "Gã hề sát nhân Art the Clown trở lại gieo rắc kinh hoàng trong đêm Giáng sinh.", Duration = 125, PosterUrl = "/posters/terrifier-3.jpg", ReleaseDate = new DateTime(2026,7,11,0,0,0), EndDate = new DateTime(2026,9,3,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 14, Title = "Beetlejuice Beetlejuice", Description = "Ba thế hệ trong gia đình Deetz vô tình mở lại cánh cổng dẫn đến thế giới của hồn ma Beetlejuice.", Duration = 105, PosterUrl = "/posters/beetlejuice-beetlejuice.jpg", ReleaseDate = new DateTime(2026,8,9,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 15, Title = "Longlegs", Description = "Một đặc vụ FBI điều tra loạt án mạng liên quan đến các manh mối huyền bí đầy ám ảnh.", Duration = 101, PosterUrl = "/posters/longlegs.jpg", ReleaseDate = new DateTime(2026,8,6,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 16, Title = "Inside Out 2", Description = "Riley bước vào tuổi dậy thì và phải đối mặt với những cảm xúc mới phức tạp hơn trong tâm trí mình.", Duration = 96, PosterUrl = "/posters/inside-out-2.jpg", ReleaseDate = new DateTime(2026,8,16,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 17, Title = "Moana 2", Description = "Moana lên đường trong một chuyến hải trình mới đầy thử thách cùng những người bạn cũ và mới.", Duration = 100, PosterUrl = "/posters/moana-2.jpg", ReleaseDate = new DateTime(2026,8,31,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 18, Title = "Despicable Me 4", Description = "Gru phải bảo vệ gia đình mới của mình trước một kẻ thù cũ đầy nguy hiểm.", Duration = 94, PosterUrl = "/posters/despicable-me-4.jpg", ReleaseDate = new DateTime(2026,8,13,0,0,0), EndDate = new DateTime(2026,10,3,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 19, Title = "The Wild Robot", Description = "Một robot bị mắc kẹt trên hòn đảo hoang phải học cách sinh tồn và trở thành người mẹ nuôi của một chú ngỗng con.", Duration = 102, PosterUrl = "/posters/the-wild-robot.jpg", ReleaseDate = new DateTime(2026,6,21,0,0,0), EndDate = new DateTime(2026,7,31,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 20, Title = "Kung Fu Panda 4", Description = "Po phải tìm người kế nhiệm làm Rồng Chiến Binh trong khi đối mặt với một pháp sư biến hình nguy hiểm.", Duration = 94, PosterUrl = "/posters/kung-fu-panda-4.jpg", ReleaseDate = new DateTime(2026,7,16,0,0,0), EndDate = new DateTime(2026,8,22,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 21, Title = "Barbie", Description = "Barbie rời khỏi thế giới hoàn hảo của mình để khám phá thế giới thực đầy bất ngờ.", Duration = 114, PosterUrl = "/posters/barbie.jpg", ReleaseDate = new DateTime(2026,9,26,0,0,0), EndDate = new DateTime(2026,11,17,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 22, Title = "No Hard Feelings", Description = "Một phụ nữ được thuê để giúp một chàng trai nhút nhát tự tin hơn trước khi vào đại học.", Duration = 103, PosterUrl = "/posters/no-hard-feelings.jpg", ReleaseDate = new DateTime(2026,7,13,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 23, Title = "Argylle", Description = "Một nữ tiểu thuyết gia phát hiện cốt truyện trong sách của mình đang trở thành sự thật ngoài đời.", Duration = 139, PosterUrl = "/posters/argylle.jpg", ReleaseDate = new DateTime(2026,9,16,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 24, Title = "Y2K", Description = "Một nhóm bạn trẻ phải sống sót qua đêm giao thừa thiên niên kỷ khi máy móc nổi loạn.", Duration = 93, PosterUrl = "/posters/y2k.jpg", ReleaseDate = new DateTime(2026,8,15,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 25, Title = "Am I OK?", Description = "Một phụ nữ ở độ tuổi 30 bắt đầu hành trình khám phá lại chính bản thân mình.", Duration = 96, PosterUrl = "/posters/am-i-ok.jpg", ReleaseDate = new DateTime(2026,6,14,0,0,0), EndDate = new DateTime(2026,8,1,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 26, Title = "Dune: Part Two", Description = "Paul Atreides hợp lực cùng người Fremen trên hành trình trả thù và định đoạt số phận cả vũ trụ.", Duration = 166, PosterUrl = "/posters/dune-part-two.jpg", ReleaseDate = new DateTime(2026,9,17,0,0,0), EndDate = new DateTime(2026,10,29,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 27, Title = "Godzilla x Kong: The New Empire", Description = "Hai quái vật huyền thoại Godzilla và Kong buộc phải bắt tay chống lại một mối đe dọa ẩn giấu.", Duration = 115, PosterUrl = "/posters/godzilla-x-kong-the-new-empire.jpg", ReleaseDate = new DateTime(2026,6,24,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 28, Title = "Alien: Romulus", Description = "Một nhóm người trẻ khai thác trạm vũ trụ bỏ hoang chạm trán sinh vật ngoài hành tinh nguy hiểm bậc nhất vũ trụ.", Duration = 119, PosterUrl = "/posters/alien-romulus.jpg", ReleaseDate = new DateTime(2026,7,30,0,0,0), EndDate = new DateTime(2026,9,28,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 29, Title = "The Creator", Description = "Trong cuộc chiến giữa loài người và trí tuệ nhân tạo, một cựu binh phát hiện vũ khí bí mật mang hình hài đứa trẻ.", Duration = 133, PosterUrl = "/posters/the-creator.jpg", ReleaseDate = new DateTime(2026,6,13,0,0,0), EndDate = new DateTime(2026,7,19,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 30, Title = "Poor Things", Description = "Một phụ nữ trẻ được hồi sinh bởi khoa học kỳ lạ và bắt đầu hành trình khám phá thế giới theo cách riêng của mình.", Duration = 141, PosterUrl = "/posters/poor-things.jpg", ReleaseDate = new DateTime(2026,7,7,0,0,0), EndDate = new DateTime(2026,8,21,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 31, Title = "Free Solo", Description = "Ghi lại hành trình leo núi El Capitan không dây bảo hộ đầy mạo hiểm của vận động viên Alex Honnold.", Duration = 100, PosterUrl = "/posters/free-solo.jpg", ReleaseDate = new DateTime(2026,7,29,0,0,0), EndDate = new DateTime(2026,8,31,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 32, Title = "My Octopus Teacher", Description = "Một nhà làm phim xây dựng mối quan hệ đặc biệt với một con bạch tuộc hoang dã ngoài khơi Nam Phi.", Duration = 85, PosterUrl = "/posters/my-octopus-teacher.jpg", ReleaseDate = new DateTime(2026,6,20,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 33, Title = "13th", Description = "Bộ phim tài liệu phân tích mối liên hệ giữa chế độ nô lệ và hệ thống nhà tù ở nước Mỹ hiện đại.", Duration = 100, PosterUrl = "/posters/13th.jpg", ReleaseDate = new DateTime(2026,9,18,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 34, Title = "Won't You Be My Neighbor?", Description = "Chân dung về cuộc đời và di sản của Fred Rogers, người dẫn chương trình truyền hình thiếu nhi huyền thoại.", Duration = 94, PosterUrl = "/posters/won-t-you-be-my-neighbor.jpg", ReleaseDate = new DateTime(2026,7,24,0,0,0), EndDate = new DateTime(2026,9,1,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 35, Title = "Fyre: The Greatest Party That Never Happened", Description = "Câu chuyện có thật đằng sau lễ hội âm nhạc xa hoa sụp đổ thảm hại trên mạng xã hội.", Duration = 97, PosterUrl = "/posters/fyre-the-greatest-party-that-never-happened.jpg", ReleaseDate = new DateTime(2026,7,23,0,0,0), EndDate = new DateTime(2026,9,21,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) }
        );

        modelBuilder.Entity<MovieGenre>().HasData(
            new MovieGenre { MovieId = 1, GenreId = 1 },
            new MovieGenre { MovieId = 2, GenreId = 1 },
            new MovieGenre { MovieId = 3, GenreId = 1 },
            new MovieGenre { MovieId = 4, GenreId = 1 },
            new MovieGenre { MovieId = 5, GenreId = 1 },
            new MovieGenre { MovieId = 6, GenreId = 2 },
            new MovieGenre { MovieId = 7, GenreId = 2 },
            new MovieGenre { MovieId = 8, GenreId = 2 },
            new MovieGenre { MovieId = 9, GenreId = 2 },
            new MovieGenre { MovieId = 10, GenreId = 2 },
            new MovieGenre { MovieId = 11, GenreId = 3 },
            new MovieGenre { MovieId = 12, GenreId = 3 },
            new MovieGenre { MovieId = 13, GenreId = 3 },
            new MovieGenre { MovieId = 14, GenreId = 3 },
            new MovieGenre { MovieId = 15, GenreId = 3 },
            new MovieGenre { MovieId = 16, GenreId = 4 },
            new MovieGenre { MovieId = 17, GenreId = 4 },
            new MovieGenre { MovieId = 18, GenreId = 4 },
            new MovieGenre { MovieId = 19, GenreId = 4 },
            new MovieGenre { MovieId = 20, GenreId = 4 },
            new MovieGenre { MovieId = 21, GenreId = 5 },
            new MovieGenre { MovieId = 22, GenreId = 5 },
            new MovieGenre { MovieId = 23, GenreId = 5 },
            new MovieGenre { MovieId = 24, GenreId = 5 },
            new MovieGenre { MovieId = 25, GenreId = 5 },
            new MovieGenre { MovieId = 26, GenreId = 6 },
            new MovieGenre { MovieId = 27, GenreId = 6 },
            new MovieGenre { MovieId = 28, GenreId = 6 },
            new MovieGenre { MovieId = 29, GenreId = 6 },
            new MovieGenre { MovieId = 30, GenreId = 6 },
            new MovieGenre { MovieId = 31, GenreId = 7 },
            new MovieGenre { MovieId = 32, GenreId = 7 },
            new MovieGenre { MovieId = 33, GenreId = 7 },
            new MovieGenre { MovieId = 34, GenreId = 7 },
            new MovieGenre { MovieId = 35, GenreId = 7 }
        );
    }
}

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

        // ---- Suất chiếu mẫu (7 ngày tới cho phim "Đang chiếu", 7 ngày kế tiếp mở bán
        // trước cho phim "Sắp chiếu"), cùng toàn bộ ghế trống tương ứng của mỗi suất ----
        modelBuilder.Entity<Showtime>().HasData(
            new Showtime { ShowtimeId = 1, MovieId = 6, RoomId = 1, StartTime = new DateTime(2026,7,29,9,30,0), EndTime = new DateTime(2026,7,29,11,13,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 2, MovieId = 6, RoomId = 2, StartTime = new DateTime(2026,8,2,9,30,0), EndTime = new DateTime(2026,8,2,11,13,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 3, MovieId = 7, RoomId = 2, StartTime = new DateTime(2026,7,30,9,30,0), EndTime = new DateTime(2026,7,30,11,40,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 4, MovieId = 7, RoomId = 1, StartTime = new DateTime(2026,8,3,9,30,0), EndTime = new DateTime(2026,8,3,11,40,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 5, MovieId = 10, RoomId = 1, StartTime = new DateTime(2026,7,31,9,30,0), EndTime = new DateTime(2026,7,31,11,16,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 6, MovieId = 10, RoomId = 2, StartTime = new DateTime(2026,8,4,9,30,0), EndTime = new DateTime(2026,8,4,11,16,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 7, MovieId = 11, RoomId = 2, StartTime = new DateTime(2026,8,1,9,30,0), EndTime = new DateTime(2026,8,1,11,51,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 8, MovieId = 11, RoomId = 1, StartTime = new DateTime(2026,7,29,11,33,0), EndTime = new DateTime(2026,7,29,13,54,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 9, MovieId = 13, RoomId = 1, StartTime = new DateTime(2026,8,2,9,30,0), EndTime = new DateTime(2026,8,2,11,35,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 10, MovieId = 13, RoomId = 2, StartTime = new DateTime(2026,7,30,12,0,0), EndTime = new DateTime(2026,7,30,14,5,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 11, MovieId = 18, RoomId = 2, StartTime = new DateTime(2026,8,3,9,30,0), EndTime = new DateTime(2026,8,3,11,4,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 12, MovieId = 18, RoomId = 1, StartTime = new DateTime(2026,7,31,11,36,0), EndTime = new DateTime(2026,7,31,13,10,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 13, MovieId = 21, RoomId = 1, StartTime = new DateTime(2026,8,4,9,30,0), EndTime = new DateTime(2026,8,4,11,24,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 14, MovieId = 21, RoomId = 2, StartTime = new DateTime(2026,8,1,12,11,0), EndTime = new DateTime(2026,8,1,14,5,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 15, MovieId = 25, RoomId = 2, StartTime = new DateTime(2026,7,29,9,30,0), EndTime = new DateTime(2026,7,29,11,6,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 16, MovieId = 25, RoomId = 1, StartTime = new DateTime(2026,8,2,11,55,0), EndTime = new DateTime(2026,8,2,13,31,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 17, MovieId = 26, RoomId = 1, StartTime = new DateTime(2026,7,30,9,30,0), EndTime = new DateTime(2026,7,30,12,16,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 18, MovieId = 26, RoomId = 2, StartTime = new DateTime(2026,8,3,11,24,0), EndTime = new DateTime(2026,8,3,14,10,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 19, MovieId = 28, RoomId = 2, StartTime = new DateTime(2026,7,31,9,30,0), EndTime = new DateTime(2026,7,31,11,29,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 20, MovieId = 28, RoomId = 1, StartTime = new DateTime(2026,8,4,11,44,0), EndTime = new DateTime(2026,8,4,13,43,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 21, MovieId = 29, RoomId = 1, StartTime = new DateTime(2026,8,1,9,30,0), EndTime = new DateTime(2026,8,1,11,43,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 22, MovieId = 29, RoomId = 2, StartTime = new DateTime(2026,7,29,11,26,0), EndTime = new DateTime(2026,7,29,13,39,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 23, MovieId = 30, RoomId = 2, StartTime = new DateTime(2026,8,2,11,33,0), EndTime = new DateTime(2026,8,2,13,54,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 24, MovieId = 30, RoomId = 1, StartTime = new DateTime(2026,7,30,12,36,0), EndTime = new DateTime(2026,7,30,14,57,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 25, MovieId = 34, RoomId = 1, StartTime = new DateTime(2026,8,3,12,0,0), EndTime = new DateTime(2026,8,3,13,34,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 26, MovieId = 34, RoomId = 2, StartTime = new DateTime(2026,7,31,11,49,0), EndTime = new DateTime(2026,7,31,13,23,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 27, MovieId = 35, RoomId = 2, StartTime = new DateTime(2026,8,4,11,36,0), EndTime = new DateTime(2026,8,4,13,13,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 28, MovieId = 35, RoomId = 1, StartTime = new DateTime(2026,8,1,12,3,0), EndTime = new DateTime(2026,8,1,13,40,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 29, MovieId = 2, RoomId = 1, StartTime = new DateTime(2026,8,5,9,30,0), EndTime = new DateTime(2026,8,5,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 30, MovieId = 2, RoomId = 2, StartTime = new DateTime(2026,8,9,9,30,0), EndTime = new DateTime(2026,8,9,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 31, MovieId = 4, RoomId = 2, StartTime = new DateTime(2026,8,6,9,30,0), EndTime = new DateTime(2026,8,6,11,58,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 32, MovieId = 4, RoomId = 1, StartTime = new DateTime(2026,8,10,9,30,0), EndTime = new DateTime(2026,8,10,11,58,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 33, MovieId = 9, RoomId = 1, StartTime = new DateTime(2026,8,7,9,30,0), EndTime = new DateTime(2026,8,7,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 34, MovieId = 9, RoomId = 2, StartTime = new DateTime(2026,8,11,9,30,0), EndTime = new DateTime(2026,8,11,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 35, MovieId = 12, RoomId = 2, StartTime = new DateTime(2026,8,8,9,30,0), EndTime = new DateTime(2026,8,8,11,37,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 36, MovieId = 12, RoomId = 1, StartTime = new DateTime(2026,8,5,11,45,0), EndTime = new DateTime(2026,8,5,13,52,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 37, MovieId = 14, RoomId = 1, StartTime = new DateTime(2026,8,9,9,30,0), EndTime = new DateTime(2026,8,9,11,15,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 38, MovieId = 14, RoomId = 2, StartTime = new DateTime(2026,8,6,12,18,0), EndTime = new DateTime(2026,8,6,14,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 39, MovieId = 15, RoomId = 2, StartTime = new DateTime(2026,8,10,9,30,0), EndTime = new DateTime(2026,8,10,11,11,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 40, MovieId = 15, RoomId = 1, StartTime = new DateTime(2026,8,7,11,45,0), EndTime = new DateTime(2026,8,7,13,26,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 41, MovieId = 16, RoomId = 1, StartTime = new DateTime(2026,8,11,9,30,0), EndTime = new DateTime(2026,8,11,11,6,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 42, MovieId = 16, RoomId = 2, StartTime = new DateTime(2026,8,8,11,57,0), EndTime = new DateTime(2026,8,8,13,33,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 43, MovieId = 17, RoomId = 2, StartTime = new DateTime(2026,8,5,9,30,0), EndTime = new DateTime(2026,8,5,11,10,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 44, MovieId = 17, RoomId = 1, StartTime = new DateTime(2026,8,9,11,35,0), EndTime = new DateTime(2026,8,9,13,15,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 45, MovieId = 22, RoomId = 1, StartTime = new DateTime(2026,8,6,9,30,0), EndTime = new DateTime(2026,8,6,11,13,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 46, MovieId = 22, RoomId = 2, StartTime = new DateTime(2026,8,10,11,31,0), EndTime = new DateTime(2026,8,10,13,14,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 47, MovieId = 23, RoomId = 2, StartTime = new DateTime(2026,8,7,9,30,0), EndTime = new DateTime(2026,8,7,11,49,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 48, MovieId = 23, RoomId = 1, StartTime = new DateTime(2026,8,11,11,26,0), EndTime = new DateTime(2026,8,11,13,45,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 49, MovieId = 24, RoomId = 1, StartTime = new DateTime(2026,8,8,9,30,0), EndTime = new DateTime(2026,8,8,11,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 50, MovieId = 24, RoomId = 2, StartTime = new DateTime(2026,8,5,11,30,0), EndTime = new DateTime(2026,8,5,13,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 51, MovieId = 27, RoomId = 2, StartTime = new DateTime(2026,8,9,11,45,0), EndTime = new DateTime(2026,8,9,13,40,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 52, MovieId = 27, RoomId = 1, StartTime = new DateTime(2026,8,6,11,33,0), EndTime = new DateTime(2026,8,6,13,28,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 53, MovieId = 32, RoomId = 1, StartTime = new DateTime(2026,8,10,12,18,0), EndTime = new DateTime(2026,8,10,13,43,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 54, MovieId = 32, RoomId = 2, StartTime = new DateTime(2026,8,7,12,9,0), EndTime = new DateTime(2026,8,7,13,34,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 55, MovieId = 33, RoomId = 2, StartTime = new DateTime(2026,8,11,11,45,0), EndTime = new DateTime(2026,8,11,13,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 56, MovieId = 33, RoomId = 1, StartTime = new DateTime(2026,8,8,11,23,0), EndTime = new DateTime(2026,8,8,13,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" }
        );

        modelBuilder.Entity<ShowtimeSeat>().HasData(
            new ShowtimeSeat { ShowtimeSeatId = 1, ShowtimeId = 1, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 2, ShowtimeId = 1, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 3, ShowtimeId = 1, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 4, ShowtimeId = 1, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 5, ShowtimeId = 1, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 6, ShowtimeId = 1, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 7, ShowtimeId = 1, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 8, ShowtimeId = 1, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 9, ShowtimeId = 1, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 10, ShowtimeId = 1, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 11, ShowtimeId = 1, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 12, ShowtimeId = 1, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 13, ShowtimeId = 1, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 14, ShowtimeId = 1, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 15, ShowtimeId = 1, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 16, ShowtimeId = 2, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 17, ShowtimeId = 2, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 18, ShowtimeId = 2, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 19, ShowtimeId = 2, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 20, ShowtimeId = 2, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 21, ShowtimeId = 2, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 22, ShowtimeId = 2, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 23, ShowtimeId = 2, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 24, ShowtimeId = 3, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 25, ShowtimeId = 3, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 26, ShowtimeId = 3, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 27, ShowtimeId = 3, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 28, ShowtimeId = 3, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 29, ShowtimeId = 3, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 30, ShowtimeId = 3, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 31, ShowtimeId = 3, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 32, ShowtimeId = 4, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 33, ShowtimeId = 4, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 34, ShowtimeId = 4, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 35, ShowtimeId = 4, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 36, ShowtimeId = 4, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 37, ShowtimeId = 4, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 38, ShowtimeId = 4, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 39, ShowtimeId = 4, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 40, ShowtimeId = 4, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 41, ShowtimeId = 4, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 42, ShowtimeId = 4, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 43, ShowtimeId = 4, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 44, ShowtimeId = 4, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 45, ShowtimeId = 4, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 46, ShowtimeId = 4, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 47, ShowtimeId = 5, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 48, ShowtimeId = 5, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 49, ShowtimeId = 5, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 50, ShowtimeId = 5, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 51, ShowtimeId = 5, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 52, ShowtimeId = 5, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 53, ShowtimeId = 5, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 54, ShowtimeId = 5, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 55, ShowtimeId = 5, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 56, ShowtimeId = 5, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 57, ShowtimeId = 5, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 58, ShowtimeId = 5, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 59, ShowtimeId = 5, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 60, ShowtimeId = 5, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 61, ShowtimeId = 5, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 62, ShowtimeId = 6, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 63, ShowtimeId = 6, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 64, ShowtimeId = 6, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 65, ShowtimeId = 6, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 66, ShowtimeId = 6, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 67, ShowtimeId = 6, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 68, ShowtimeId = 6, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 69, ShowtimeId = 6, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 70, ShowtimeId = 7, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 71, ShowtimeId = 7, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 72, ShowtimeId = 7, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 73, ShowtimeId = 7, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 74, ShowtimeId = 7, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 75, ShowtimeId = 7, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 76, ShowtimeId = 7, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 77, ShowtimeId = 7, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 78, ShowtimeId = 8, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 79, ShowtimeId = 8, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 80, ShowtimeId = 8, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 81, ShowtimeId = 8, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 82, ShowtimeId = 8, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 83, ShowtimeId = 8, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 84, ShowtimeId = 8, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 85, ShowtimeId = 8, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 86, ShowtimeId = 8, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 87, ShowtimeId = 8, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 88, ShowtimeId = 8, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 89, ShowtimeId = 8, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 90, ShowtimeId = 8, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 91, ShowtimeId = 8, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 92, ShowtimeId = 8, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 93, ShowtimeId = 9, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 94, ShowtimeId = 9, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 95, ShowtimeId = 9, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 96, ShowtimeId = 9, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 97, ShowtimeId = 9, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 98, ShowtimeId = 9, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 99, ShowtimeId = 9, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 100, ShowtimeId = 9, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 101, ShowtimeId = 9, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 102, ShowtimeId = 9, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 103, ShowtimeId = 9, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 104, ShowtimeId = 9, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 105, ShowtimeId = 9, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 106, ShowtimeId = 9, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 107, ShowtimeId = 9, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 108, ShowtimeId = 10, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 109, ShowtimeId = 10, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 110, ShowtimeId = 10, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 111, ShowtimeId = 10, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 112, ShowtimeId = 10, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 113, ShowtimeId = 10, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 114, ShowtimeId = 10, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 115, ShowtimeId = 10, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 116, ShowtimeId = 11, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 117, ShowtimeId = 11, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 118, ShowtimeId = 11, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 119, ShowtimeId = 11, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 120, ShowtimeId = 11, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 121, ShowtimeId = 11, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 122, ShowtimeId = 11, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 123, ShowtimeId = 11, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 124, ShowtimeId = 12, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 125, ShowtimeId = 12, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 126, ShowtimeId = 12, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 127, ShowtimeId = 12, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 128, ShowtimeId = 12, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 129, ShowtimeId = 12, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 130, ShowtimeId = 12, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 131, ShowtimeId = 12, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 132, ShowtimeId = 12, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 133, ShowtimeId = 12, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 134, ShowtimeId = 12, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 135, ShowtimeId = 12, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 136, ShowtimeId = 12, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 137, ShowtimeId = 12, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 138, ShowtimeId = 12, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 139, ShowtimeId = 13, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 140, ShowtimeId = 13, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 141, ShowtimeId = 13, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 142, ShowtimeId = 13, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 143, ShowtimeId = 13, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 144, ShowtimeId = 13, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 145, ShowtimeId = 13, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 146, ShowtimeId = 13, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 147, ShowtimeId = 13, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 148, ShowtimeId = 13, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 149, ShowtimeId = 13, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 150, ShowtimeId = 13, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 151, ShowtimeId = 13, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 152, ShowtimeId = 13, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 153, ShowtimeId = 13, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 154, ShowtimeId = 14, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 155, ShowtimeId = 14, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 156, ShowtimeId = 14, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 157, ShowtimeId = 14, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 158, ShowtimeId = 14, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 159, ShowtimeId = 14, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 160, ShowtimeId = 14, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 161, ShowtimeId = 14, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 162, ShowtimeId = 15, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 163, ShowtimeId = 15, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 164, ShowtimeId = 15, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 165, ShowtimeId = 15, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 166, ShowtimeId = 15, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 167, ShowtimeId = 15, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 168, ShowtimeId = 15, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 169, ShowtimeId = 15, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 170, ShowtimeId = 16, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 171, ShowtimeId = 16, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 172, ShowtimeId = 16, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 173, ShowtimeId = 16, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 174, ShowtimeId = 16, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 175, ShowtimeId = 16, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 176, ShowtimeId = 16, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 177, ShowtimeId = 16, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 178, ShowtimeId = 16, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 179, ShowtimeId = 16, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 180, ShowtimeId = 16, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 181, ShowtimeId = 16, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 182, ShowtimeId = 16, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 183, ShowtimeId = 16, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 184, ShowtimeId = 16, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 185, ShowtimeId = 17, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 186, ShowtimeId = 17, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 187, ShowtimeId = 17, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 188, ShowtimeId = 17, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 189, ShowtimeId = 17, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 190, ShowtimeId = 17, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 191, ShowtimeId = 17, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 192, ShowtimeId = 17, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 193, ShowtimeId = 17, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 194, ShowtimeId = 17, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 195, ShowtimeId = 17, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 196, ShowtimeId = 17, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 197, ShowtimeId = 17, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 198, ShowtimeId = 17, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 199, ShowtimeId = 17, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200, ShowtimeId = 18, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 201, ShowtimeId = 18, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 202, ShowtimeId = 18, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 203, ShowtimeId = 18, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 204, ShowtimeId = 18, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 205, ShowtimeId = 18, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 206, ShowtimeId = 18, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 207, ShowtimeId = 18, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 208, ShowtimeId = 19, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 209, ShowtimeId = 19, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 210, ShowtimeId = 19, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 211, ShowtimeId = 19, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 212, ShowtimeId = 19, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 213, ShowtimeId = 19, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 214, ShowtimeId = 19, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 215, ShowtimeId = 19, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 216, ShowtimeId = 20, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 217, ShowtimeId = 20, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 218, ShowtimeId = 20, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 219, ShowtimeId = 20, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 220, ShowtimeId = 20, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 221, ShowtimeId = 20, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 222, ShowtimeId = 20, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 223, ShowtimeId = 20, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 224, ShowtimeId = 20, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 225, ShowtimeId = 20, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 226, ShowtimeId = 20, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 227, ShowtimeId = 20, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 228, ShowtimeId = 20, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 229, ShowtimeId = 20, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 230, ShowtimeId = 20, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 231, ShowtimeId = 21, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 232, ShowtimeId = 21, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 233, ShowtimeId = 21, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 234, ShowtimeId = 21, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 235, ShowtimeId = 21, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 236, ShowtimeId = 21, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 237, ShowtimeId = 21, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 238, ShowtimeId = 21, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 239, ShowtimeId = 21, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 240, ShowtimeId = 21, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 241, ShowtimeId = 21, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 242, ShowtimeId = 21, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 243, ShowtimeId = 21, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 244, ShowtimeId = 21, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 245, ShowtimeId = 21, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 246, ShowtimeId = 22, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 247, ShowtimeId = 22, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 248, ShowtimeId = 22, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 249, ShowtimeId = 22, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 250, ShowtimeId = 22, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 251, ShowtimeId = 22, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 252, ShowtimeId = 22, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 253, ShowtimeId = 22, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 254, ShowtimeId = 23, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 255, ShowtimeId = 23, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 256, ShowtimeId = 23, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 257, ShowtimeId = 23, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 258, ShowtimeId = 23, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 259, ShowtimeId = 23, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 260, ShowtimeId = 23, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 261, ShowtimeId = 23, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 262, ShowtimeId = 24, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 263, ShowtimeId = 24, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 264, ShowtimeId = 24, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 265, ShowtimeId = 24, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 266, ShowtimeId = 24, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 267, ShowtimeId = 24, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 268, ShowtimeId = 24, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 269, ShowtimeId = 24, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 270, ShowtimeId = 24, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 271, ShowtimeId = 24, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 272, ShowtimeId = 24, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 273, ShowtimeId = 24, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 274, ShowtimeId = 24, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 275, ShowtimeId = 24, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 276, ShowtimeId = 24, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 277, ShowtimeId = 25, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 278, ShowtimeId = 25, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 279, ShowtimeId = 25, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 280, ShowtimeId = 25, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 281, ShowtimeId = 25, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 282, ShowtimeId = 25, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 283, ShowtimeId = 25, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 284, ShowtimeId = 25, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 285, ShowtimeId = 25, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 286, ShowtimeId = 25, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 287, ShowtimeId = 25, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 288, ShowtimeId = 25, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 289, ShowtimeId = 25, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 290, ShowtimeId = 25, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 291, ShowtimeId = 25, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 292, ShowtimeId = 26, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 293, ShowtimeId = 26, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 294, ShowtimeId = 26, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 295, ShowtimeId = 26, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 296, ShowtimeId = 26, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 297, ShowtimeId = 26, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 298, ShowtimeId = 26, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 299, ShowtimeId = 26, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 300, ShowtimeId = 27, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 301, ShowtimeId = 27, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 302, ShowtimeId = 27, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 303, ShowtimeId = 27, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 304, ShowtimeId = 27, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 305, ShowtimeId = 27, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 306, ShowtimeId = 27, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 307, ShowtimeId = 27, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 308, ShowtimeId = 28, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 309, ShowtimeId = 28, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 310, ShowtimeId = 28, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 311, ShowtimeId = 28, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 312, ShowtimeId = 28, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 313, ShowtimeId = 28, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 314, ShowtimeId = 28, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 315, ShowtimeId = 28, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 316, ShowtimeId = 28, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 317, ShowtimeId = 28, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 318, ShowtimeId = 28, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 319, ShowtimeId = 28, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 320, ShowtimeId = 28, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 321, ShowtimeId = 28, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 322, ShowtimeId = 28, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 323, ShowtimeId = 29, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 324, ShowtimeId = 29, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 325, ShowtimeId = 29, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 326, ShowtimeId = 29, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 327, ShowtimeId = 29, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 328, ShowtimeId = 29, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 329, ShowtimeId = 29, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 330, ShowtimeId = 29, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 331, ShowtimeId = 29, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 332, ShowtimeId = 29, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 333, ShowtimeId = 29, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 334, ShowtimeId = 29, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 335, ShowtimeId = 29, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 336, ShowtimeId = 29, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 337, ShowtimeId = 29, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 338, ShowtimeId = 30, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 339, ShowtimeId = 30, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 340, ShowtimeId = 30, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 341, ShowtimeId = 30, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 342, ShowtimeId = 30, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 343, ShowtimeId = 30, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 344, ShowtimeId = 30, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 345, ShowtimeId = 30, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 346, ShowtimeId = 31, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 347, ShowtimeId = 31, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 348, ShowtimeId = 31, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 349, ShowtimeId = 31, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 350, ShowtimeId = 31, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 351, ShowtimeId = 31, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 352, ShowtimeId = 31, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 353, ShowtimeId = 31, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 354, ShowtimeId = 32, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 355, ShowtimeId = 32, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 356, ShowtimeId = 32, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 357, ShowtimeId = 32, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 358, ShowtimeId = 32, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 359, ShowtimeId = 32, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 360, ShowtimeId = 32, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 361, ShowtimeId = 32, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 362, ShowtimeId = 32, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 363, ShowtimeId = 32, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 364, ShowtimeId = 32, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 365, ShowtimeId = 32, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 366, ShowtimeId = 32, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 367, ShowtimeId = 32, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 368, ShowtimeId = 32, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 369, ShowtimeId = 33, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 370, ShowtimeId = 33, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 371, ShowtimeId = 33, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 372, ShowtimeId = 33, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 373, ShowtimeId = 33, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 374, ShowtimeId = 33, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 375, ShowtimeId = 33, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 376, ShowtimeId = 33, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 377, ShowtimeId = 33, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 378, ShowtimeId = 33, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 379, ShowtimeId = 33, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 380, ShowtimeId = 33, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 381, ShowtimeId = 33, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 382, ShowtimeId = 33, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 383, ShowtimeId = 33, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 384, ShowtimeId = 34, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 385, ShowtimeId = 34, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 386, ShowtimeId = 34, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 387, ShowtimeId = 34, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 388, ShowtimeId = 34, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 389, ShowtimeId = 34, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 390, ShowtimeId = 34, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 391, ShowtimeId = 34, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 392, ShowtimeId = 35, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 393, ShowtimeId = 35, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 394, ShowtimeId = 35, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 395, ShowtimeId = 35, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 396, ShowtimeId = 35, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 397, ShowtimeId = 35, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 398, ShowtimeId = 35, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 399, ShowtimeId = 35, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 400, ShowtimeId = 36, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 401, ShowtimeId = 36, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 402, ShowtimeId = 36, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 403, ShowtimeId = 36, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 404, ShowtimeId = 36, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 405, ShowtimeId = 36, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 406, ShowtimeId = 36, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 407, ShowtimeId = 36, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 408, ShowtimeId = 36, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 409, ShowtimeId = 36, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 410, ShowtimeId = 36, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 411, ShowtimeId = 36, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 412, ShowtimeId = 36, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 413, ShowtimeId = 36, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 414, ShowtimeId = 36, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 415, ShowtimeId = 37, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 416, ShowtimeId = 37, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 417, ShowtimeId = 37, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 418, ShowtimeId = 37, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 419, ShowtimeId = 37, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 420, ShowtimeId = 37, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 421, ShowtimeId = 37, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 422, ShowtimeId = 37, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 423, ShowtimeId = 37, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 424, ShowtimeId = 37, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 425, ShowtimeId = 37, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 426, ShowtimeId = 37, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 427, ShowtimeId = 37, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 428, ShowtimeId = 37, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 429, ShowtimeId = 37, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 430, ShowtimeId = 38, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 431, ShowtimeId = 38, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 432, ShowtimeId = 38, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 433, ShowtimeId = 38, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 434, ShowtimeId = 38, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 435, ShowtimeId = 38, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 436, ShowtimeId = 38, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 437, ShowtimeId = 38, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 438, ShowtimeId = 39, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 439, ShowtimeId = 39, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 440, ShowtimeId = 39, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 441, ShowtimeId = 39, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 442, ShowtimeId = 39, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 443, ShowtimeId = 39, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 444, ShowtimeId = 39, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 445, ShowtimeId = 39, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 446, ShowtimeId = 40, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 447, ShowtimeId = 40, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 448, ShowtimeId = 40, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 449, ShowtimeId = 40, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 450, ShowtimeId = 40, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 451, ShowtimeId = 40, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 452, ShowtimeId = 40, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 453, ShowtimeId = 40, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 454, ShowtimeId = 40, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 455, ShowtimeId = 40, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 456, ShowtimeId = 40, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 457, ShowtimeId = 40, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 458, ShowtimeId = 40, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 459, ShowtimeId = 40, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 460, ShowtimeId = 40, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 461, ShowtimeId = 41, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 462, ShowtimeId = 41, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 463, ShowtimeId = 41, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 464, ShowtimeId = 41, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 465, ShowtimeId = 41, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 466, ShowtimeId = 41, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 467, ShowtimeId = 41, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 468, ShowtimeId = 41, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 469, ShowtimeId = 41, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 470, ShowtimeId = 41, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 471, ShowtimeId = 41, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 472, ShowtimeId = 41, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 473, ShowtimeId = 41, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 474, ShowtimeId = 41, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 475, ShowtimeId = 41, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 476, ShowtimeId = 42, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 477, ShowtimeId = 42, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 478, ShowtimeId = 42, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 479, ShowtimeId = 42, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 480, ShowtimeId = 42, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 481, ShowtimeId = 42, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 482, ShowtimeId = 42, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 483, ShowtimeId = 42, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 484, ShowtimeId = 43, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 485, ShowtimeId = 43, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 486, ShowtimeId = 43, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 487, ShowtimeId = 43, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 488, ShowtimeId = 43, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 489, ShowtimeId = 43, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 490, ShowtimeId = 43, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 491, ShowtimeId = 43, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 492, ShowtimeId = 44, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 493, ShowtimeId = 44, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 494, ShowtimeId = 44, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 495, ShowtimeId = 44, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 496, ShowtimeId = 44, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 497, ShowtimeId = 44, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 498, ShowtimeId = 44, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 499, ShowtimeId = 44, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 500, ShowtimeId = 44, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 501, ShowtimeId = 44, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 502, ShowtimeId = 44, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 503, ShowtimeId = 44, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 504, ShowtimeId = 44, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 505, ShowtimeId = 44, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 506, ShowtimeId = 44, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 507, ShowtimeId = 45, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 508, ShowtimeId = 45, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 509, ShowtimeId = 45, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 510, ShowtimeId = 45, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 511, ShowtimeId = 45, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 512, ShowtimeId = 45, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 513, ShowtimeId = 45, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 514, ShowtimeId = 45, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 515, ShowtimeId = 45, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 516, ShowtimeId = 45, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 517, ShowtimeId = 45, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 518, ShowtimeId = 45, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 519, ShowtimeId = 45, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 520, ShowtimeId = 45, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 521, ShowtimeId = 45, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 522, ShowtimeId = 46, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 523, ShowtimeId = 46, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 524, ShowtimeId = 46, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 525, ShowtimeId = 46, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 526, ShowtimeId = 46, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 527, ShowtimeId = 46, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 528, ShowtimeId = 46, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 529, ShowtimeId = 46, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 530, ShowtimeId = 47, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 531, ShowtimeId = 47, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 532, ShowtimeId = 47, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 533, ShowtimeId = 47, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 534, ShowtimeId = 47, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 535, ShowtimeId = 47, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 536, ShowtimeId = 47, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 537, ShowtimeId = 47, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 538, ShowtimeId = 48, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 539, ShowtimeId = 48, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 540, ShowtimeId = 48, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 541, ShowtimeId = 48, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 542, ShowtimeId = 48, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 543, ShowtimeId = 48, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 544, ShowtimeId = 48, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 545, ShowtimeId = 48, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 546, ShowtimeId = 48, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 547, ShowtimeId = 48, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 548, ShowtimeId = 48, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 549, ShowtimeId = 48, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 550, ShowtimeId = 48, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 551, ShowtimeId = 48, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 552, ShowtimeId = 48, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 553, ShowtimeId = 49, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 554, ShowtimeId = 49, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 555, ShowtimeId = 49, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 556, ShowtimeId = 49, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 557, ShowtimeId = 49, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 558, ShowtimeId = 49, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 559, ShowtimeId = 49, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 560, ShowtimeId = 49, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 561, ShowtimeId = 49, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 562, ShowtimeId = 49, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 563, ShowtimeId = 49, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 564, ShowtimeId = 49, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 565, ShowtimeId = 49, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 566, ShowtimeId = 49, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 567, ShowtimeId = 49, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 568, ShowtimeId = 50, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 569, ShowtimeId = 50, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 570, ShowtimeId = 50, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 571, ShowtimeId = 50, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 572, ShowtimeId = 50, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 573, ShowtimeId = 50, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 574, ShowtimeId = 50, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 575, ShowtimeId = 50, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 576, ShowtimeId = 51, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 577, ShowtimeId = 51, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 578, ShowtimeId = 51, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 579, ShowtimeId = 51, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 580, ShowtimeId = 51, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 581, ShowtimeId = 51, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 582, ShowtimeId = 51, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 583, ShowtimeId = 51, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 584, ShowtimeId = 52, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 585, ShowtimeId = 52, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 586, ShowtimeId = 52, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 587, ShowtimeId = 52, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 588, ShowtimeId = 52, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 589, ShowtimeId = 52, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 590, ShowtimeId = 52, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 591, ShowtimeId = 52, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 592, ShowtimeId = 52, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 593, ShowtimeId = 52, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 594, ShowtimeId = 52, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 595, ShowtimeId = 52, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 596, ShowtimeId = 52, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 597, ShowtimeId = 52, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 598, ShowtimeId = 52, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 599, ShowtimeId = 53, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 600, ShowtimeId = 53, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 601, ShowtimeId = 53, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 602, ShowtimeId = 53, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 603, ShowtimeId = 53, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 604, ShowtimeId = 53, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 605, ShowtimeId = 53, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 606, ShowtimeId = 53, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 607, ShowtimeId = 53, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 608, ShowtimeId = 53, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 609, ShowtimeId = 53, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 610, ShowtimeId = 53, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 611, ShowtimeId = 53, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 612, ShowtimeId = 53, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 613, ShowtimeId = 53, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 614, ShowtimeId = 54, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 615, ShowtimeId = 54, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 616, ShowtimeId = 54, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 617, ShowtimeId = 54, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 618, ShowtimeId = 54, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 619, ShowtimeId = 54, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 620, ShowtimeId = 54, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 621, ShowtimeId = 54, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 622, ShowtimeId = 55, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 623, ShowtimeId = 55, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 624, ShowtimeId = 55, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 625, ShowtimeId = 55, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 626, ShowtimeId = 55, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 627, ShowtimeId = 55, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 628, ShowtimeId = 55, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 629, ShowtimeId = 55, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 630, ShowtimeId = 56, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 631, ShowtimeId = 56, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 632, ShowtimeId = 56, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 633, ShowtimeId = 56, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 634, ShowtimeId = 56, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 635, ShowtimeId = 56, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 636, ShowtimeId = 56, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 637, ShowtimeId = 56, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 638, ShowtimeId = 56, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 639, ShowtimeId = 56, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 640, ShowtimeId = 56, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 641, ShowtimeId = 56, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 642, ShowtimeId = 56, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 643, ShowtimeId = 56, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 644, ShowtimeId = 56, SeatId = 15, Status = "Trống" }
        );
    }
}

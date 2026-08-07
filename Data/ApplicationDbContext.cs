using DatVeXemPhim.Models;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Combo> Combos => Set<Combo>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PendingChange> PendingChanges => Set<PendingChange>();
    public DbSet<RefundRequest> RefundRequests => Set<RefundRequest>();
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

        // ---- RefundRequests (yêu cầu hoàn tiền, cần Nhân viên + Admin duyệt) ----
        modelBuilder.Entity<RefundRequest>()
            .HasOne(r => r.Ticket)
            .WithMany()
            .HasForeignKey(r => r.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RefundRequest>()
            .HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RefundRequest>()
            .HasOne(r => r.StaffApprover)
            .WithMany()
            .HasForeignKey(r => r.StaffApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RefundRequest>()
            .HasOne(r => r.AdminApprover)
            .WithMany()
            .HasForeignKey(r => r.AdminApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RefundRequest>()
            .HasOne(r => r.Rejecter)
            .WithMany()
            .HasForeignKey(r => r.RejectedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- PendingChange ---- (hàng đợi chờ Admin duyệt, xem Models/PendingChange.cs)
        modelBuilder.Entity<PendingChange>()
            .HasOne(pc => pc.SubmittedByUser)
            .WithMany()
            .HasForeignKey(pc => pc.SubmittedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PendingChange>()
            .HasOne(pc => pc.ReviewedByUser)
            .WithMany()
            .HasForeignKey(pc => pc.ReviewedBy)
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

        // Mật khẩu demo cho MỌI tài khoản seed bên dưới (User lẫn Customer) là "123456"
        // (đã băm bằng PasswordHasherHelper — xem Services/PasswordHasherHelper.cs).
        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Username = "admin01", PasswordHash = "100000.CHbfym1v7fmbKrh7bi/tSw==.hOJV2nu7uA6qiEAqU5BkCXQ7lpThnQOAvqosUHxby2M=", FullName = "Nguyễn Văn Quản", Email = "admin01@rapphim.vn", Phone = "0900000001", RoleId = 2, IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) },
            new User { UserId = 2, Username = "staff01", PasswordHash = "100000.glxfROvxJ6dnkFPGXnwJDw==.KWyDIAdywy5y/vUCO3btYzxO/5Vj0DU7KqD29qqZl1A=", FullName = "Trần Thị Nhân Viên", Email = "staff01@rapphim.vn", Phone = "0900000002", RoleId = 1, IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) }
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
            new Customer { CustomerId = 1, FullName = "Lê Văn An", Email = "levanan@gmail.com", PasswordHash = "100000.4SgTxqKC1i7w9NcxXcVMug==.d1OwkzA/BzT14g9XjJUynl6I2Q8E89AWKCNl5kH24Fs=", Phone = "0911111111", LoyaltyPoint = 150, MembershipRank = "Thành viên Bạc", IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) },
            new Customer { CustomerId = 2, FullName = "Phạm Thị Bình", Email = "phambinh@gmail.com", PasswordHash = "100000.kxJLGbkx9ggbzb7IS9HeXA==.+y/EWaZsaqW7VX9EkiBhxYCZrYAKVhvyy1KOlQeYAl0=", Phone = "0922222222", LoyaltyPoint = 0, MembershipRank = "Thành viên mới", IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) },
            new Customer { CustomerId = 3, FullName = "Hoàng Minh Châu", Email = "hoangchau@gmail.com", PasswordHash = "100000.wfRv+xtcdWvsuJNlQKaVPg==.mMtChUK9eHp8Bx41+z/gcGMh6HGquvH0jiqTjHbOml4=", Phone = "0933333333", LoyaltyPoint = 500, MembershipRank = "Thành viên Vàng", IsActive = true, CreatedAt = new DateTime(2026,7,9,15,25,8,577) }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { MovieId = 1, Title = "Mission: Impossible – The Final Reckoning", Description = "Ethan Hunt và đội IMF đối mặt nhiệm vụ nguy hiểm nhất sự nghiệp trong phần cuối của loạt phim gián điệp hành động kinh điển.", Duration = 170, PosterUrl = "/posters/mission-impossible-the-final-reckoning.jpg", ReleaseDate = new DateTime(2026,8,7,0,0,0), EndDate = new DateTime(2026,9,10,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 2, Title = "Bad Boys: Ride or Die", Description = "Hai cảnh sát Miami Mike Lowrey và Marcus Burnett phải chạy đua để minh oan cho người chỉ huy quá cố của mình.", Duration = 115, PosterUrl = "/posters/bad-boys-ride-or-die.jpg", ReleaseDate = new DateTime(2026,8,15,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 3, Title = "The Fall Guy", Description = "Một diễn viên đóng thế phải điều tra vụ mất tích của ngôi sao điện ảnh trong lúc cố gắng hàn gắn chuyện tình cũ.", Duration = 126, PosterUrl = "/posters/the-fall-guy.jpg", ReleaseDate = new DateTime(2026,7,4,0,0,0), EndDate = new DateTime(2026,8,13,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 4, Title = "Furiosa: A Mad Max Saga", Description = "Câu chuyện về tuổi trẻ của Furiosa trong thế giới hậu tận thế khắc nghiệt của vũ trụ Mad Max.", Duration = 148, PosterUrl = "/posters/furiosa-a-mad-max-saga.jpg", ReleaseDate = new DateTime(2026,8,28,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 5, Title = "Twisters", Description = "Một nhóm thợ săn bão liều lĩnh đối đầu với những cơn lốc xoáy ngày càng khốc liệt ở vùng Trung Tây nước Mỹ.", Duration = 122, PosterUrl = "/posters/twisters.jpg", ReleaseDate = new DateTime(2026,6,23,0,0,0), EndDate = new DateTime(2026,7,22,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 6, Title = "Anyone but You", Description = "Hai người từng có một đêm hẹn hò tuyệt vời rồi trở mặt bất ngờ, buộc phải giả vờ yêu nhau tại một đám cưới ở Úc.", Duration = 103, PosterUrl = "/posters/anyone-but-you.jpg", ReleaseDate = new DateTime(2026,6,29,0,0,0), EndDate = new DateTime(2026,8,15,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 7, Title = "It Ends with Us", Description = "Một người phụ nữ trẻ phải đối mặt với những lựa chọn khó khăn khi tình yêu và quá khứ đau buồn đan xen.", Duration = 130, PosterUrl = "/posters/it-ends-with-us.jpg", BannerUrl = "/banners/it-ends-with-us-banner.jpg", ReleaseDate = new DateTime(2026,9,22,0,0,0), EndDate = new DateTime(2026,11,10,0,0,0), Status = "Đang chiếu", ShowOnBanner = true, CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 8, Title = "We Live in Time", Description = "Một cặp đôi cùng nhau trải qua những cột mốc vui buồn của cuộc sống, tình yêu và bệnh tật.", Duration = 108, PosterUrl = "/posters/we-live-in-time.jpg", ReleaseDate = new DateTime(2026,7,31,0,0,0), EndDate = new DateTime(2026,9,9,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 9, Title = "The Idea of You", Description = "Một người mẹ đơn thân bất ngờ nảy sinh tình cảm với chàng ca sĩ trẻ của một ban nhạc nổi tiếng.", Duration = 115, PosterUrl = "/posters/the-idea-of-you.jpg", ReleaseDate = new DateTime(2026,9,13,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 10, Title = "Past Lives", Description = "Hai người bạn thời thơ ấu tái ngộ sau nhiều năm xa cách, đối diện với những gì có thể đã xảy ra.", Duration = 106, PosterUrl = "/posters/past-lives.jpg", BannerUrl = "/banners/past-lives-banner.jpg", ReleaseDate = new DateTime(2026,8,29,0,0,0), EndDate = new DateTime(2026,10,24,0,0,0), Status = "Đang chiếu", ShowOnBanner = true, CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 11, Title = "The Substance", Description = "Một ngôi sao đang lụi tàn sử dụng loại thuốc bí ẩn để tạo ra phiên bản trẻ trung hơn của chính mình, với cái giá khủng khiếp.", Duration = 141, PosterUrl = "/posters/the-substance.jpg", BannerUrl = "/banners/the-substance-banner.jpg", ReleaseDate = new DateTime(2026,8,17,0,0,0), EndDate = new DateTime(2026,9,17,0,0,0), Status = "Đang chiếu", ShowOnBanner = true, CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 12, Title = "Smile 2", Description = "Một ngôi sao nhạc pop phải đối mặt với những sự kiện ngày càng đáng sợ khi thực tại bắt đầu sụp đổ quanh cô.", Duration = 127, PosterUrl = "/posters/smile-2.jpg", ReleaseDate = new DateTime(2026,6,15,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 13, Title = "Terrifier 3", Description = "Gã hề sát nhân Art the Clown trở lại gieo rắc kinh hoàng trong đêm Giáng sinh.", Duration = 125, PosterUrl = "/posters/terrifier-3.jpg", ReleaseDate = new DateTime(2026,7,11,0,0,0), EndDate = new DateTime(2026,9,3,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 14, Title = "Beetlejuice Beetlejuice", Description = "Ba thế hệ trong gia đình Deetz vô tình mở lại cánh cổng dẫn đến thế giới của hồn ma Beetlejuice.", Duration = 105, PosterUrl = "/posters/beetlejuice-beetlejuice.jpg", ReleaseDate = new DateTime(2026,8,9,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 15, Title = "Longlegs", Description = "Một đặc vụ FBI điều tra loạt án mạng liên quan đến các manh mối huyền bí đầy ám ảnh.", Duration = 101, PosterUrl = "/posters/longlegs.jpg", ReleaseDate = new DateTime(2026,8,6,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 16, Title = "Inside Out 2", Description = "Riley bước vào tuổi dậy thì và phải đối mặt với những cảm xúc mới phức tạp hơn trong tâm trí mình.", Duration = 96, PosterUrl = "/posters/inside-out-2.jpg", ReleaseDate = new DateTime(2026,8,16,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 17, Title = "Moana 2", Description = "Moana lên đường trong một chuyến hải trình mới đầy thử thách cùng những người bạn cũ và mới.", Duration = 100, PosterUrl = "/posters/moana-2.jpg", ReleaseDate = new DateTime(2026,8,31,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 18, Title = "Despicable Me 4", Description = "Gru phải bảo vệ gia đình mới của mình trước một kẻ thù cũ đầy nguy hiểm.", Duration = 94, PosterUrl = "/posters/despicable-me-4.jpg", ReleaseDate = new DateTime(2026,8,13,0,0,0), EndDate = new DateTime(2026,10,3,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 19, Title = "The Wild Robot", Description = "Một robot bị mắc kẹt trên hòn đảo hoang phải học cách sinh tồn và trở thành người mẹ nuôi của một chú ngỗng con.", Duration = 102, PosterUrl = "/posters/the-wild-robot.jpg", ReleaseDate = new DateTime(2026,6,21,0,0,0), EndDate = new DateTime(2026,7,31,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 20, Title = "Kung Fu Panda 4", Description = "Po phải tìm người kế nhiệm làm Rồng Chiến Binh trong khi đối mặt với một pháp sư biến hình nguy hiểm.", Duration = 94, PosterUrl = "/posters/kung-fu-panda-4.jpg", ReleaseDate = new DateTime(2026,7,16,0,0,0), EndDate = new DateTime(2026,8,22,0,0,0), Status = "Ngừng chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 21, Title = "Barbie", Description = "Barbie rời khỏi thế giới hoàn hảo của mình để khám phá thế giới thực đầy bất ngờ.", Duration = 114, PosterUrl = "/posters/barbie.jpg", BannerUrl = "/banners/barbie-banner.jpg", ReleaseDate = new DateTime(2026,9,26,0,0,0), EndDate = new DateTime(2026,11,17,0,0,0), Status = "Đang chiếu", ShowOnBanner = true, CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 22, Title = "No Hard Feelings", Description = "Một phụ nữ được thuê để giúp một chàng trai nhút nhát tự tin hơn trước khi vào đại học.", Duration = 103, PosterUrl = "/posters/no-hard-feelings.jpg", ReleaseDate = new DateTime(2026,7,13,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 23, Title = "Argylle", Description = "Một nữ tiểu thuyết gia phát hiện cốt truyện trong sách của mình đang trở thành sự thật ngoài đời.", Duration = 139, PosterUrl = "/posters/argylle.jpg", ReleaseDate = new DateTime(2026,9,16,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 24, Title = "Y2K", Description = "Một nhóm bạn trẻ phải sống sót qua đêm giao thừa thiên niên kỷ khi máy móc nổi loạn.", Duration = 93, PosterUrl = "/posters/y2k.jpg", ReleaseDate = new DateTime(2026,8,15,0,0,0), EndDate = null, Status = "Sắp chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 25, Title = "Am I OK?", Description = "Một phụ nữ ở độ tuổi 30 bắt đầu hành trình khám phá lại chính bản thân mình.", Duration = 96, PosterUrl = "/posters/am-i-ok.jpg", ReleaseDate = new DateTime(2026,6,14,0,0,0), EndDate = new DateTime(2026,8,1,0,0,0), Status = "Đang chiếu", CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
            new Movie { MovieId = 26, Title = "Dune: Part Two", Description = "Paul Atreides hợp lực cùng người Fremen trên hành trình trả thù và định đoạt số phận cả vũ trụ.", Duration = 166, PosterUrl = "/posters/dune-part-two.jpg", BannerUrl = "/banners/dune-2-banner.jpg", ReleaseDate = new DateTime(2026,9,17,0,0,0), EndDate = new DateTime(2026,10,29,0,0,0), Status = "Đang chiếu", ShowOnBanner = true, CreatedAt = new DateTime(2026,7,12,17,13,55,597) },
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
            new Showtime { ShowtimeId = 100001, MovieId = 6, RoomId = 1, StartTime = new DateTime(2026,7,29,9,30,0), EndTime = new DateTime(2026,7,29,11,13,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100002, MovieId = 6, RoomId = 2, StartTime = new DateTime(2026,8,2,9,30,0), EndTime = new DateTime(2026,8,2,11,13,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100003, MovieId = 7, RoomId = 2, StartTime = new DateTime(2026,7,30,9,30,0), EndTime = new DateTime(2026,7,30,11,40,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100004, MovieId = 7, RoomId = 1, StartTime = new DateTime(2026,8,3,9,30,0), EndTime = new DateTime(2026,8,3,11,40,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100005, MovieId = 10, RoomId = 1, StartTime = new DateTime(2026,7,31,9,30,0), EndTime = new DateTime(2026,7,31,11,16,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100006, MovieId = 10, RoomId = 2, StartTime = new DateTime(2026,8,4,9,30,0), EndTime = new DateTime(2026,8,4,11,16,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100007, MovieId = 11, RoomId = 2, StartTime = new DateTime(2026,8,1,9,30,0), EndTime = new DateTime(2026,8,1,11,51,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100008, MovieId = 11, RoomId = 1, StartTime = new DateTime(2026,7,29,11,33,0), EndTime = new DateTime(2026,7,29,13,54,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100009, MovieId = 13, RoomId = 1, StartTime = new DateTime(2026,8,2,9,30,0), EndTime = new DateTime(2026,8,2,11,35,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100010, MovieId = 13, RoomId = 2, StartTime = new DateTime(2026,7,30,12,0,0), EndTime = new DateTime(2026,7,30,14,5,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100011, MovieId = 18, RoomId = 2, StartTime = new DateTime(2026,8,3,9,30,0), EndTime = new DateTime(2026,8,3,11,4,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100012, MovieId = 18, RoomId = 1, StartTime = new DateTime(2026,7,31,11,36,0), EndTime = new DateTime(2026,7,31,13,10,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100013, MovieId = 21, RoomId = 1, StartTime = new DateTime(2026,8,4,9,30,0), EndTime = new DateTime(2026,8,4,11,24,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100014, MovieId = 21, RoomId = 2, StartTime = new DateTime(2026,8,1,12,11,0), EndTime = new DateTime(2026,8,1,14,5,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100015, MovieId = 25, RoomId = 2, StartTime = new DateTime(2026,7,29,9,30,0), EndTime = new DateTime(2026,7,29,11,6,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100016, MovieId = 25, RoomId = 1, StartTime = new DateTime(2026,8,2,11,55,0), EndTime = new DateTime(2026,8,2,13,31,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100017, MovieId = 26, RoomId = 1, StartTime = new DateTime(2026,7,30,9,30,0), EndTime = new DateTime(2026,7,30,12,16,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100018, MovieId = 26, RoomId = 2, StartTime = new DateTime(2026,8,3,11,24,0), EndTime = new DateTime(2026,8,3,14,10,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100019, MovieId = 28, RoomId = 2, StartTime = new DateTime(2026,7,31,9,30,0), EndTime = new DateTime(2026,7,31,11,29,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100020, MovieId = 28, RoomId = 1, StartTime = new DateTime(2026,8,4,11,44,0), EndTime = new DateTime(2026,8,4,13,43,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100021, MovieId = 29, RoomId = 1, StartTime = new DateTime(2026,8,1,9,30,0), EndTime = new DateTime(2026,8,1,11,43,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100022, MovieId = 29, RoomId = 2, StartTime = new DateTime(2026,7,29,11,26,0), EndTime = new DateTime(2026,7,29,13,39,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100023, MovieId = 30, RoomId = 2, StartTime = new DateTime(2026,8,2,11,33,0), EndTime = new DateTime(2026,8,2,13,54,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100024, MovieId = 30, RoomId = 1, StartTime = new DateTime(2026,7,30,12,36,0), EndTime = new DateTime(2026,7,30,14,57,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100025, MovieId = 34, RoomId = 1, StartTime = new DateTime(2026,8,3,12,0,0), EndTime = new DateTime(2026,8,3,13,34,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100026, MovieId = 34, RoomId = 2, StartTime = new DateTime(2026,7,31,11,49,0), EndTime = new DateTime(2026,7,31,13,23,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100027, MovieId = 35, RoomId = 2, StartTime = new DateTime(2026,8,4,11,36,0), EndTime = new DateTime(2026,8,4,13,13,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100028, MovieId = 35, RoomId = 1, StartTime = new DateTime(2026,8,1,12,3,0), EndTime = new DateTime(2026,8,1,13,40,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100029, MovieId = 2, RoomId = 1, StartTime = new DateTime(2026,8,5,9,30,0), EndTime = new DateTime(2026,8,5,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100030, MovieId = 2, RoomId = 2, StartTime = new DateTime(2026,8,9,9,30,0), EndTime = new DateTime(2026,8,9,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100031, MovieId = 4, RoomId = 2, StartTime = new DateTime(2026,8,6,9,30,0), EndTime = new DateTime(2026,8,6,11,58,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100032, MovieId = 4, RoomId = 1, StartTime = new DateTime(2026,8,10,9,30,0), EndTime = new DateTime(2026,8,10,11,58,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100033, MovieId = 9, RoomId = 1, StartTime = new DateTime(2026,8,7,9,30,0), EndTime = new DateTime(2026,8,7,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100034, MovieId = 9, RoomId = 2, StartTime = new DateTime(2026,8,11,9,30,0), EndTime = new DateTime(2026,8,11,11,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100035, MovieId = 12, RoomId = 2, StartTime = new DateTime(2026,8,8,9,30,0), EndTime = new DateTime(2026,8,8,11,37,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100036, MovieId = 12, RoomId = 1, StartTime = new DateTime(2026,8,5,11,45,0), EndTime = new DateTime(2026,8,5,13,52,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100037, MovieId = 14, RoomId = 1, StartTime = new DateTime(2026,8,9,9,30,0), EndTime = new DateTime(2026,8,9,11,15,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100038, MovieId = 14, RoomId = 2, StartTime = new DateTime(2026,8,6,12,18,0), EndTime = new DateTime(2026,8,6,14,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100039, MovieId = 15, RoomId = 2, StartTime = new DateTime(2026,8,10,9,30,0), EndTime = new DateTime(2026,8,10,11,11,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100040, MovieId = 15, RoomId = 1, StartTime = new DateTime(2026,8,7,11,45,0), EndTime = new DateTime(2026,8,7,13,26,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100041, MovieId = 16, RoomId = 1, StartTime = new DateTime(2026,8,11,9,30,0), EndTime = new DateTime(2026,8,11,11,6,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100042, MovieId = 16, RoomId = 2, StartTime = new DateTime(2026,8,8,11,57,0), EndTime = new DateTime(2026,8,8,13,33,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100043, MovieId = 17, RoomId = 2, StartTime = new DateTime(2026,8,5,9,30,0), EndTime = new DateTime(2026,8,5,11,10,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100044, MovieId = 17, RoomId = 1, StartTime = new DateTime(2026,8,9,11,35,0), EndTime = new DateTime(2026,8,9,13,15,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100045, MovieId = 22, RoomId = 1, StartTime = new DateTime(2026,8,6,9,30,0), EndTime = new DateTime(2026,8,6,11,13,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100046, MovieId = 22, RoomId = 2, StartTime = new DateTime(2026,8,10,11,31,0), EndTime = new DateTime(2026,8,10,13,14,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100047, MovieId = 23, RoomId = 2, StartTime = new DateTime(2026,8,7,9,30,0), EndTime = new DateTime(2026,8,7,11,49,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100048, MovieId = 23, RoomId = 1, StartTime = new DateTime(2026,8,11,11,26,0), EndTime = new DateTime(2026,8,11,13,45,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100049, MovieId = 24, RoomId = 1, StartTime = new DateTime(2026,8,8,9,30,0), EndTime = new DateTime(2026,8,8,11,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100050, MovieId = 24, RoomId = 2, StartTime = new DateTime(2026,8,5,11,30,0), EndTime = new DateTime(2026,8,5,13,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100051, MovieId = 27, RoomId = 2, StartTime = new DateTime(2026,8,9,11,45,0), EndTime = new DateTime(2026,8,9,13,40,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100052, MovieId = 27, RoomId = 1, StartTime = new DateTime(2026,8,6,11,33,0), EndTime = new DateTime(2026,8,6,13,28,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100053, MovieId = 32, RoomId = 1, StartTime = new DateTime(2026,8,10,12,18,0), EndTime = new DateTime(2026,8,10,13,43,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100054, MovieId = 32, RoomId = 2, StartTime = new DateTime(2026,8,7,12,9,0), EndTime = new DateTime(2026,8,7,13,34,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100055, MovieId = 33, RoomId = 2, StartTime = new DateTime(2026,8,11,11,45,0), EndTime = new DateTime(2026,8,11,13,25,0), TicketPrice = 75000m, Status = "Sắp chiếu" },
            new Showtime { ShowtimeId = 100056, MovieId = 33, RoomId = 1, StartTime = new DateTime(2026,8,8,11,23,0), EndTime = new DateTime(2026,8,8,13,3,0), TicketPrice = 75000m, Status = "Sắp chiếu" }
        );

        modelBuilder.Entity<ShowtimeSeat>().HasData(
            new ShowtimeSeat { ShowtimeSeatId = 200001, ShowtimeId = 100001, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200002, ShowtimeId = 100001, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200003, ShowtimeId = 100001, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200004, ShowtimeId = 100001, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200005, ShowtimeId = 100001, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200006, ShowtimeId = 100001, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200007, ShowtimeId = 100001, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200008, ShowtimeId = 100001, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200009, ShowtimeId = 100001, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200010, ShowtimeId = 100001, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200011, ShowtimeId = 100001, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200012, ShowtimeId = 100001, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200013, ShowtimeId = 100001, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200014, ShowtimeId = 100001, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200015, ShowtimeId = 100001, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200016, ShowtimeId = 100002, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200017, ShowtimeId = 100002, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200018, ShowtimeId = 100002, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200019, ShowtimeId = 100002, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200020, ShowtimeId = 100002, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200021, ShowtimeId = 100002, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200022, ShowtimeId = 100002, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200023, ShowtimeId = 100002, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200024, ShowtimeId = 100003, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200025, ShowtimeId = 100003, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200026, ShowtimeId = 100003, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200027, ShowtimeId = 100003, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200028, ShowtimeId = 100003, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200029, ShowtimeId = 100003, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200030, ShowtimeId = 100003, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200031, ShowtimeId = 100003, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200032, ShowtimeId = 100004, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200033, ShowtimeId = 100004, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200034, ShowtimeId = 100004, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200035, ShowtimeId = 100004, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200036, ShowtimeId = 100004, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200037, ShowtimeId = 100004, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200038, ShowtimeId = 100004, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200039, ShowtimeId = 100004, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200040, ShowtimeId = 100004, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200041, ShowtimeId = 100004, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200042, ShowtimeId = 100004, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200043, ShowtimeId = 100004, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200044, ShowtimeId = 100004, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200045, ShowtimeId = 100004, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200046, ShowtimeId = 100004, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200047, ShowtimeId = 100005, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200048, ShowtimeId = 100005, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200049, ShowtimeId = 100005, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200050, ShowtimeId = 100005, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200051, ShowtimeId = 100005, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200052, ShowtimeId = 100005, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200053, ShowtimeId = 100005, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200054, ShowtimeId = 100005, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200055, ShowtimeId = 100005, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200056, ShowtimeId = 100005, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200057, ShowtimeId = 100005, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200058, ShowtimeId = 100005, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200059, ShowtimeId = 100005, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200060, ShowtimeId = 100005, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200061, ShowtimeId = 100005, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200062, ShowtimeId = 100006, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200063, ShowtimeId = 100006, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200064, ShowtimeId = 100006, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200065, ShowtimeId = 100006, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200066, ShowtimeId = 100006, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200067, ShowtimeId = 100006, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200068, ShowtimeId = 100006, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200069, ShowtimeId = 100006, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200070, ShowtimeId = 100007, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200071, ShowtimeId = 100007, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200072, ShowtimeId = 100007, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200073, ShowtimeId = 100007, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200074, ShowtimeId = 100007, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200075, ShowtimeId = 100007, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200076, ShowtimeId = 100007, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200077, ShowtimeId = 100007, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200078, ShowtimeId = 100008, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200079, ShowtimeId = 100008, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200080, ShowtimeId = 100008, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200081, ShowtimeId = 100008, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200082, ShowtimeId = 100008, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200083, ShowtimeId = 100008, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200084, ShowtimeId = 100008, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200085, ShowtimeId = 100008, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200086, ShowtimeId = 100008, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200087, ShowtimeId = 100008, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200088, ShowtimeId = 100008, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200089, ShowtimeId = 100008, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200090, ShowtimeId = 100008, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200091, ShowtimeId = 100008, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200092, ShowtimeId = 100008, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200093, ShowtimeId = 100009, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200094, ShowtimeId = 100009, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200095, ShowtimeId = 100009, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200096, ShowtimeId = 100009, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200097, ShowtimeId = 100009, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200098, ShowtimeId = 100009, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200099, ShowtimeId = 100009, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200100, ShowtimeId = 100009, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200101, ShowtimeId = 100009, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200102, ShowtimeId = 100009, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200103, ShowtimeId = 100009, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200104, ShowtimeId = 100009, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200105, ShowtimeId = 100009, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200106, ShowtimeId = 100009, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200107, ShowtimeId = 100009, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200108, ShowtimeId = 100010, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200109, ShowtimeId = 100010, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200110, ShowtimeId = 100010, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200111, ShowtimeId = 100010, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200112, ShowtimeId = 100010, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200113, ShowtimeId = 100010, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200114, ShowtimeId = 100010, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200115, ShowtimeId = 100010, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200116, ShowtimeId = 100011, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200117, ShowtimeId = 100011, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200118, ShowtimeId = 100011, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200119, ShowtimeId = 100011, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200120, ShowtimeId = 100011, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200121, ShowtimeId = 100011, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200122, ShowtimeId = 100011, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200123, ShowtimeId = 100011, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200124, ShowtimeId = 100012, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200125, ShowtimeId = 100012, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200126, ShowtimeId = 100012, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200127, ShowtimeId = 100012, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200128, ShowtimeId = 100012, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200129, ShowtimeId = 100012, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200130, ShowtimeId = 100012, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200131, ShowtimeId = 100012, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200132, ShowtimeId = 100012, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200133, ShowtimeId = 100012, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200134, ShowtimeId = 100012, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200135, ShowtimeId = 100012, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200136, ShowtimeId = 100012, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200137, ShowtimeId = 100012, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200138, ShowtimeId = 100012, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200139, ShowtimeId = 100013, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200140, ShowtimeId = 100013, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200141, ShowtimeId = 100013, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200142, ShowtimeId = 100013, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200143, ShowtimeId = 100013, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200144, ShowtimeId = 100013, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200145, ShowtimeId = 100013, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200146, ShowtimeId = 100013, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200147, ShowtimeId = 100013, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200148, ShowtimeId = 100013, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200149, ShowtimeId = 100013, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200150, ShowtimeId = 100013, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200151, ShowtimeId = 100013, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200152, ShowtimeId = 100013, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200153, ShowtimeId = 100013, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200154, ShowtimeId = 100014, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200155, ShowtimeId = 100014, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200156, ShowtimeId = 100014, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200157, ShowtimeId = 100014, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200158, ShowtimeId = 100014, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200159, ShowtimeId = 100014, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200160, ShowtimeId = 100014, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200161, ShowtimeId = 100014, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200162, ShowtimeId = 100015, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200163, ShowtimeId = 100015, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200164, ShowtimeId = 100015, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200165, ShowtimeId = 100015, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200166, ShowtimeId = 100015, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200167, ShowtimeId = 100015, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200168, ShowtimeId = 100015, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200169, ShowtimeId = 100015, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200170, ShowtimeId = 100016, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200171, ShowtimeId = 100016, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200172, ShowtimeId = 100016, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200173, ShowtimeId = 100016, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200174, ShowtimeId = 100016, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200175, ShowtimeId = 100016, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200176, ShowtimeId = 100016, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200177, ShowtimeId = 100016, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200178, ShowtimeId = 100016, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200179, ShowtimeId = 100016, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200180, ShowtimeId = 100016, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200181, ShowtimeId = 100016, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200182, ShowtimeId = 100016, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200183, ShowtimeId = 100016, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200184, ShowtimeId = 100016, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200185, ShowtimeId = 100017, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200186, ShowtimeId = 100017, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200187, ShowtimeId = 100017, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200188, ShowtimeId = 100017, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200189, ShowtimeId = 100017, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200190, ShowtimeId = 100017, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200191, ShowtimeId = 100017, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200192, ShowtimeId = 100017, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200193, ShowtimeId = 100017, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200194, ShowtimeId = 100017, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200195, ShowtimeId = 100017, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200196, ShowtimeId = 100017, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200197, ShowtimeId = 100017, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200198, ShowtimeId = 100017, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200199, ShowtimeId = 100017, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200200, ShowtimeId = 100018, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200201, ShowtimeId = 100018, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200202, ShowtimeId = 100018, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200203, ShowtimeId = 100018, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200204, ShowtimeId = 100018, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200205, ShowtimeId = 100018, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200206, ShowtimeId = 100018, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200207, ShowtimeId = 100018, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200208, ShowtimeId = 100019, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200209, ShowtimeId = 100019, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200210, ShowtimeId = 100019, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200211, ShowtimeId = 100019, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200212, ShowtimeId = 100019, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200213, ShowtimeId = 100019, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200214, ShowtimeId = 100019, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200215, ShowtimeId = 100019, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200216, ShowtimeId = 100020, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200217, ShowtimeId = 100020, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200218, ShowtimeId = 100020, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200219, ShowtimeId = 100020, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200220, ShowtimeId = 100020, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200221, ShowtimeId = 100020, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200222, ShowtimeId = 100020, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200223, ShowtimeId = 100020, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200224, ShowtimeId = 100020, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200225, ShowtimeId = 100020, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200226, ShowtimeId = 100020, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200227, ShowtimeId = 100020, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200228, ShowtimeId = 100020, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200229, ShowtimeId = 100020, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200230, ShowtimeId = 100020, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200231, ShowtimeId = 100021, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200232, ShowtimeId = 100021, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200233, ShowtimeId = 100021, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200234, ShowtimeId = 100021, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200235, ShowtimeId = 100021, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200236, ShowtimeId = 100021, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200237, ShowtimeId = 100021, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200238, ShowtimeId = 100021, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200239, ShowtimeId = 100021, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200240, ShowtimeId = 100021, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200241, ShowtimeId = 100021, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200242, ShowtimeId = 100021, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200243, ShowtimeId = 100021, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200244, ShowtimeId = 100021, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200245, ShowtimeId = 100021, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200246, ShowtimeId = 100022, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200247, ShowtimeId = 100022, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200248, ShowtimeId = 100022, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200249, ShowtimeId = 100022, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200250, ShowtimeId = 100022, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200251, ShowtimeId = 100022, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200252, ShowtimeId = 100022, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200253, ShowtimeId = 100022, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200254, ShowtimeId = 100023, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200255, ShowtimeId = 100023, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200256, ShowtimeId = 100023, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200257, ShowtimeId = 100023, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200258, ShowtimeId = 100023, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200259, ShowtimeId = 100023, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200260, ShowtimeId = 100023, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200261, ShowtimeId = 100023, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200262, ShowtimeId = 100024, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200263, ShowtimeId = 100024, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200264, ShowtimeId = 100024, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200265, ShowtimeId = 100024, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200266, ShowtimeId = 100024, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200267, ShowtimeId = 100024, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200268, ShowtimeId = 100024, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200269, ShowtimeId = 100024, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200270, ShowtimeId = 100024, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200271, ShowtimeId = 100024, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200272, ShowtimeId = 100024, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200273, ShowtimeId = 100024, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200274, ShowtimeId = 100024, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200275, ShowtimeId = 100024, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200276, ShowtimeId = 100024, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200277, ShowtimeId = 100025, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200278, ShowtimeId = 100025, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200279, ShowtimeId = 100025, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200280, ShowtimeId = 100025, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200281, ShowtimeId = 100025, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200282, ShowtimeId = 100025, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200283, ShowtimeId = 100025, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200284, ShowtimeId = 100025, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200285, ShowtimeId = 100025, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200286, ShowtimeId = 100025, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200287, ShowtimeId = 100025, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200288, ShowtimeId = 100025, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200289, ShowtimeId = 100025, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200290, ShowtimeId = 100025, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200291, ShowtimeId = 100025, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200292, ShowtimeId = 100026, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200293, ShowtimeId = 100026, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200294, ShowtimeId = 100026, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200295, ShowtimeId = 100026, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200296, ShowtimeId = 100026, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200297, ShowtimeId = 100026, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200298, ShowtimeId = 100026, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200299, ShowtimeId = 100026, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200300, ShowtimeId = 100027, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200301, ShowtimeId = 100027, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200302, ShowtimeId = 100027, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200303, ShowtimeId = 100027, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200304, ShowtimeId = 100027, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200305, ShowtimeId = 100027, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200306, ShowtimeId = 100027, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200307, ShowtimeId = 100027, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200308, ShowtimeId = 100028, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200309, ShowtimeId = 100028, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200310, ShowtimeId = 100028, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200311, ShowtimeId = 100028, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200312, ShowtimeId = 100028, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200313, ShowtimeId = 100028, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200314, ShowtimeId = 100028, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200315, ShowtimeId = 100028, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200316, ShowtimeId = 100028, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200317, ShowtimeId = 100028, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200318, ShowtimeId = 100028, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200319, ShowtimeId = 100028, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200320, ShowtimeId = 100028, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200321, ShowtimeId = 100028, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200322, ShowtimeId = 100028, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200323, ShowtimeId = 100029, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200324, ShowtimeId = 100029, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200325, ShowtimeId = 100029, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200326, ShowtimeId = 100029, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200327, ShowtimeId = 100029, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200328, ShowtimeId = 100029, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200329, ShowtimeId = 100029, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200330, ShowtimeId = 100029, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200331, ShowtimeId = 100029, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200332, ShowtimeId = 100029, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200333, ShowtimeId = 100029, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200334, ShowtimeId = 100029, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200335, ShowtimeId = 100029, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200336, ShowtimeId = 100029, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200337, ShowtimeId = 100029, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200338, ShowtimeId = 100030, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200339, ShowtimeId = 100030, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200340, ShowtimeId = 100030, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200341, ShowtimeId = 100030, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200342, ShowtimeId = 100030, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200343, ShowtimeId = 100030, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200344, ShowtimeId = 100030, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200345, ShowtimeId = 100030, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200346, ShowtimeId = 100031, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200347, ShowtimeId = 100031, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200348, ShowtimeId = 100031, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200349, ShowtimeId = 100031, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200350, ShowtimeId = 100031, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200351, ShowtimeId = 100031, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200352, ShowtimeId = 100031, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200353, ShowtimeId = 100031, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200354, ShowtimeId = 100032, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200355, ShowtimeId = 100032, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200356, ShowtimeId = 100032, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200357, ShowtimeId = 100032, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200358, ShowtimeId = 100032, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200359, ShowtimeId = 100032, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200360, ShowtimeId = 100032, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200361, ShowtimeId = 100032, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200362, ShowtimeId = 100032, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200363, ShowtimeId = 100032, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200364, ShowtimeId = 100032, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200365, ShowtimeId = 100032, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200366, ShowtimeId = 100032, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200367, ShowtimeId = 100032, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200368, ShowtimeId = 100032, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200369, ShowtimeId = 100033, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200370, ShowtimeId = 100033, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200371, ShowtimeId = 100033, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200372, ShowtimeId = 100033, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200373, ShowtimeId = 100033, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200374, ShowtimeId = 100033, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200375, ShowtimeId = 100033, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200376, ShowtimeId = 100033, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200377, ShowtimeId = 100033, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200378, ShowtimeId = 100033, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200379, ShowtimeId = 100033, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200380, ShowtimeId = 100033, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200381, ShowtimeId = 100033, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200382, ShowtimeId = 100033, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200383, ShowtimeId = 100033, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200384, ShowtimeId = 100034, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200385, ShowtimeId = 100034, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200386, ShowtimeId = 100034, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200387, ShowtimeId = 100034, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200388, ShowtimeId = 100034, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200389, ShowtimeId = 100034, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200390, ShowtimeId = 100034, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200391, ShowtimeId = 100034, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200392, ShowtimeId = 100035, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200393, ShowtimeId = 100035, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200394, ShowtimeId = 100035, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200395, ShowtimeId = 100035, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200396, ShowtimeId = 100035, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200397, ShowtimeId = 100035, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200398, ShowtimeId = 100035, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200399, ShowtimeId = 100035, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200400, ShowtimeId = 100036, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200401, ShowtimeId = 100036, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200402, ShowtimeId = 100036, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200403, ShowtimeId = 100036, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200404, ShowtimeId = 100036, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200405, ShowtimeId = 100036, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200406, ShowtimeId = 100036, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200407, ShowtimeId = 100036, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200408, ShowtimeId = 100036, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200409, ShowtimeId = 100036, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200410, ShowtimeId = 100036, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200411, ShowtimeId = 100036, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200412, ShowtimeId = 100036, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200413, ShowtimeId = 100036, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200414, ShowtimeId = 100036, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200415, ShowtimeId = 100037, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200416, ShowtimeId = 100037, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200417, ShowtimeId = 100037, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200418, ShowtimeId = 100037, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200419, ShowtimeId = 100037, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200420, ShowtimeId = 100037, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200421, ShowtimeId = 100037, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200422, ShowtimeId = 100037, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200423, ShowtimeId = 100037, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200424, ShowtimeId = 100037, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200425, ShowtimeId = 100037, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200426, ShowtimeId = 100037, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200427, ShowtimeId = 100037, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200428, ShowtimeId = 100037, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200429, ShowtimeId = 100037, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200430, ShowtimeId = 100038, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200431, ShowtimeId = 100038, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200432, ShowtimeId = 100038, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200433, ShowtimeId = 100038, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200434, ShowtimeId = 100038, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200435, ShowtimeId = 100038, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200436, ShowtimeId = 100038, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200437, ShowtimeId = 100038, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200438, ShowtimeId = 100039, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200439, ShowtimeId = 100039, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200440, ShowtimeId = 100039, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200441, ShowtimeId = 100039, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200442, ShowtimeId = 100039, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200443, ShowtimeId = 100039, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200444, ShowtimeId = 100039, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200445, ShowtimeId = 100039, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200446, ShowtimeId = 100040, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200447, ShowtimeId = 100040, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200448, ShowtimeId = 100040, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200449, ShowtimeId = 100040, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200450, ShowtimeId = 100040, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200451, ShowtimeId = 100040, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200452, ShowtimeId = 100040, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200453, ShowtimeId = 100040, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200454, ShowtimeId = 100040, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200455, ShowtimeId = 100040, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200456, ShowtimeId = 100040, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200457, ShowtimeId = 100040, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200458, ShowtimeId = 100040, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200459, ShowtimeId = 100040, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200460, ShowtimeId = 100040, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200461, ShowtimeId = 100041, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200462, ShowtimeId = 100041, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200463, ShowtimeId = 100041, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200464, ShowtimeId = 100041, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200465, ShowtimeId = 100041, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200466, ShowtimeId = 100041, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200467, ShowtimeId = 100041, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200468, ShowtimeId = 100041, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200469, ShowtimeId = 100041, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200470, ShowtimeId = 100041, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200471, ShowtimeId = 100041, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200472, ShowtimeId = 100041, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200473, ShowtimeId = 100041, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200474, ShowtimeId = 100041, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200475, ShowtimeId = 100041, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200476, ShowtimeId = 100042, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200477, ShowtimeId = 100042, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200478, ShowtimeId = 100042, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200479, ShowtimeId = 100042, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200480, ShowtimeId = 100042, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200481, ShowtimeId = 100042, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200482, ShowtimeId = 100042, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200483, ShowtimeId = 100042, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200484, ShowtimeId = 100043, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200485, ShowtimeId = 100043, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200486, ShowtimeId = 100043, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200487, ShowtimeId = 100043, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200488, ShowtimeId = 100043, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200489, ShowtimeId = 100043, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200490, ShowtimeId = 100043, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200491, ShowtimeId = 100043, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200492, ShowtimeId = 100044, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200493, ShowtimeId = 100044, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200494, ShowtimeId = 100044, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200495, ShowtimeId = 100044, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200496, ShowtimeId = 100044, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200497, ShowtimeId = 100044, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200498, ShowtimeId = 100044, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200499, ShowtimeId = 100044, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200500, ShowtimeId = 100044, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200501, ShowtimeId = 100044, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200502, ShowtimeId = 100044, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200503, ShowtimeId = 100044, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200504, ShowtimeId = 100044, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200505, ShowtimeId = 100044, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200506, ShowtimeId = 100044, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200507, ShowtimeId = 100045, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200508, ShowtimeId = 100045, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200509, ShowtimeId = 100045, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200510, ShowtimeId = 100045, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200511, ShowtimeId = 100045, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200512, ShowtimeId = 100045, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200513, ShowtimeId = 100045, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200514, ShowtimeId = 100045, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200515, ShowtimeId = 100045, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200516, ShowtimeId = 100045, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200517, ShowtimeId = 100045, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200518, ShowtimeId = 100045, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200519, ShowtimeId = 100045, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200520, ShowtimeId = 100045, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200521, ShowtimeId = 100045, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200522, ShowtimeId = 100046, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200523, ShowtimeId = 100046, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200524, ShowtimeId = 100046, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200525, ShowtimeId = 100046, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200526, ShowtimeId = 100046, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200527, ShowtimeId = 100046, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200528, ShowtimeId = 100046, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200529, ShowtimeId = 100046, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200530, ShowtimeId = 100047, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200531, ShowtimeId = 100047, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200532, ShowtimeId = 100047, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200533, ShowtimeId = 100047, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200534, ShowtimeId = 100047, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200535, ShowtimeId = 100047, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200536, ShowtimeId = 100047, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200537, ShowtimeId = 100047, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200538, ShowtimeId = 100048, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200539, ShowtimeId = 100048, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200540, ShowtimeId = 100048, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200541, ShowtimeId = 100048, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200542, ShowtimeId = 100048, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200543, ShowtimeId = 100048, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200544, ShowtimeId = 100048, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200545, ShowtimeId = 100048, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200546, ShowtimeId = 100048, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200547, ShowtimeId = 100048, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200548, ShowtimeId = 100048, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200549, ShowtimeId = 100048, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200550, ShowtimeId = 100048, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200551, ShowtimeId = 100048, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200552, ShowtimeId = 100048, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200553, ShowtimeId = 100049, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200554, ShowtimeId = 100049, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200555, ShowtimeId = 100049, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200556, ShowtimeId = 100049, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200557, ShowtimeId = 100049, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200558, ShowtimeId = 100049, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200559, ShowtimeId = 100049, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200560, ShowtimeId = 100049, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200561, ShowtimeId = 100049, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200562, ShowtimeId = 100049, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200563, ShowtimeId = 100049, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200564, ShowtimeId = 100049, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200565, ShowtimeId = 100049, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200566, ShowtimeId = 100049, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200567, ShowtimeId = 100049, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200568, ShowtimeId = 100050, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200569, ShowtimeId = 100050, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200570, ShowtimeId = 100050, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200571, ShowtimeId = 100050, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200572, ShowtimeId = 100050, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200573, ShowtimeId = 100050, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200574, ShowtimeId = 100050, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200575, ShowtimeId = 100050, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200576, ShowtimeId = 100051, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200577, ShowtimeId = 100051, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200578, ShowtimeId = 100051, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200579, ShowtimeId = 100051, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200580, ShowtimeId = 100051, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200581, ShowtimeId = 100051, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200582, ShowtimeId = 100051, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200583, ShowtimeId = 100051, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200584, ShowtimeId = 100052, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200585, ShowtimeId = 100052, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200586, ShowtimeId = 100052, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200587, ShowtimeId = 100052, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200588, ShowtimeId = 100052, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200589, ShowtimeId = 100052, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200590, ShowtimeId = 100052, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200591, ShowtimeId = 100052, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200592, ShowtimeId = 100052, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200593, ShowtimeId = 100052, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200594, ShowtimeId = 100052, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200595, ShowtimeId = 100052, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200596, ShowtimeId = 100052, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200597, ShowtimeId = 100052, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200598, ShowtimeId = 100052, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200599, ShowtimeId = 100053, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200600, ShowtimeId = 100053, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200601, ShowtimeId = 100053, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200602, ShowtimeId = 100053, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200603, ShowtimeId = 100053, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200604, ShowtimeId = 100053, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200605, ShowtimeId = 100053, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200606, ShowtimeId = 100053, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200607, ShowtimeId = 100053, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200608, ShowtimeId = 100053, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200609, ShowtimeId = 100053, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200610, ShowtimeId = 100053, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200611, ShowtimeId = 100053, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200612, ShowtimeId = 100053, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200613, ShowtimeId = 100053, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200614, ShowtimeId = 100054, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200615, ShowtimeId = 100054, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200616, ShowtimeId = 100054, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200617, ShowtimeId = 100054, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200618, ShowtimeId = 100054, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200619, ShowtimeId = 100054, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200620, ShowtimeId = 100054, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200621, ShowtimeId = 100054, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200622, ShowtimeId = 100055, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200623, ShowtimeId = 100055, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200624, ShowtimeId = 100055, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200625, ShowtimeId = 100055, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200626, ShowtimeId = 100055, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200627, ShowtimeId = 100055, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200628, ShowtimeId = 100055, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200629, ShowtimeId = 100055, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200630, ShowtimeId = 100056, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200631, ShowtimeId = 100056, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200632, ShowtimeId = 100056, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200633, ShowtimeId = 100056, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200634, ShowtimeId = 100056, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200635, ShowtimeId = 100056, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200636, ShowtimeId = 100056, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200637, ShowtimeId = 100056, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200638, ShowtimeId = 100056, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200639, ShowtimeId = 100056, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200640, ShowtimeId = 100056, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200641, ShowtimeId = 100056, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200642, ShowtimeId = 100056, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200643, ShowtimeId = 100056, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200644, ShowtimeId = 100056, SeatId = 15, Status = "Trống" }
        );

        // ---- Suất chiếu cuối tuần này: tối Thứ 7 (08/08/2026) + cả ngày Chủ nhật (09/08/2026) ----
        modelBuilder.Entity<Showtime>().HasData(
            new Showtime { ShowtimeId = 100057, MovieId = 6, RoomId = 1, StartTime = new DateTime(2026,8,8,17,0,0), EndTime = new DateTime(2026,8,8,18,43,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100058, MovieId = 7, RoomId = 2, StartTime = new DateTime(2026,8,8,17,0,0), EndTime = new DateTime(2026,8,8,19,10,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100059, MovieId = 10, RoomId = 1, StartTime = new DateTime(2026,8,8,19,3,0), EndTime = new DateTime(2026,8,8,20,49,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100060, MovieId = 11, RoomId = 2, StartTime = new DateTime(2026,8,8,19,30,0), EndTime = new DateTime(2026,8,8,21,51,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100061, MovieId = 13, RoomId = 1, StartTime = new DateTime(2026,8,8,21,9,0), EndTime = new DateTime(2026,8,8,23,14,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100062, MovieId = 18, RoomId = 2, StartTime = new DateTime(2026,8,8,22,11,0), EndTime = new DateTime(2026,8,8,23,45,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100063, MovieId = 6, RoomId = 1, StartTime = new DateTime(2026,8,9,9,0,0), EndTime = new DateTime(2026,8,9,10,43,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100064, MovieId = 7, RoomId = 2, StartTime = new DateTime(2026,8,9,9,0,0), EndTime = new DateTime(2026,8,9,11,10,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100065, MovieId = 10, RoomId = 1, StartTime = new DateTime(2026,8,9,11,3,0), EndTime = new DateTime(2026,8,9,12,49,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100066, MovieId = 11, RoomId = 2, StartTime = new DateTime(2026,8,9,11,30,0), EndTime = new DateTime(2026,8,9,13,51,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100067, MovieId = 13, RoomId = 1, StartTime = new DateTime(2026,8,9,13,9,0), EndTime = new DateTime(2026,8,9,15,14,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100068, MovieId = 18, RoomId = 2, StartTime = new DateTime(2026,8,9,14,11,0), EndTime = new DateTime(2026,8,9,15,45,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100069, MovieId = 21, RoomId = 1, StartTime = new DateTime(2026,8,9,15,34,0), EndTime = new DateTime(2026,8,9,17,28,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100070, MovieId = 25, RoomId = 2, StartTime = new DateTime(2026,8,9,16,5,0), EndTime = new DateTime(2026,8,9,17,41,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100071, MovieId = 26, RoomId = 1, StartTime = new DateTime(2026,8,9,17,48,0), EndTime = new DateTime(2026,8,9,20,34,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100072, MovieId = 28, RoomId = 2, StartTime = new DateTime(2026,8,9,18,1,0), EndTime = new DateTime(2026,8,9,20,0,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100073, MovieId = 29, RoomId = 1, StartTime = new DateTime(2026,8,9,20,54,0), EndTime = new DateTime(2026,8,9,23,7,0), TicketPrice = 75000m, Status = "Đang chiếu" },
            new Showtime { ShowtimeId = 100074, MovieId = 30, RoomId = 2, StartTime = new DateTime(2026,8,9,20,20,0), EndTime = new DateTime(2026,8,9,22,41,0), TicketPrice = 75000m, Status = "Đang chiếu" }
        );

        modelBuilder.Entity<ShowtimeSeat>().HasData(
            new ShowtimeSeat { ShowtimeSeatId = 200645, ShowtimeId = 100057, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200646, ShowtimeId = 100057, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200647, ShowtimeId = 100057, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200648, ShowtimeId = 100057, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200649, ShowtimeId = 100057, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200650, ShowtimeId = 100057, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200651, ShowtimeId = 100057, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200652, ShowtimeId = 100057, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200653, ShowtimeId = 100057, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200654, ShowtimeId = 100057, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200655, ShowtimeId = 100057, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200656, ShowtimeId = 100057, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200657, ShowtimeId = 100057, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200658, ShowtimeId = 100057, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200659, ShowtimeId = 100057, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200660, ShowtimeId = 100058, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200661, ShowtimeId = 100058, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200662, ShowtimeId = 100058, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200663, ShowtimeId = 100058, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200664, ShowtimeId = 100058, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200665, ShowtimeId = 100058, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200666, ShowtimeId = 100058, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200667, ShowtimeId = 100058, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200668, ShowtimeId = 100059, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200669, ShowtimeId = 100059, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200670, ShowtimeId = 100059, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200671, ShowtimeId = 100059, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200672, ShowtimeId = 100059, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200673, ShowtimeId = 100059, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200674, ShowtimeId = 100059, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200675, ShowtimeId = 100059, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200676, ShowtimeId = 100059, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200677, ShowtimeId = 100059, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200678, ShowtimeId = 100059, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200679, ShowtimeId = 100059, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200680, ShowtimeId = 100059, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200681, ShowtimeId = 100059, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200682, ShowtimeId = 100059, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200683, ShowtimeId = 100060, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200684, ShowtimeId = 100060, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200685, ShowtimeId = 100060, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200686, ShowtimeId = 100060, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200687, ShowtimeId = 100060, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200688, ShowtimeId = 100060, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200689, ShowtimeId = 100060, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200690, ShowtimeId = 100060, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200691, ShowtimeId = 100061, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200692, ShowtimeId = 100061, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200693, ShowtimeId = 100061, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200694, ShowtimeId = 100061, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200695, ShowtimeId = 100061, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200696, ShowtimeId = 100061, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200697, ShowtimeId = 100061, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200698, ShowtimeId = 100061, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200699, ShowtimeId = 100061, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200700, ShowtimeId = 100061, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200701, ShowtimeId = 100061, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200702, ShowtimeId = 100061, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200703, ShowtimeId = 100061, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200704, ShowtimeId = 100061, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200705, ShowtimeId = 100061, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200706, ShowtimeId = 100062, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200707, ShowtimeId = 100062, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200708, ShowtimeId = 100062, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200709, ShowtimeId = 100062, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200710, ShowtimeId = 100062, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200711, ShowtimeId = 100062, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200712, ShowtimeId = 100062, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200713, ShowtimeId = 100062, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200714, ShowtimeId = 100063, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200715, ShowtimeId = 100063, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200716, ShowtimeId = 100063, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200717, ShowtimeId = 100063, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200718, ShowtimeId = 100063, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200719, ShowtimeId = 100063, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200720, ShowtimeId = 100063, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200721, ShowtimeId = 100063, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200722, ShowtimeId = 100063, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200723, ShowtimeId = 100063, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200724, ShowtimeId = 100063, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200725, ShowtimeId = 100063, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200726, ShowtimeId = 100063, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200727, ShowtimeId = 100063, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200728, ShowtimeId = 100063, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200729, ShowtimeId = 100064, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200730, ShowtimeId = 100064, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200731, ShowtimeId = 100064, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200732, ShowtimeId = 100064, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200733, ShowtimeId = 100064, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200734, ShowtimeId = 100064, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200735, ShowtimeId = 100064, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200736, ShowtimeId = 100064, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200737, ShowtimeId = 100065, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200738, ShowtimeId = 100065, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200739, ShowtimeId = 100065, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200740, ShowtimeId = 100065, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200741, ShowtimeId = 100065, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200742, ShowtimeId = 100065, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200743, ShowtimeId = 100065, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200744, ShowtimeId = 100065, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200745, ShowtimeId = 100065, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200746, ShowtimeId = 100065, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200747, ShowtimeId = 100065, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200748, ShowtimeId = 100065, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200749, ShowtimeId = 100065, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200750, ShowtimeId = 100065, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200751, ShowtimeId = 100065, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200752, ShowtimeId = 100066, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200753, ShowtimeId = 100066, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200754, ShowtimeId = 100066, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200755, ShowtimeId = 100066, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200756, ShowtimeId = 100066, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200757, ShowtimeId = 100066, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200758, ShowtimeId = 100066, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200759, ShowtimeId = 100066, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200760, ShowtimeId = 100067, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200761, ShowtimeId = 100067, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200762, ShowtimeId = 100067, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200763, ShowtimeId = 100067, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200764, ShowtimeId = 100067, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200765, ShowtimeId = 100067, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200766, ShowtimeId = 100067, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200767, ShowtimeId = 100067, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200768, ShowtimeId = 100067, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200769, ShowtimeId = 100067, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200770, ShowtimeId = 100067, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200771, ShowtimeId = 100067, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200772, ShowtimeId = 100067, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200773, ShowtimeId = 100067, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200774, ShowtimeId = 100067, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200775, ShowtimeId = 100068, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200776, ShowtimeId = 100068, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200777, ShowtimeId = 100068, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200778, ShowtimeId = 100068, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200779, ShowtimeId = 100068, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200780, ShowtimeId = 100068, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200781, ShowtimeId = 100068, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200782, ShowtimeId = 100068, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200783, ShowtimeId = 100069, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200784, ShowtimeId = 100069, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200785, ShowtimeId = 100069, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200786, ShowtimeId = 100069, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200787, ShowtimeId = 100069, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200788, ShowtimeId = 100069, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200789, ShowtimeId = 100069, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200790, ShowtimeId = 100069, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200791, ShowtimeId = 100069, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200792, ShowtimeId = 100069, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200793, ShowtimeId = 100069, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200794, ShowtimeId = 100069, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200795, ShowtimeId = 100069, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200796, ShowtimeId = 100069, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200797, ShowtimeId = 100069, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200798, ShowtimeId = 100070, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200799, ShowtimeId = 100070, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200800, ShowtimeId = 100070, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200801, ShowtimeId = 100070, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200802, ShowtimeId = 100070, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200803, ShowtimeId = 100070, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200804, ShowtimeId = 100070, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200805, ShowtimeId = 100070, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200806, ShowtimeId = 100071, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200807, ShowtimeId = 100071, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200808, ShowtimeId = 100071, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200809, ShowtimeId = 100071, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200810, ShowtimeId = 100071, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200811, ShowtimeId = 100071, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200812, ShowtimeId = 100071, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200813, ShowtimeId = 100071, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200814, ShowtimeId = 100071, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200815, ShowtimeId = 100071, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200816, ShowtimeId = 100071, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200817, ShowtimeId = 100071, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200818, ShowtimeId = 100071, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200819, ShowtimeId = 100071, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200820, ShowtimeId = 100071, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200821, ShowtimeId = 100072, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200822, ShowtimeId = 100072, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200823, ShowtimeId = 100072, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200824, ShowtimeId = 100072, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200825, ShowtimeId = 100072, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200826, ShowtimeId = 100072, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200827, ShowtimeId = 100072, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200828, ShowtimeId = 100072, SeatId = 23, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200829, ShowtimeId = 100073, SeatId = 1, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200830, ShowtimeId = 100073, SeatId = 2, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200831, ShowtimeId = 100073, SeatId = 3, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200832, ShowtimeId = 100073, SeatId = 4, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200833, ShowtimeId = 100073, SeatId = 5, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200834, ShowtimeId = 100073, SeatId = 6, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200835, ShowtimeId = 100073, SeatId = 7, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200836, ShowtimeId = 100073, SeatId = 8, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200837, ShowtimeId = 100073, SeatId = 9, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200838, ShowtimeId = 100073, SeatId = 10, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200839, ShowtimeId = 100073, SeatId = 11, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200840, ShowtimeId = 100073, SeatId = 12, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200841, ShowtimeId = 100073, SeatId = 13, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200842, ShowtimeId = 100073, SeatId = 14, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200843, ShowtimeId = 100073, SeatId = 15, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200844, ShowtimeId = 100074, SeatId = 16, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200845, ShowtimeId = 100074, SeatId = 17, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200846, ShowtimeId = 100074, SeatId = 18, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200847, ShowtimeId = 100074, SeatId = 19, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200848, ShowtimeId = 100074, SeatId = 20, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200849, ShowtimeId = 100074, SeatId = 21, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200850, ShowtimeId = 100074, SeatId = 22, Status = "Trống" },
            new ShowtimeSeat { ShowtimeSeatId = 200851, ShowtimeId = 100074, SeatId = 23, Status = "Trống" }
        );
    }
}

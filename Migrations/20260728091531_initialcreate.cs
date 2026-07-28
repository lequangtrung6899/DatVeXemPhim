using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    ComboId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComboName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.ComboId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LoyaltyPoint = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinOrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageLimit = table.Column<int>(type: "int", nullable: false),
                    UsedCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.VoucherId);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    RowLabel = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ColumnNumber = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Showtimes",
                columns: table => new
                {
                    ShowtimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.ShowtimeId);
                    table.ForeignKey(
                        name: "FK_Showtimes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Showtimes_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShowtimeSeats",
                columns: table => new
                {
                    ShowtimeSeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HoldExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowtimeSeats", x => x.ShowtimeSeatId);
                    table.ForeignKey(
                        name: "FK_ShowtimeSeats_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShowtimeSeats_Showtimes_ShowtimeId",
                        column: x => x.ShowtimeId,
                        principalTable: "Showtimes",
                        principalColumn: "ShowtimeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Showtimes_ShowtimeId",
                        column: x => x.ShowtimeId,
                        principalTable: "Showtimes",
                        principalColumn: "ShowtimeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransactionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketCombos",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ComboId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCombos", x => new { x.TicketId, x.ComboId });
                    table.ForeignKey(
                        name: "FK_TicketCombos_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "ComboId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketCombos_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    TicketDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ShowtimeSeatId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetails", x => x.TicketDetailId);
                    table.ForeignKey(
                        name: "FK_TicketDetails_ShowtimeSeats_ShowtimeSeatId",
                        column: x => x.ShowtimeSeatId,
                        principalTable: "ShowtimeSeats",
                        principalColumn: "ShowtimeSeatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "ComboId", "ComboName", "Description", "IsActive", "Price" },
                values: new object[,]
                {
                    { 1, "Combo 1: Bắp lớn + Nước lớn", "1 bắp rang bơ lớn + 1 nước ngọt lớn", true, 89000.00m },
                    { 2, "Combo 2: Bắp nhỏ + 2 Nước", "1 bắp rang bơ nhỏ + 2 nước ngọt vừa", true, 79000.00m }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CreatedAt", "Email", "FullName", "IsActive", "LoyaltyPoint", "MembershipRank", "PasswordHash", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "levanan@gmail.com", "Lê Văn An", true, 150, "Thành viên Bạc", "$2a$hash_cust01", "0911111111" },
                    { 2, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "phambinh@gmail.com", "Phạm Thị Bình", true, 0, "Thành viên mới", "$2a$hash_cust02", "0922222222" },
                    { 3, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "hoangchau@gmail.com", "Hoàng Minh Châu", true, 500, "Thành viên Vàng", "$2a$hash_cust03", "0933333333" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Hành động" },
                    { 2, "Tình cảm" },
                    { 3, "Kinh dị" },
                    { 4, "Hoạt hình" },
                    { 5, "Hài hước" },
                    { 6, "Viễn tưởng" },
                    { 7, "Tài liệu" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "CreatedAt", "Description", "Duration", "EndDate", "PosterUrl", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Ethan Hunt và đội IMF đối mặt nhiệm vụ nguy hiểm nhất sự nghiệp trong phần cuối của loạt phim gián điệp hành động kinh điển.", 170, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/mission-impossible-the-final-reckoning.jpg", new DateTime(2026, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngừng chiếu", "Mission: Impossible – The Final Reckoning" },
                    { 2, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai cảnh sát Miami Mike Lowrey và Marcus Burnett phải chạy đua để minh oan cho người chỉ huy quá cố của mình.", 115, null, "/posters/bad-boys-ride-or-die.jpg", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Bad Boys: Ride or Die" },
                    { 3, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một diễn viên đóng thế phải điều tra vụ mất tích của ngôi sao điện ảnh trong lúc cố gắng hàn gắn chuyện tình cũ.", 126, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/the-fall-guy.jpg", new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngừng chiếu", "The Fall Guy" },
                    { 4, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Câu chuyện về tuổi trẻ của Furiosa trong thế giới hậu tận thế khắc nghiệt của vũ trụ Mad Max.", 148, null, "/posters/furiosa-a-mad-max-saga.jpg", new DateTime(2026, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Furiosa: A Mad Max Saga" },
                    { 5, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhóm thợ săn bão liều lĩnh đối đầu với những cơn lốc xoáy ngày càng khốc liệt ở vùng Trung Tây nước Mỹ.", 122, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/twisters.jpg", new DateTime(2026, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngừng chiếu", "Twisters" },
                    { 6, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai người từng có một đêm hẹn hò tuyệt vời rồi trở mặt bất ngờ, buộc phải giả vờ yêu nhau tại một đám cưới ở Úc.", 103, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/anyone-but-you.jpg", new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Anyone but You" },
                    { 7, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một người phụ nữ trẻ phải đối mặt với những lựa chọn khó khăn khi tình yêu và quá khứ đau buồn đan xen.", 130, new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/it-ends-with-us.jpg", new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "It Ends with Us" },
                    { 8, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một cặp đôi cùng nhau trải qua những cột mốc vui buồn của cuộc sống, tình yêu và bệnh tật.", 108, new DateTime(2026, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/we-live-in-time.jpg", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngừng chiếu", "We Live in Time" },
                    { 9, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một người mẹ đơn thân bất ngờ nảy sinh tình cảm với chàng ca sĩ trẻ của một ban nhạc nổi tiếng.", 115, null, "/posters/the-idea-of-you.jpg", new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "The Idea of You" },
                    { 10, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai người bạn thời thơ ấu tái ngộ sau nhiều năm xa cách, đối diện với những gì có thể đã xảy ra.", 106, new DateTime(2026, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/past-lives.jpg", new DateTime(2026, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Past Lives" },
                    { 11, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một ngôi sao đang lụi tàn sử dụng loại thuốc bí ẩn để tạo ra phiên bản trẻ trung hơn của chính mình, với cái giá khủng khiếp.", 141, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/the-substance.jpg", new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "The Substance" },
                    { 12, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một ngôi sao nhạc pop phải đối mặt với những sự kiện ngày càng đáng sợ khi thực tại bắt đầu sụp đổ quanh cô.", 127, null, "/posters/smile-2.jpg", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Smile 2" },
                    { 13, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Gã hề sát nhân Art the Clown trở lại gieo rắc kinh hoàng trong đêm Giáng sinh.", 125, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/terrifier-3.jpg", new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Terrifier 3" },
                    { 14, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Ba thế hệ trong gia đình Deetz vô tình mở lại cánh cổng dẫn đến thế giới của hồn ma Beetlejuice.", 105, null, "/posters/beetlejuice-beetlejuice.jpg", new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Beetlejuice Beetlejuice" },
                    { 15, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một đặc vụ FBI điều tra loạt án mạng liên quan đến các manh mối huyền bí đầy ám ảnh.", 101, null, "/posters/longlegs.jpg", new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Longlegs" },
                    { 16, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Riley bước vào tuổi dậy thì và phải đối mặt với những cảm xúc mới phức tạp hơn trong tâm trí mình.", 96, null, "/posters/inside-out-2.jpg", new DateTime(2026, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Inside Out 2" },
                    { 17, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Moana lên đường trong một chuyến hải trình mới đầy thử thách cùng những người bạn cũ và mới.", 100, null, "/posters/moana-2.jpg", new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Moana 2" },
                    { 18, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Gru phải bảo vệ gia đình mới của mình trước một kẻ thù cũ đầy nguy hiểm.", 94, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/despicable-me-4.jpg", new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Despicable Me 4" },
                    { 19, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một robot bị mắc kẹt trên hòn đảo hoang phải học cách sinh tồn và trở thành người mẹ nuôi của một chú ngỗng con.", 102, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/the-wild-robot.jpg", new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngừng chiếu", "The Wild Robot" },
                    { 20, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Po phải tìm người kế nhiệm làm Rồng Chiến Binh trong khi đối mặt với một pháp sư biến hình nguy hiểm.", 94, new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/kung-fu-panda-4.jpg", new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngừng chiếu", "Kung Fu Panda 4" },
                    { 21, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Barbie rời khỏi thế giới hoàn hảo của mình để khám phá thế giới thực đầy bất ngờ.", 114, new DateTime(2026, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/barbie.jpg", new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Barbie" },
                    { 22, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một phụ nữ được thuê để giúp một chàng trai nhút nhát tự tin hơn trước khi vào đại học.", 103, null, "/posters/no-hard-feelings.jpg", new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "No Hard Feelings" },
                    { 23, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nữ tiểu thuyết gia phát hiện cốt truyện trong sách của mình đang trở thành sự thật ngoài đời.", 139, null, "/posters/argylle.jpg", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Argylle" },
                    { 24, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhóm bạn trẻ phải sống sót qua đêm giao thừa thiên niên kỷ khi máy móc nổi loạn.", 93, null, "/posters/y2k.jpg", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Y2K" },
                    { 25, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một phụ nữ ở độ tuổi 30 bắt đầu hành trình khám phá lại chính bản thân mình.", 96, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/am-i-ok.jpg", new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Am I OK?" },
                    { 26, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Paul Atreides hợp lực cùng người Fremen trên hành trình trả thù và định đoạt số phận cả vũ trụ.", 166, new DateTime(2026, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/dune-part-two.jpg", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Dune: Part Two" },
                    { 27, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai quái vật huyền thoại Godzilla và Kong buộc phải bắt tay chống lại một mối đe dọa ẩn giấu.", 115, null, "/posters/godzilla-x-kong-the-new-empire.jpg", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "Godzilla x Kong: The New Empire" },
                    { 28, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhóm người trẻ khai thác trạm vũ trụ bỏ hoang chạm trán sinh vật ngoài hành tinh nguy hiểm bậc nhất vũ trụ.", 119, new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/alien-romulus.jpg", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Alien: Romulus" },
                    { 29, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Trong cuộc chiến giữa loài người và trí tuệ nhân tạo, một cựu binh phát hiện vũ khí bí mật mang hình hài đứa trẻ.", 133, new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/the-creator.jpg", new DateTime(2026, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "The Creator" },
                    { 30, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một phụ nữ trẻ được hồi sinh bởi khoa học kỳ lạ và bắt đầu hành trình khám phá thế giới theo cách riêng của mình.", 141, new DateTime(2026, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/poor-things.jpg", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Poor Things" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "CreatedAt", "Description", "Duration", "EndDate", "PosterUrl", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { 31, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Ghi lại hành trình leo núi El Capitan không dây bảo hộ đầy mạo hiểm của vận động viên Alex Honnold.", 100, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/free-solo.jpg", new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ngừng chiếu", "Free Solo" },
                    { 32, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhà làm phim xây dựng mối quan hệ đặc biệt với một con bạch tuộc hoang dã ngoài khơi Nam Phi.", 85, null, "/posters/my-octopus-teacher.jpg", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "My Octopus Teacher" },
                    { 33, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Bộ phim tài liệu phân tích mối liên hệ giữa chế độ nô lệ và hệ thống nhà tù ở nước Mỹ hiện đại.", 100, null, "/posters/13th.jpg", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", "13th" },
                    { 34, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Chân dung về cuộc đời và di sản của Fred Rogers, người dẫn chương trình truyền hình thiếu nhi huyền thoại.", 94, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/won-t-you-be-my-neighbor.jpg", new DateTime(2026, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Won't You Be My Neighbor?" },
                    { 35, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Câu chuyện có thật đằng sau lễ hội âm nhạc xa hoa sụp đổ thảm hại trên mạng xã hội.", 97, new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "/posters/fyre-the-greatest-party-that-never-happened.jpg", new DateTime(2026, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", "Fyre: The Greatest Party That Never Happened" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Staff" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "IsActive", "RoomName", "TotalSeats" },
                values: new object[,]
                {
                    { 1, true, "Phòng chiếu 1", 15 },
                    { 2, true, "Phòng chiếu 2", 8 }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "VoucherId", "Code", "DiscountType", "DiscountValue", "EndDate", "IsActive", "MinOrderAmount", "StartDate", "UsageLimit", "UsedCount" },
                values: new object[,]
                {
                    { 1, "SUMMER10", "Phần trăm", 10.00m, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 100000.00m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, 5 },
                    { 2, "GIAM20K", "Số tiền cố định", 20000.00m, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 150000.00m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 0 }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 15 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 18 },
                    { 4, 19 },
                    { 4, 20 },
                    { 5, 21 },
                    { 5, 22 },
                    { 5, 23 },
                    { 5, 24 },
                    { 5, 25 },
                    { 6, 26 },
                    { 6, 27 },
                    { 6, 28 },
                    { 6, 29 },
                    { 6, 30 },
                    { 7, 31 },
                    { 7, 32 },
                    { 7, 33 },
                    { 7, 34 },
                    { 7, 35 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatId", "ColumnNumber", "RoomId", "RowLabel", "SeatType" },
                values: new object[,]
                {
                    { 1, 1, 1, "A", "Thường" },
                    { 2, 2, 1, "A", "Thường" },
                    { 3, 3, 1, "A", "Thường" },
                    { 4, 4, 1, "A", "Thường" },
                    { 5, 5, 1, "A", "Thường" },
                    { 6, 1, 1, "B", "Thường" },
                    { 7, 2, 1, "B", "Thường" }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatId", "ColumnNumber", "RoomId", "RowLabel", "SeatType" },
                values: new object[,]
                {
                    { 8, 3, 1, "B", "Thường" },
                    { 9, 4, 1, "B", "Thường" },
                    { 10, 5, 1, "B", "Thường" },
                    { 11, 1, 1, "C", "VIP" },
                    { 12, 2, 1, "C", "VIP" },
                    { 13, 3, 1, "C", "VIP" },
                    { 14, 4, 1, "C", "VIP" },
                    { 15, 5, 1, "C", "VIP" },
                    { 16, 1, 2, "A", "Thường" },
                    { 17, 2, 2, "A", "Thường" },
                    { 18, 3, 2, "A", "Thường" },
                    { 19, 4, 2, "A", "Thường" },
                    { 20, 1, 2, "B", "Đôi" },
                    { 21, 2, 2, "B", "Đôi" },
                    { 22, 3, 2, "B", "Đôi" },
                    { 23, 4, 2, "B", "Đôi" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "admin01@rapphim.vn", "Nguyễn Văn Quản", true, "$2a$hash_admin01", "0900000001", 2, "admin01" },
                    { 2, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "staff01@rapphim.vn", "Trần Thị Nhân Viên", true, "$2a$hash_staff01", "0900000002", 1, "staff01" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TicketId",
                table: "Payments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId_CustomerId",
                table: "Reviews",
                columns: new[] { "MovieId", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RoomId_RowLabel_ColumnNumber",
                table: "Seats",
                columns: new[] { "RoomId", "RowLabel", "ColumnNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtimes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_RoomId",
                table: "Showtimes",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeats_SeatId",
                table: "ShowtimeSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeats_ShowtimeId_SeatId",
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeId", "SeatId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketCombos_ComboId",
                table: "TicketCombos",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_ShowtimeSeatId",
                table: "TicketDetails",
                column: "ShowtimeSeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerId",
                table: "Tickets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShowtimeId",
                table: "Tickets",
                column: "ShowtimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VoucherId",
                table: "Tickets",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_Code",
                table: "Vouchers",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TicketCombos");

            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "ShowtimeSeats");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Showtimes");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}

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
                name: "ContactMessages",
                columns: table => new
                {
                    ContactMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.ContactMessageId);
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
                    BannerUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShowOnBanner = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SubmittedBy = table.Column<int>(type: "int", nullable: true),
                    ReviewedBy = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasPendingEdit = table.Column<bool>(type: "bit", nullable: false),
                    PendingChangesJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "PendingChanges",
                columns: table => new
                {
                    PendingChangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ChangesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubmittedBy = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReviewedBy = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingChanges", x => x.PendingChangeId);
                    table.ForeignKey(
                        name: "FK_PendingChanges_Users_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingChanges_Users_SubmittedBy",
                        column: x => x.SubmittedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
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
                    HeldBySessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                name: "RefundRequests",
                columns: table => new
                {
                    RefundRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StaffApprovedBy = table.Column<int>(type: "int", nullable: true),
                    StaffApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminApprovedBy = table.Column<int>(type: "int", nullable: true),
                    AdminApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RejectedBy = table.Column<int>(type: "int", nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundRequests", x => x.RefundRequestId);
                    table.ForeignKey(
                        name: "FK_RefundRequests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundRequests_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundRequests_Users_AdminApprovedBy",
                        column: x => x.AdminApprovedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundRequests_Users_RejectedBy",
                        column: x => x.RejectedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundRequests_Users_StaffApprovedBy",
                        column: x => x.StaffApprovedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
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
                    { 1, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "levanan@gmail.com", "Lê Văn An", true, 150, "Thành viên Bạc", "100000.4SgTxqKC1i7w9NcxXcVMug==.d1OwkzA/BzT14g9XjJUynl6I2Q8E89AWKCNl5kH24Fs=", "0911111111" },
                    { 2, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "phambinh@gmail.com", "Phạm Thị Bình", true, 0, "Thành viên mới", "100000.kxJLGbkx9ggbzb7IS9HeXA==.+y/EWaZsaqW7VX9EkiBhxYCZrYAKVhvyy1KOlQeYAl0=", "0922222222" },
                    { 3, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "hoangchau@gmail.com", "Hoàng Minh Châu", true, 500, "Thành viên Vàng", "100000.wfRv+xtcdWvsuJNlQKaVPg==.mMtChUK9eHp8Bx41+z/gcGMh6HGquvH0jiqTjHbOml4=", "0933333333" }
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
                columns: new[] { "MovieId", "ApprovalStatus", "BannerUrl", "CreatedAt", "Description", "Duration", "EndDate", "HasPendingEdit", "PendingChangesJson", "PosterUrl", "ReleaseDate", "ReviewedAt", "ReviewedBy", "ShowOnBanner", "Status", "SubmittedBy", "Title" },
                values: new object[,]
                {
                    { 1, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Ethan Hunt và đội IMF đối mặt nhiệm vụ nguy hiểm nhất sự nghiệp trong phần cuối của loạt phim gián điệp hành động kinh điển.", 170, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/mission-impossible-the-final-reckoning.jpg", new DateTime(2026, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Ngừng chiếu", null, "Mission: Impossible – The Final Reckoning" },
                    { 2, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai cảnh sát Miami Mike Lowrey và Marcus Burnett phải chạy đua để minh oan cho người chỉ huy quá cố của mình.", 115, null, false, null, "/posters/bad-boys-ride-or-die.jpg", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Bad Boys: Ride or Die" },
                    { 3, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một diễn viên đóng thế phải điều tra vụ mất tích của ngôi sao điện ảnh trong lúc cố gắng hàn gắn chuyện tình cũ.", 126, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/the-fall-guy.jpg", new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Ngừng chiếu", null, "The Fall Guy" },
                    { 4, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Câu chuyện về tuổi trẻ của Furiosa trong thế giới hậu tận thế khắc nghiệt của vũ trụ Mad Max.", 148, null, false, null, "/posters/furiosa-a-mad-max-saga.jpg", new DateTime(2026, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Furiosa: A Mad Max Saga" },
                    { 5, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhóm thợ săn bão liều lĩnh đối đầu với những cơn lốc xoáy ngày càng khốc liệt ở vùng Trung Tây nước Mỹ.", 122, new DateTime(2026, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/twisters.jpg", new DateTime(2026, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Ngừng chiếu", null, "Twisters" },
                    { 6, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai người từng có một đêm hẹn hò tuyệt vời rồi trở mặt bất ngờ, buộc phải giả vờ yêu nhau tại một đám cưới ở Úc.", 103, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/anyone-but-you.jpg", new DateTime(2026, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Anyone but You" },
                    { 7, "Đã duyệt", "/banners/it-ends-with-us-banner.jpg", new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một người phụ nữ trẻ phải đối mặt với những lựa chọn khó khăn khi tình yêu và quá khứ đau buồn đan xen.", 130, new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/it-ends-with-us.jpg", new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Đang chiếu", null, "It Ends with Us" },
                    { 8, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một cặp đôi cùng nhau trải qua những cột mốc vui buồn của cuộc sống, tình yêu và bệnh tật.", 108, new DateTime(2026, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/we-live-in-time.jpg", new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Ngừng chiếu", null, "We Live in Time" },
                    { 9, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một người mẹ đơn thân bất ngờ nảy sinh tình cảm với chàng ca sĩ trẻ của một ban nhạc nổi tiếng.", 115, null, false, null, "/posters/the-idea-of-you.jpg", new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "The Idea of You" },
                    { 10, "Đã duyệt", "/banners/past-lives-banner.jpg", new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai người bạn thời thơ ấu tái ngộ sau nhiều năm xa cách, đối diện với những gì có thể đã xảy ra.", 106, new DateTime(2026, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/past-lives.jpg", new DateTime(2026, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Đang chiếu", null, "Past Lives" },
                    { 11, "Đã duyệt", "/banners/the-substance-banner.jpg", new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một ngôi sao đang lụi tàn sử dụng loại thuốc bí ẩn để tạo ra phiên bản trẻ trung hơn của chính mình, với cái giá khủng khiếp.", 141, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/the-substance.jpg", new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Đang chiếu", null, "The Substance" },
                    { 12, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một ngôi sao nhạc pop phải đối mặt với những sự kiện ngày càng đáng sợ khi thực tại bắt đầu sụp đổ quanh cô.", 127, null, false, null, "/posters/smile-2.jpg", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Smile 2" },
                    { 13, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Gã hề sát nhân Art the Clown trở lại gieo rắc kinh hoàng trong đêm Giáng sinh.", 125, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/terrifier-3.jpg", new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Terrifier 3" },
                    { 14, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Ba thế hệ trong gia đình Deetz vô tình mở lại cánh cổng dẫn đến thế giới của hồn ma Beetlejuice.", 105, null, false, null, "/posters/beetlejuice-beetlejuice.jpg", new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Beetlejuice Beetlejuice" },
                    { 15, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một đặc vụ FBI điều tra loạt án mạng liên quan đến các manh mối huyền bí đầy ám ảnh.", 101, null, false, null, "/posters/longlegs.jpg", new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Longlegs" },
                    { 16, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Riley bước vào tuổi dậy thì và phải đối mặt với những cảm xúc mới phức tạp hơn trong tâm trí mình.", 96, null, false, null, "/posters/inside-out-2.jpg", new DateTime(2026, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Inside Out 2" },
                    { 17, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Moana lên đường trong một chuyến hải trình mới đầy thử thách cùng những người bạn cũ và mới.", 100, null, false, null, "/posters/moana-2.jpg", new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Moana 2" },
                    { 18, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Gru phải bảo vệ gia đình mới của mình trước một kẻ thù cũ đầy nguy hiểm.", 94, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/despicable-me-4.jpg", new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Despicable Me 4" },
                    { 19, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một robot bị mắc kẹt trên hòn đảo hoang phải học cách sinh tồn và trở thành người mẹ nuôi của một chú ngỗng con.", 102, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/the-wild-robot.jpg", new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Ngừng chiếu", null, "The Wild Robot" },
                    { 20, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Po phải tìm người kế nhiệm làm Rồng Chiến Binh trong khi đối mặt với một pháp sư biến hình nguy hiểm.", 94, new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/kung-fu-panda-4.jpg", new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Ngừng chiếu", null, "Kung Fu Panda 4" },
                    { 21, "Đã duyệt", "/banners/barbie-banner.jpg", new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Barbie rời khỏi thế giới hoàn hảo của mình để khám phá thế giới thực đầy bất ngờ.", 114, new DateTime(2026, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/barbie.jpg", new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Đang chiếu", null, "Barbie" },
                    { 22, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một phụ nữ được thuê để giúp một chàng trai nhút nhát tự tin hơn trước khi vào đại học.", 103, null, false, null, "/posters/no-hard-feelings.jpg", new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "No Hard Feelings" },
                    { 23, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nữ tiểu thuyết gia phát hiện cốt truyện trong sách của mình đang trở thành sự thật ngoài đời.", 139, null, false, null, "/posters/argylle.jpg", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Argylle" },
                    { 24, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhóm bạn trẻ phải sống sót qua đêm giao thừa thiên niên kỷ khi máy móc nổi loạn.", 93, null, false, null, "/posters/y2k.jpg", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Y2K" },
                    { 25, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một phụ nữ ở độ tuổi 30 bắt đầu hành trình khám phá lại chính bản thân mình.", 96, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/am-i-ok.jpg", new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Am I OK?" },
                    { 26, "Đã duyệt", "/banners/dune-2-banner.jpg", new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Paul Atreides hợp lực cùng người Fremen trên hành trình trả thù và định đoạt số phận cả vũ trụ.", 166, new DateTime(2026, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/dune-part-two.jpg", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Đang chiếu", null, "Dune: Part Two" },
                    { 27, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Hai quái vật huyền thoại Godzilla và Kong buộc phải bắt tay chống lại một mối đe dọa ẩn giấu.", 115, null, false, null, "/posters/godzilla-x-kong-the-new-empire.jpg", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "Godzilla x Kong: The New Empire" },
                    { 28, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhóm người trẻ khai thác trạm vũ trụ bỏ hoang chạm trán sinh vật ngoài hành tinh nguy hiểm bậc nhất vũ trụ.", 119, new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/alien-romulus.jpg", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Alien: Romulus" },
                    { 29, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Trong cuộc chiến giữa loài người và trí tuệ nhân tạo, một cựu binh phát hiện vũ khí bí mật mang hình hài đứa trẻ.", 133, new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/the-creator.jpg", new DateTime(2026, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "The Creator" },
                    { 30, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một phụ nữ trẻ được hồi sinh bởi khoa học kỳ lạ và bắt đầu hành trình khám phá thế giới theo cách riêng của mình.", 141, new DateTime(2026, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/poor-things.jpg", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Poor Things" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "ApprovalStatus", "BannerUrl", "CreatedAt", "Description", "Duration", "EndDate", "HasPendingEdit", "PendingChangesJson", "PosterUrl", "ReleaseDate", "ReviewedAt", "ReviewedBy", "ShowOnBanner", "Status", "SubmittedBy", "Title" },
                values: new object[,]
                {
                    { 31, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Ghi lại hành trình leo núi El Capitan không dây bảo hộ đầy mạo hiểm của vận động viên Alex Honnold.", 100, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/free-solo.jpg", new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Ngừng chiếu", null, "Free Solo" },
                    { 32, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Một nhà làm phim xây dựng mối quan hệ đặc biệt với một con bạch tuộc hoang dã ngoài khơi Nam Phi.", 85, null, false, null, "/posters/my-octopus-teacher.jpg", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "My Octopus Teacher" },
                    { 33, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Bộ phim tài liệu phân tích mối liên hệ giữa chế độ nô lệ và hệ thống nhà tù ở nước Mỹ hiện đại.", 100, null, false, null, "/posters/13th.jpg", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Sắp chiếu", null, "13th" },
                    { 34, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Chân dung về cuộc đời và di sản của Fred Rogers, người dẫn chương trình truyền hình thiếu nhi huyền thoại.", 94, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/won-t-you-be-my-neighbor.jpg", new DateTime(2026, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Won't You Be My Neighbor?" },
                    { 35, "Đã duyệt", null, new DateTime(2026, 7, 12, 17, 13, 55, 597, DateTimeKind.Unspecified), "Câu chuyện có thật đằng sau lễ hội âm nhạc xa hoa sụp đổ thảm hại trên mạng xã hội.", 97, new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/posters/fyre-the-greatest-party-that-never-happened.jpg", new DateTime(2026, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Đang chiếu", null, "Fyre: The Greatest Party That Never Happened" }
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
                table: "Showtimes",
                columns: new[] { "ShowtimeId", "EndTime", "MovieId", "RoomId", "StartTime", "Status", "TicketPrice" },
                values: new object[,]
                {
                    { 100001, new DateTime(2026, 7, 29, 11, 13, 0, 0, DateTimeKind.Unspecified), 6, 1, new DateTime(2026, 7, 29, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100002, new DateTime(2026, 8, 2, 11, 13, 0, 0, DateTimeKind.Unspecified), 6, 2, new DateTime(2026, 8, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100003, new DateTime(2026, 7, 30, 11, 40, 0, 0, DateTimeKind.Unspecified), 7, 2, new DateTime(2026, 7, 30, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100004, new DateTime(2026, 8, 3, 11, 40, 0, 0, DateTimeKind.Unspecified), 7, 1, new DateTime(2026, 8, 3, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100005, new DateTime(2026, 7, 31, 11, 16, 0, 0, DateTimeKind.Unspecified), 10, 1, new DateTime(2026, 7, 31, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100006, new DateTime(2026, 8, 4, 11, 16, 0, 0, DateTimeKind.Unspecified), 10, 2, new DateTime(2026, 8, 4, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100007, new DateTime(2026, 8, 1, 11, 51, 0, 0, DateTimeKind.Unspecified), 11, 2, new DateTime(2026, 8, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100008, new DateTime(2026, 7, 29, 13, 54, 0, 0, DateTimeKind.Unspecified), 11, 1, new DateTime(2026, 7, 29, 11, 33, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100009, new DateTime(2026, 8, 2, 11, 35, 0, 0, DateTimeKind.Unspecified), 13, 1, new DateTime(2026, 8, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100010, new DateTime(2026, 7, 30, 14, 5, 0, 0, DateTimeKind.Unspecified), 13, 2, new DateTime(2026, 7, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100011, new DateTime(2026, 8, 3, 11, 4, 0, 0, DateTimeKind.Unspecified), 18, 2, new DateTime(2026, 8, 3, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100012, new DateTime(2026, 7, 31, 13, 10, 0, 0, DateTimeKind.Unspecified), 18, 1, new DateTime(2026, 7, 31, 11, 36, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100013, new DateTime(2026, 8, 4, 11, 24, 0, 0, DateTimeKind.Unspecified), 21, 1, new DateTime(2026, 8, 4, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100014, new DateTime(2026, 8, 1, 14, 5, 0, 0, DateTimeKind.Unspecified), 21, 2, new DateTime(2026, 8, 1, 12, 11, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100015, new DateTime(2026, 7, 29, 11, 6, 0, 0, DateTimeKind.Unspecified), 25, 2, new DateTime(2026, 7, 29, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100016, new DateTime(2026, 8, 2, 13, 31, 0, 0, DateTimeKind.Unspecified), 25, 1, new DateTime(2026, 8, 2, 11, 55, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100017, new DateTime(2026, 7, 30, 12, 16, 0, 0, DateTimeKind.Unspecified), 26, 1, new DateTime(2026, 7, 30, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100018, new DateTime(2026, 8, 3, 14, 10, 0, 0, DateTimeKind.Unspecified), 26, 2, new DateTime(2026, 8, 3, 11, 24, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100019, new DateTime(2026, 7, 31, 11, 29, 0, 0, DateTimeKind.Unspecified), 28, 2, new DateTime(2026, 7, 31, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100020, new DateTime(2026, 8, 4, 13, 43, 0, 0, DateTimeKind.Unspecified), 28, 1, new DateTime(2026, 8, 4, 11, 44, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100021, new DateTime(2026, 8, 1, 11, 43, 0, 0, DateTimeKind.Unspecified), 29, 1, new DateTime(2026, 8, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100022, new DateTime(2026, 7, 29, 13, 39, 0, 0, DateTimeKind.Unspecified), 29, 2, new DateTime(2026, 7, 29, 11, 26, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100023, new DateTime(2026, 8, 2, 13, 54, 0, 0, DateTimeKind.Unspecified), 30, 2, new DateTime(2026, 8, 2, 11, 33, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100024, new DateTime(2026, 7, 30, 14, 57, 0, 0, DateTimeKind.Unspecified), 30, 1, new DateTime(2026, 7, 30, 12, 36, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100025, new DateTime(2026, 8, 3, 13, 34, 0, 0, DateTimeKind.Unspecified), 34, 1, new DateTime(2026, 8, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100026, new DateTime(2026, 7, 31, 13, 23, 0, 0, DateTimeKind.Unspecified), 34, 2, new DateTime(2026, 7, 31, 11, 49, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "ShowtimeId", "EndTime", "MovieId", "RoomId", "StartTime", "Status", "TicketPrice" },
                values: new object[,]
                {
                    { 100027, new DateTime(2026, 8, 4, 13, 13, 0, 0, DateTimeKind.Unspecified), 35, 2, new DateTime(2026, 8, 4, 11, 36, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100028, new DateTime(2026, 8, 1, 13, 40, 0, 0, DateTimeKind.Unspecified), 35, 1, new DateTime(2026, 8, 1, 12, 3, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100029, new DateTime(2026, 8, 5, 11, 25, 0, 0, DateTimeKind.Unspecified), 2, 1, new DateTime(2026, 8, 5, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100030, new DateTime(2026, 8, 9, 11, 25, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(2026, 8, 9, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100031, new DateTime(2026, 8, 6, 11, 58, 0, 0, DateTimeKind.Unspecified), 4, 2, new DateTime(2026, 8, 6, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100032, new DateTime(2026, 8, 10, 11, 58, 0, 0, DateTimeKind.Unspecified), 4, 1, new DateTime(2026, 8, 10, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100033, new DateTime(2026, 8, 7, 11, 25, 0, 0, DateTimeKind.Unspecified), 9, 1, new DateTime(2026, 8, 7, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100034, new DateTime(2026, 8, 11, 11, 25, 0, 0, DateTimeKind.Unspecified), 9, 2, new DateTime(2026, 8, 11, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100035, new DateTime(2026, 8, 8, 11, 37, 0, 0, DateTimeKind.Unspecified), 12, 2, new DateTime(2026, 8, 8, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100036, new DateTime(2026, 8, 5, 13, 52, 0, 0, DateTimeKind.Unspecified), 12, 1, new DateTime(2026, 8, 5, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100037, new DateTime(2026, 8, 9, 11, 15, 0, 0, DateTimeKind.Unspecified), 14, 1, new DateTime(2026, 8, 9, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100038, new DateTime(2026, 8, 6, 14, 3, 0, 0, DateTimeKind.Unspecified), 14, 2, new DateTime(2026, 8, 6, 12, 18, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100039, new DateTime(2026, 8, 10, 11, 11, 0, 0, DateTimeKind.Unspecified), 15, 2, new DateTime(2026, 8, 10, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100040, new DateTime(2026, 8, 7, 13, 26, 0, 0, DateTimeKind.Unspecified), 15, 1, new DateTime(2026, 8, 7, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100041, new DateTime(2026, 8, 11, 11, 6, 0, 0, DateTimeKind.Unspecified), 16, 1, new DateTime(2026, 8, 11, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100042, new DateTime(2026, 8, 8, 13, 33, 0, 0, DateTimeKind.Unspecified), 16, 2, new DateTime(2026, 8, 8, 11, 57, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100043, new DateTime(2026, 8, 5, 11, 10, 0, 0, DateTimeKind.Unspecified), 17, 2, new DateTime(2026, 8, 5, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100044, new DateTime(2026, 8, 9, 13, 15, 0, 0, DateTimeKind.Unspecified), 17, 1, new DateTime(2026, 8, 9, 11, 35, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100045, new DateTime(2026, 8, 6, 11, 13, 0, 0, DateTimeKind.Unspecified), 22, 1, new DateTime(2026, 8, 6, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100046, new DateTime(2026, 8, 10, 13, 14, 0, 0, DateTimeKind.Unspecified), 22, 2, new DateTime(2026, 8, 10, 11, 31, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100047, new DateTime(2026, 8, 7, 11, 49, 0, 0, DateTimeKind.Unspecified), 23, 2, new DateTime(2026, 8, 7, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100048, new DateTime(2026, 8, 11, 13, 45, 0, 0, DateTimeKind.Unspecified), 23, 1, new DateTime(2026, 8, 11, 11, 26, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100049, new DateTime(2026, 8, 8, 11, 3, 0, 0, DateTimeKind.Unspecified), 24, 1, new DateTime(2026, 8, 8, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100050, new DateTime(2026, 8, 5, 13, 3, 0, 0, DateTimeKind.Unspecified), 24, 2, new DateTime(2026, 8, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100051, new DateTime(2026, 8, 9, 13, 40, 0, 0, DateTimeKind.Unspecified), 27, 2, new DateTime(2026, 8, 9, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100052, new DateTime(2026, 8, 6, 13, 28, 0, 0, DateTimeKind.Unspecified), 27, 1, new DateTime(2026, 8, 6, 11, 33, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100053, new DateTime(2026, 8, 10, 13, 43, 0, 0, DateTimeKind.Unspecified), 32, 1, new DateTime(2026, 8, 10, 12, 18, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100054, new DateTime(2026, 8, 7, 13, 34, 0, 0, DateTimeKind.Unspecified), 32, 2, new DateTime(2026, 8, 7, 12, 9, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100055, new DateTime(2026, 8, 11, 13, 25, 0, 0, DateTimeKind.Unspecified), 33, 2, new DateTime(2026, 8, 11, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100056, new DateTime(2026, 8, 8, 13, 3, 0, 0, DateTimeKind.Unspecified), 33, 1, new DateTime(2026, 8, 8, 11, 23, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 100057, new DateTime(2026, 8, 8, 18, 43, 0, 0, DateTimeKind.Unspecified), 6, 1, new DateTime(2026, 8, 8, 17, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100058, new DateTime(2026, 8, 8, 19, 10, 0, 0, DateTimeKind.Unspecified), 7, 2, new DateTime(2026, 8, 8, 17, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100059, new DateTime(2026, 8, 8, 20, 49, 0, 0, DateTimeKind.Unspecified), 10, 1, new DateTime(2026, 8, 8, 19, 3, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100060, new DateTime(2026, 8, 8, 21, 51, 0, 0, DateTimeKind.Unspecified), 11, 2, new DateTime(2026, 8, 8, 19, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100061, new DateTime(2026, 8, 8, 23, 14, 0, 0, DateTimeKind.Unspecified), 13, 1, new DateTime(2026, 8, 8, 21, 9, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100062, new DateTime(2026, 8, 8, 23, 45, 0, 0, DateTimeKind.Unspecified), 18, 2, new DateTime(2026, 8, 8, 22, 11, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100063, new DateTime(2026, 8, 9, 10, 43, 0, 0, DateTimeKind.Unspecified), 6, 1, new DateTime(2026, 8, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100064, new DateTime(2026, 8, 9, 11, 10, 0, 0, DateTimeKind.Unspecified), 7, 2, new DateTime(2026, 8, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100065, new DateTime(2026, 8, 9, 12, 49, 0, 0, DateTimeKind.Unspecified), 10, 1, new DateTime(2026, 8, 9, 11, 3, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100066, new DateTime(2026, 8, 9, 13, 51, 0, 0, DateTimeKind.Unspecified), 11, 2, new DateTime(2026, 8, 9, 11, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100067, new DateTime(2026, 8, 9, 15, 14, 0, 0, DateTimeKind.Unspecified), 13, 1, new DateTime(2026, 8, 9, 13, 9, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100068, new DateTime(2026, 8, 9, 15, 45, 0, 0, DateTimeKind.Unspecified), 18, 2, new DateTime(2026, 8, 9, 14, 11, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "ShowtimeId", "EndTime", "MovieId", "RoomId", "StartTime", "Status", "TicketPrice" },
                values: new object[,]
                {
                    { 100069, new DateTime(2026, 8, 9, 17, 28, 0, 0, DateTimeKind.Unspecified), 21, 1, new DateTime(2026, 8, 9, 15, 34, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100070, new DateTime(2026, 8, 9, 17, 41, 0, 0, DateTimeKind.Unspecified), 25, 2, new DateTime(2026, 8, 9, 16, 5, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100071, new DateTime(2026, 8, 9, 20, 34, 0, 0, DateTimeKind.Unspecified), 26, 1, new DateTime(2026, 8, 9, 17, 48, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100072, new DateTime(2026, 8, 9, 20, 0, 0, 0, DateTimeKind.Unspecified), 28, 2, new DateTime(2026, 8, 9, 18, 1, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100073, new DateTime(2026, 8, 9, 23, 7, 0, 0, DateTimeKind.Unspecified), 29, 1, new DateTime(2026, 8, 9, 20, 54, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100074, new DateTime(2026, 8, 9, 22, 41, 0, 0, DateTimeKind.Unspecified), 30, 2, new DateTime(2026, 8, 9, 20, 20, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "admin01@rapphim.vn", "Nguyễn Văn Quản", true, "100000.CHbfym1v7fmbKrh7bi/tSw==.hOJV2nu7uA6qiEAqU5BkCXQ7lpThnQOAvqosUHxby2M=", "0900000001", 2, "admin01" },
                    { 2, new DateTime(2026, 7, 9, 15, 25, 8, 577, DateTimeKind.Unspecified), "staff01@rapphim.vn", "Trần Thị Nhân Viên", true, "100000.glxfROvxJ6dnkFPGXnwJDw==.KWyDIAdywy5y/vUCO3btYzxO/5Vj0DU7KqD29qqZl1A=", "0900000002", 1, "staff01" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200001, null, null, 1, 100001, "Trống" },
                    { 200002, null, null, 2, 100001, "Trống" },
                    { 200003, null, null, 3, 100001, "Trống" },
                    { 200004, null, null, 4, 100001, "Trống" },
                    { 200005, null, null, 5, 100001, "Trống" },
                    { 200006, null, null, 6, 100001, "Trống" },
                    { 200007, null, null, 7, 100001, "Trống" },
                    { 200008, null, null, 8, 100001, "Trống" },
                    { 200009, null, null, 9, 100001, "Trống" },
                    { 200010, null, null, 10, 100001, "Trống" },
                    { 200011, null, null, 11, 100001, "Trống" },
                    { 200012, null, null, 12, 100001, "Trống" },
                    { 200013, null, null, 13, 100001, "Trống" },
                    { 200014, null, null, 14, 100001, "Trống" },
                    { 200015, null, null, 15, 100001, "Trống" },
                    { 200016, null, null, 16, 100002, "Trống" },
                    { 200017, null, null, 17, 100002, "Trống" },
                    { 200018, null, null, 18, 100002, "Trống" },
                    { 200019, null, null, 19, 100002, "Trống" },
                    { 200020, null, null, 20, 100002, "Trống" },
                    { 200021, null, null, 21, 100002, "Trống" },
                    { 200022, null, null, 22, 100002, "Trống" },
                    { 200023, null, null, 23, 100002, "Trống" },
                    { 200024, null, null, 16, 100003, "Trống" },
                    { 200025, null, null, 17, 100003, "Trống" },
                    { 200026, null, null, 18, 100003, "Trống" },
                    { 200027, null, null, 19, 100003, "Trống" },
                    { 200028, null, null, 20, 100003, "Trống" },
                    { 200029, null, null, 21, 100003, "Trống" },
                    { 200030, null, null, 22, 100003, "Trống" },
                    { 200031, null, null, 23, 100003, "Trống" },
                    { 200032, null, null, 1, 100004, "Trống" },
                    { 200033, null, null, 2, 100004, "Trống" },
                    { 200034, null, null, 3, 100004, "Trống" },
                    { 200035, null, null, 4, 100004, "Trống" },
                    { 200036, null, null, 5, 100004, "Trống" },
                    { 200037, null, null, 6, 100004, "Trống" },
                    { 200038, null, null, 7, 100004, "Trống" },
                    { 200039, null, null, 8, 100004, "Trống" },
                    { 200040, null, null, 9, 100004, "Trống" },
                    { 200041, null, null, 10, 100004, "Trống" },
                    { 200042, null, null, 11, 100004, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200043, null, null, 12, 100004, "Trống" },
                    { 200044, null, null, 13, 100004, "Trống" },
                    { 200045, null, null, 14, 100004, "Trống" },
                    { 200046, null, null, 15, 100004, "Trống" },
                    { 200047, null, null, 1, 100005, "Trống" },
                    { 200048, null, null, 2, 100005, "Trống" },
                    { 200049, null, null, 3, 100005, "Trống" },
                    { 200050, null, null, 4, 100005, "Trống" },
                    { 200051, null, null, 5, 100005, "Trống" },
                    { 200052, null, null, 6, 100005, "Trống" },
                    { 200053, null, null, 7, 100005, "Trống" },
                    { 200054, null, null, 8, 100005, "Trống" },
                    { 200055, null, null, 9, 100005, "Trống" },
                    { 200056, null, null, 10, 100005, "Trống" },
                    { 200057, null, null, 11, 100005, "Trống" },
                    { 200058, null, null, 12, 100005, "Trống" },
                    { 200059, null, null, 13, 100005, "Trống" },
                    { 200060, null, null, 14, 100005, "Trống" },
                    { 200061, null, null, 15, 100005, "Trống" },
                    { 200062, null, null, 16, 100006, "Trống" },
                    { 200063, null, null, 17, 100006, "Trống" },
                    { 200064, null, null, 18, 100006, "Trống" },
                    { 200065, null, null, 19, 100006, "Trống" },
                    { 200066, null, null, 20, 100006, "Trống" },
                    { 200067, null, null, 21, 100006, "Trống" },
                    { 200068, null, null, 22, 100006, "Trống" },
                    { 200069, null, null, 23, 100006, "Trống" },
                    { 200070, null, null, 16, 100007, "Trống" },
                    { 200071, null, null, 17, 100007, "Trống" },
                    { 200072, null, null, 18, 100007, "Trống" },
                    { 200073, null, null, 19, 100007, "Trống" },
                    { 200074, null, null, 20, 100007, "Trống" },
                    { 200075, null, null, 21, 100007, "Trống" },
                    { 200076, null, null, 22, 100007, "Trống" },
                    { 200077, null, null, 23, 100007, "Trống" },
                    { 200078, null, null, 1, 100008, "Trống" },
                    { 200079, null, null, 2, 100008, "Trống" },
                    { 200080, null, null, 3, 100008, "Trống" },
                    { 200081, null, null, 4, 100008, "Trống" },
                    { 200082, null, null, 5, 100008, "Trống" },
                    { 200083, null, null, 6, 100008, "Trống" },
                    { 200084, null, null, 7, 100008, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200085, null, null, 8, 100008, "Trống" },
                    { 200086, null, null, 9, 100008, "Trống" },
                    { 200087, null, null, 10, 100008, "Trống" },
                    { 200088, null, null, 11, 100008, "Trống" },
                    { 200089, null, null, 12, 100008, "Trống" },
                    { 200090, null, null, 13, 100008, "Trống" },
                    { 200091, null, null, 14, 100008, "Trống" },
                    { 200092, null, null, 15, 100008, "Trống" },
                    { 200093, null, null, 1, 100009, "Trống" },
                    { 200094, null, null, 2, 100009, "Trống" },
                    { 200095, null, null, 3, 100009, "Trống" },
                    { 200096, null, null, 4, 100009, "Trống" },
                    { 200097, null, null, 5, 100009, "Trống" },
                    { 200098, null, null, 6, 100009, "Trống" },
                    { 200099, null, null, 7, 100009, "Trống" },
                    { 200100, null, null, 8, 100009, "Trống" },
                    { 200101, null, null, 9, 100009, "Trống" },
                    { 200102, null, null, 10, 100009, "Trống" },
                    { 200103, null, null, 11, 100009, "Trống" },
                    { 200104, null, null, 12, 100009, "Trống" },
                    { 200105, null, null, 13, 100009, "Trống" },
                    { 200106, null, null, 14, 100009, "Trống" },
                    { 200107, null, null, 15, 100009, "Trống" },
                    { 200108, null, null, 16, 100010, "Trống" },
                    { 200109, null, null, 17, 100010, "Trống" },
                    { 200110, null, null, 18, 100010, "Trống" },
                    { 200111, null, null, 19, 100010, "Trống" },
                    { 200112, null, null, 20, 100010, "Trống" },
                    { 200113, null, null, 21, 100010, "Trống" },
                    { 200114, null, null, 22, 100010, "Trống" },
                    { 200115, null, null, 23, 100010, "Trống" },
                    { 200116, null, null, 16, 100011, "Trống" },
                    { 200117, null, null, 17, 100011, "Trống" },
                    { 200118, null, null, 18, 100011, "Trống" },
                    { 200119, null, null, 19, 100011, "Trống" },
                    { 200120, null, null, 20, 100011, "Trống" },
                    { 200121, null, null, 21, 100011, "Trống" },
                    { 200122, null, null, 22, 100011, "Trống" },
                    { 200123, null, null, 23, 100011, "Trống" },
                    { 200124, null, null, 1, 100012, "Trống" },
                    { 200125, null, null, 2, 100012, "Trống" },
                    { 200126, null, null, 3, 100012, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200127, null, null, 4, 100012, "Trống" },
                    { 200128, null, null, 5, 100012, "Trống" },
                    { 200129, null, null, 6, 100012, "Trống" },
                    { 200130, null, null, 7, 100012, "Trống" },
                    { 200131, null, null, 8, 100012, "Trống" },
                    { 200132, null, null, 9, 100012, "Trống" },
                    { 200133, null, null, 10, 100012, "Trống" },
                    { 200134, null, null, 11, 100012, "Trống" },
                    { 200135, null, null, 12, 100012, "Trống" },
                    { 200136, null, null, 13, 100012, "Trống" },
                    { 200137, null, null, 14, 100012, "Trống" },
                    { 200138, null, null, 15, 100012, "Trống" },
                    { 200139, null, null, 1, 100013, "Trống" },
                    { 200140, null, null, 2, 100013, "Trống" },
                    { 200141, null, null, 3, 100013, "Trống" },
                    { 200142, null, null, 4, 100013, "Trống" },
                    { 200143, null, null, 5, 100013, "Trống" },
                    { 200144, null, null, 6, 100013, "Trống" },
                    { 200145, null, null, 7, 100013, "Trống" },
                    { 200146, null, null, 8, 100013, "Trống" },
                    { 200147, null, null, 9, 100013, "Trống" },
                    { 200148, null, null, 10, 100013, "Trống" },
                    { 200149, null, null, 11, 100013, "Trống" },
                    { 200150, null, null, 12, 100013, "Trống" },
                    { 200151, null, null, 13, 100013, "Trống" },
                    { 200152, null, null, 14, 100013, "Trống" },
                    { 200153, null, null, 15, 100013, "Trống" },
                    { 200154, null, null, 16, 100014, "Trống" },
                    { 200155, null, null, 17, 100014, "Trống" },
                    { 200156, null, null, 18, 100014, "Trống" },
                    { 200157, null, null, 19, 100014, "Trống" },
                    { 200158, null, null, 20, 100014, "Trống" },
                    { 200159, null, null, 21, 100014, "Trống" },
                    { 200160, null, null, 22, 100014, "Trống" },
                    { 200161, null, null, 23, 100014, "Trống" },
                    { 200162, null, null, 16, 100015, "Trống" },
                    { 200163, null, null, 17, 100015, "Trống" },
                    { 200164, null, null, 18, 100015, "Trống" },
                    { 200165, null, null, 19, 100015, "Trống" },
                    { 200166, null, null, 20, 100015, "Trống" },
                    { 200167, null, null, 21, 100015, "Trống" },
                    { 200168, null, null, 22, 100015, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200169, null, null, 23, 100015, "Trống" },
                    { 200170, null, null, 1, 100016, "Trống" },
                    { 200171, null, null, 2, 100016, "Trống" },
                    { 200172, null, null, 3, 100016, "Trống" },
                    { 200173, null, null, 4, 100016, "Trống" },
                    { 200174, null, null, 5, 100016, "Trống" },
                    { 200175, null, null, 6, 100016, "Trống" },
                    { 200176, null, null, 7, 100016, "Trống" },
                    { 200177, null, null, 8, 100016, "Trống" },
                    { 200178, null, null, 9, 100016, "Trống" },
                    { 200179, null, null, 10, 100016, "Trống" },
                    { 200180, null, null, 11, 100016, "Trống" },
                    { 200181, null, null, 12, 100016, "Trống" },
                    { 200182, null, null, 13, 100016, "Trống" },
                    { 200183, null, null, 14, 100016, "Trống" },
                    { 200184, null, null, 15, 100016, "Trống" },
                    { 200185, null, null, 1, 100017, "Trống" },
                    { 200186, null, null, 2, 100017, "Trống" },
                    { 200187, null, null, 3, 100017, "Trống" },
                    { 200188, null, null, 4, 100017, "Trống" },
                    { 200189, null, null, 5, 100017, "Trống" },
                    { 200190, null, null, 6, 100017, "Trống" },
                    { 200191, null, null, 7, 100017, "Trống" },
                    { 200192, null, null, 8, 100017, "Trống" },
                    { 200193, null, null, 9, 100017, "Trống" },
                    { 200194, null, null, 10, 100017, "Trống" },
                    { 200195, null, null, 11, 100017, "Trống" },
                    { 200196, null, null, 12, 100017, "Trống" },
                    { 200197, null, null, 13, 100017, "Trống" },
                    { 200198, null, null, 14, 100017, "Trống" },
                    { 200199, null, null, 15, 100017, "Trống" },
                    { 200200, null, null, 16, 100018, "Trống" },
                    { 200201, null, null, 17, 100018, "Trống" },
                    { 200202, null, null, 18, 100018, "Trống" },
                    { 200203, null, null, 19, 100018, "Trống" },
                    { 200204, null, null, 20, 100018, "Trống" },
                    { 200205, null, null, 21, 100018, "Trống" },
                    { 200206, null, null, 22, 100018, "Trống" },
                    { 200207, null, null, 23, 100018, "Trống" },
                    { 200208, null, null, 16, 100019, "Trống" },
                    { 200209, null, null, 17, 100019, "Trống" },
                    { 200210, null, null, 18, 100019, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200211, null, null, 19, 100019, "Trống" },
                    { 200212, null, null, 20, 100019, "Trống" },
                    { 200213, null, null, 21, 100019, "Trống" },
                    { 200214, null, null, 22, 100019, "Trống" },
                    { 200215, null, null, 23, 100019, "Trống" },
                    { 200216, null, null, 1, 100020, "Trống" },
                    { 200217, null, null, 2, 100020, "Trống" },
                    { 200218, null, null, 3, 100020, "Trống" },
                    { 200219, null, null, 4, 100020, "Trống" },
                    { 200220, null, null, 5, 100020, "Trống" },
                    { 200221, null, null, 6, 100020, "Trống" },
                    { 200222, null, null, 7, 100020, "Trống" },
                    { 200223, null, null, 8, 100020, "Trống" },
                    { 200224, null, null, 9, 100020, "Trống" },
                    { 200225, null, null, 10, 100020, "Trống" },
                    { 200226, null, null, 11, 100020, "Trống" },
                    { 200227, null, null, 12, 100020, "Trống" },
                    { 200228, null, null, 13, 100020, "Trống" },
                    { 200229, null, null, 14, 100020, "Trống" },
                    { 200230, null, null, 15, 100020, "Trống" },
                    { 200231, null, null, 1, 100021, "Trống" },
                    { 200232, null, null, 2, 100021, "Trống" },
                    { 200233, null, null, 3, 100021, "Trống" },
                    { 200234, null, null, 4, 100021, "Trống" },
                    { 200235, null, null, 5, 100021, "Trống" },
                    { 200236, null, null, 6, 100021, "Trống" },
                    { 200237, null, null, 7, 100021, "Trống" },
                    { 200238, null, null, 8, 100021, "Trống" },
                    { 200239, null, null, 9, 100021, "Trống" },
                    { 200240, null, null, 10, 100021, "Trống" },
                    { 200241, null, null, 11, 100021, "Trống" },
                    { 200242, null, null, 12, 100021, "Trống" },
                    { 200243, null, null, 13, 100021, "Trống" },
                    { 200244, null, null, 14, 100021, "Trống" },
                    { 200245, null, null, 15, 100021, "Trống" },
                    { 200246, null, null, 16, 100022, "Trống" },
                    { 200247, null, null, 17, 100022, "Trống" },
                    { 200248, null, null, 18, 100022, "Trống" },
                    { 200249, null, null, 19, 100022, "Trống" },
                    { 200250, null, null, 20, 100022, "Trống" },
                    { 200251, null, null, 21, 100022, "Trống" },
                    { 200252, null, null, 22, 100022, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200253, null, null, 23, 100022, "Trống" },
                    { 200254, null, null, 16, 100023, "Trống" },
                    { 200255, null, null, 17, 100023, "Trống" },
                    { 200256, null, null, 18, 100023, "Trống" },
                    { 200257, null, null, 19, 100023, "Trống" },
                    { 200258, null, null, 20, 100023, "Trống" },
                    { 200259, null, null, 21, 100023, "Trống" },
                    { 200260, null, null, 22, 100023, "Trống" },
                    { 200261, null, null, 23, 100023, "Trống" },
                    { 200262, null, null, 1, 100024, "Trống" },
                    { 200263, null, null, 2, 100024, "Trống" },
                    { 200264, null, null, 3, 100024, "Trống" },
                    { 200265, null, null, 4, 100024, "Trống" },
                    { 200266, null, null, 5, 100024, "Trống" },
                    { 200267, null, null, 6, 100024, "Trống" },
                    { 200268, null, null, 7, 100024, "Trống" },
                    { 200269, null, null, 8, 100024, "Trống" },
                    { 200270, null, null, 9, 100024, "Trống" },
                    { 200271, null, null, 10, 100024, "Trống" },
                    { 200272, null, null, 11, 100024, "Trống" },
                    { 200273, null, null, 12, 100024, "Trống" },
                    { 200274, null, null, 13, 100024, "Trống" },
                    { 200275, null, null, 14, 100024, "Trống" },
                    { 200276, null, null, 15, 100024, "Trống" },
                    { 200277, null, null, 1, 100025, "Trống" },
                    { 200278, null, null, 2, 100025, "Trống" },
                    { 200279, null, null, 3, 100025, "Trống" },
                    { 200280, null, null, 4, 100025, "Trống" },
                    { 200281, null, null, 5, 100025, "Trống" },
                    { 200282, null, null, 6, 100025, "Trống" },
                    { 200283, null, null, 7, 100025, "Trống" },
                    { 200284, null, null, 8, 100025, "Trống" },
                    { 200285, null, null, 9, 100025, "Trống" },
                    { 200286, null, null, 10, 100025, "Trống" },
                    { 200287, null, null, 11, 100025, "Trống" },
                    { 200288, null, null, 12, 100025, "Trống" },
                    { 200289, null, null, 13, 100025, "Trống" },
                    { 200290, null, null, 14, 100025, "Trống" },
                    { 200291, null, null, 15, 100025, "Trống" },
                    { 200292, null, null, 16, 100026, "Trống" },
                    { 200293, null, null, 17, 100026, "Trống" },
                    { 200294, null, null, 18, 100026, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200295, null, null, 19, 100026, "Trống" },
                    { 200296, null, null, 20, 100026, "Trống" },
                    { 200297, null, null, 21, 100026, "Trống" },
                    { 200298, null, null, 22, 100026, "Trống" },
                    { 200299, null, null, 23, 100026, "Trống" },
                    { 200300, null, null, 16, 100027, "Trống" },
                    { 200301, null, null, 17, 100027, "Trống" },
                    { 200302, null, null, 18, 100027, "Trống" },
                    { 200303, null, null, 19, 100027, "Trống" },
                    { 200304, null, null, 20, 100027, "Trống" },
                    { 200305, null, null, 21, 100027, "Trống" },
                    { 200306, null, null, 22, 100027, "Trống" },
                    { 200307, null, null, 23, 100027, "Trống" },
                    { 200308, null, null, 1, 100028, "Trống" },
                    { 200309, null, null, 2, 100028, "Trống" },
                    { 200310, null, null, 3, 100028, "Trống" },
                    { 200311, null, null, 4, 100028, "Trống" },
                    { 200312, null, null, 5, 100028, "Trống" },
                    { 200313, null, null, 6, 100028, "Trống" },
                    { 200314, null, null, 7, 100028, "Trống" },
                    { 200315, null, null, 8, 100028, "Trống" },
                    { 200316, null, null, 9, 100028, "Trống" },
                    { 200317, null, null, 10, 100028, "Trống" },
                    { 200318, null, null, 11, 100028, "Trống" },
                    { 200319, null, null, 12, 100028, "Trống" },
                    { 200320, null, null, 13, 100028, "Trống" },
                    { 200321, null, null, 14, 100028, "Trống" },
                    { 200322, null, null, 15, 100028, "Trống" },
                    { 200323, null, null, 1, 100029, "Trống" },
                    { 200324, null, null, 2, 100029, "Trống" },
                    { 200325, null, null, 3, 100029, "Trống" },
                    { 200326, null, null, 4, 100029, "Trống" },
                    { 200327, null, null, 5, 100029, "Trống" },
                    { 200328, null, null, 6, 100029, "Trống" },
                    { 200329, null, null, 7, 100029, "Trống" },
                    { 200330, null, null, 8, 100029, "Trống" },
                    { 200331, null, null, 9, 100029, "Trống" },
                    { 200332, null, null, 10, 100029, "Trống" },
                    { 200333, null, null, 11, 100029, "Trống" },
                    { 200334, null, null, 12, 100029, "Trống" },
                    { 200335, null, null, 13, 100029, "Trống" },
                    { 200336, null, null, 14, 100029, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200337, null, null, 15, 100029, "Trống" },
                    { 200338, null, null, 16, 100030, "Trống" },
                    { 200339, null, null, 17, 100030, "Trống" },
                    { 200340, null, null, 18, 100030, "Trống" },
                    { 200341, null, null, 19, 100030, "Trống" },
                    { 200342, null, null, 20, 100030, "Trống" },
                    { 200343, null, null, 21, 100030, "Trống" },
                    { 200344, null, null, 22, 100030, "Trống" },
                    { 200345, null, null, 23, 100030, "Trống" },
                    { 200346, null, null, 16, 100031, "Trống" },
                    { 200347, null, null, 17, 100031, "Trống" },
                    { 200348, null, null, 18, 100031, "Trống" },
                    { 200349, null, null, 19, 100031, "Trống" },
                    { 200350, null, null, 20, 100031, "Trống" },
                    { 200351, null, null, 21, 100031, "Trống" },
                    { 200352, null, null, 22, 100031, "Trống" },
                    { 200353, null, null, 23, 100031, "Trống" },
                    { 200354, null, null, 1, 100032, "Trống" },
                    { 200355, null, null, 2, 100032, "Trống" },
                    { 200356, null, null, 3, 100032, "Trống" },
                    { 200357, null, null, 4, 100032, "Trống" },
                    { 200358, null, null, 5, 100032, "Trống" },
                    { 200359, null, null, 6, 100032, "Trống" },
                    { 200360, null, null, 7, 100032, "Trống" },
                    { 200361, null, null, 8, 100032, "Trống" },
                    { 200362, null, null, 9, 100032, "Trống" },
                    { 200363, null, null, 10, 100032, "Trống" },
                    { 200364, null, null, 11, 100032, "Trống" },
                    { 200365, null, null, 12, 100032, "Trống" },
                    { 200366, null, null, 13, 100032, "Trống" },
                    { 200367, null, null, 14, 100032, "Trống" },
                    { 200368, null, null, 15, 100032, "Trống" },
                    { 200369, null, null, 1, 100033, "Trống" },
                    { 200370, null, null, 2, 100033, "Trống" },
                    { 200371, null, null, 3, 100033, "Trống" },
                    { 200372, null, null, 4, 100033, "Trống" },
                    { 200373, null, null, 5, 100033, "Trống" },
                    { 200374, null, null, 6, 100033, "Trống" },
                    { 200375, null, null, 7, 100033, "Trống" },
                    { 200376, null, null, 8, 100033, "Trống" },
                    { 200377, null, null, 9, 100033, "Trống" },
                    { 200378, null, null, 10, 100033, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200379, null, null, 11, 100033, "Trống" },
                    { 200380, null, null, 12, 100033, "Trống" },
                    { 200381, null, null, 13, 100033, "Trống" },
                    { 200382, null, null, 14, 100033, "Trống" },
                    { 200383, null, null, 15, 100033, "Trống" },
                    { 200384, null, null, 16, 100034, "Trống" },
                    { 200385, null, null, 17, 100034, "Trống" },
                    { 200386, null, null, 18, 100034, "Trống" },
                    { 200387, null, null, 19, 100034, "Trống" },
                    { 200388, null, null, 20, 100034, "Trống" },
                    { 200389, null, null, 21, 100034, "Trống" },
                    { 200390, null, null, 22, 100034, "Trống" },
                    { 200391, null, null, 23, 100034, "Trống" },
                    { 200392, null, null, 16, 100035, "Trống" },
                    { 200393, null, null, 17, 100035, "Trống" },
                    { 200394, null, null, 18, 100035, "Trống" },
                    { 200395, null, null, 19, 100035, "Trống" },
                    { 200396, null, null, 20, 100035, "Trống" },
                    { 200397, null, null, 21, 100035, "Trống" },
                    { 200398, null, null, 22, 100035, "Trống" },
                    { 200399, null, null, 23, 100035, "Trống" },
                    { 200400, null, null, 1, 100036, "Trống" },
                    { 200401, null, null, 2, 100036, "Trống" },
                    { 200402, null, null, 3, 100036, "Trống" },
                    { 200403, null, null, 4, 100036, "Trống" },
                    { 200404, null, null, 5, 100036, "Trống" },
                    { 200405, null, null, 6, 100036, "Trống" },
                    { 200406, null, null, 7, 100036, "Trống" },
                    { 200407, null, null, 8, 100036, "Trống" },
                    { 200408, null, null, 9, 100036, "Trống" },
                    { 200409, null, null, 10, 100036, "Trống" },
                    { 200410, null, null, 11, 100036, "Trống" },
                    { 200411, null, null, 12, 100036, "Trống" },
                    { 200412, null, null, 13, 100036, "Trống" },
                    { 200413, null, null, 14, 100036, "Trống" },
                    { 200414, null, null, 15, 100036, "Trống" },
                    { 200415, null, null, 1, 100037, "Trống" },
                    { 200416, null, null, 2, 100037, "Trống" },
                    { 200417, null, null, 3, 100037, "Trống" },
                    { 200418, null, null, 4, 100037, "Trống" },
                    { 200419, null, null, 5, 100037, "Trống" },
                    { 200420, null, null, 6, 100037, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200421, null, null, 7, 100037, "Trống" },
                    { 200422, null, null, 8, 100037, "Trống" },
                    { 200423, null, null, 9, 100037, "Trống" },
                    { 200424, null, null, 10, 100037, "Trống" },
                    { 200425, null, null, 11, 100037, "Trống" },
                    { 200426, null, null, 12, 100037, "Trống" },
                    { 200427, null, null, 13, 100037, "Trống" },
                    { 200428, null, null, 14, 100037, "Trống" },
                    { 200429, null, null, 15, 100037, "Trống" },
                    { 200430, null, null, 16, 100038, "Trống" },
                    { 200431, null, null, 17, 100038, "Trống" },
                    { 200432, null, null, 18, 100038, "Trống" },
                    { 200433, null, null, 19, 100038, "Trống" },
                    { 200434, null, null, 20, 100038, "Trống" },
                    { 200435, null, null, 21, 100038, "Trống" },
                    { 200436, null, null, 22, 100038, "Trống" },
                    { 200437, null, null, 23, 100038, "Trống" },
                    { 200438, null, null, 16, 100039, "Trống" },
                    { 200439, null, null, 17, 100039, "Trống" },
                    { 200440, null, null, 18, 100039, "Trống" },
                    { 200441, null, null, 19, 100039, "Trống" },
                    { 200442, null, null, 20, 100039, "Trống" },
                    { 200443, null, null, 21, 100039, "Trống" },
                    { 200444, null, null, 22, 100039, "Trống" },
                    { 200445, null, null, 23, 100039, "Trống" },
                    { 200446, null, null, 1, 100040, "Trống" },
                    { 200447, null, null, 2, 100040, "Trống" },
                    { 200448, null, null, 3, 100040, "Trống" },
                    { 200449, null, null, 4, 100040, "Trống" },
                    { 200450, null, null, 5, 100040, "Trống" },
                    { 200451, null, null, 6, 100040, "Trống" },
                    { 200452, null, null, 7, 100040, "Trống" },
                    { 200453, null, null, 8, 100040, "Trống" },
                    { 200454, null, null, 9, 100040, "Trống" },
                    { 200455, null, null, 10, 100040, "Trống" },
                    { 200456, null, null, 11, 100040, "Trống" },
                    { 200457, null, null, 12, 100040, "Trống" },
                    { 200458, null, null, 13, 100040, "Trống" },
                    { 200459, null, null, 14, 100040, "Trống" },
                    { 200460, null, null, 15, 100040, "Trống" },
                    { 200461, null, null, 1, 100041, "Trống" },
                    { 200462, null, null, 2, 100041, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200463, null, null, 3, 100041, "Trống" },
                    { 200464, null, null, 4, 100041, "Trống" },
                    { 200465, null, null, 5, 100041, "Trống" },
                    { 200466, null, null, 6, 100041, "Trống" },
                    { 200467, null, null, 7, 100041, "Trống" },
                    { 200468, null, null, 8, 100041, "Trống" },
                    { 200469, null, null, 9, 100041, "Trống" },
                    { 200470, null, null, 10, 100041, "Trống" },
                    { 200471, null, null, 11, 100041, "Trống" },
                    { 200472, null, null, 12, 100041, "Trống" },
                    { 200473, null, null, 13, 100041, "Trống" },
                    { 200474, null, null, 14, 100041, "Trống" },
                    { 200475, null, null, 15, 100041, "Trống" },
                    { 200476, null, null, 16, 100042, "Trống" },
                    { 200477, null, null, 17, 100042, "Trống" },
                    { 200478, null, null, 18, 100042, "Trống" },
                    { 200479, null, null, 19, 100042, "Trống" },
                    { 200480, null, null, 20, 100042, "Trống" },
                    { 200481, null, null, 21, 100042, "Trống" },
                    { 200482, null, null, 22, 100042, "Trống" },
                    { 200483, null, null, 23, 100042, "Trống" },
                    { 200484, null, null, 16, 100043, "Trống" },
                    { 200485, null, null, 17, 100043, "Trống" },
                    { 200486, null, null, 18, 100043, "Trống" },
                    { 200487, null, null, 19, 100043, "Trống" },
                    { 200488, null, null, 20, 100043, "Trống" },
                    { 200489, null, null, 21, 100043, "Trống" },
                    { 200490, null, null, 22, 100043, "Trống" },
                    { 200491, null, null, 23, 100043, "Trống" },
                    { 200492, null, null, 1, 100044, "Trống" },
                    { 200493, null, null, 2, 100044, "Trống" },
                    { 200494, null, null, 3, 100044, "Trống" },
                    { 200495, null, null, 4, 100044, "Trống" },
                    { 200496, null, null, 5, 100044, "Trống" },
                    { 200497, null, null, 6, 100044, "Trống" },
                    { 200498, null, null, 7, 100044, "Trống" },
                    { 200499, null, null, 8, 100044, "Trống" },
                    { 200500, null, null, 9, 100044, "Trống" },
                    { 200501, null, null, 10, 100044, "Trống" },
                    { 200502, null, null, 11, 100044, "Trống" },
                    { 200503, null, null, 12, 100044, "Trống" },
                    { 200504, null, null, 13, 100044, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200505, null, null, 14, 100044, "Trống" },
                    { 200506, null, null, 15, 100044, "Trống" },
                    { 200507, null, null, 1, 100045, "Trống" },
                    { 200508, null, null, 2, 100045, "Trống" },
                    { 200509, null, null, 3, 100045, "Trống" },
                    { 200510, null, null, 4, 100045, "Trống" },
                    { 200511, null, null, 5, 100045, "Trống" },
                    { 200512, null, null, 6, 100045, "Trống" },
                    { 200513, null, null, 7, 100045, "Trống" },
                    { 200514, null, null, 8, 100045, "Trống" },
                    { 200515, null, null, 9, 100045, "Trống" },
                    { 200516, null, null, 10, 100045, "Trống" },
                    { 200517, null, null, 11, 100045, "Trống" },
                    { 200518, null, null, 12, 100045, "Trống" },
                    { 200519, null, null, 13, 100045, "Trống" },
                    { 200520, null, null, 14, 100045, "Trống" },
                    { 200521, null, null, 15, 100045, "Trống" },
                    { 200522, null, null, 16, 100046, "Trống" },
                    { 200523, null, null, 17, 100046, "Trống" },
                    { 200524, null, null, 18, 100046, "Trống" },
                    { 200525, null, null, 19, 100046, "Trống" },
                    { 200526, null, null, 20, 100046, "Trống" },
                    { 200527, null, null, 21, 100046, "Trống" },
                    { 200528, null, null, 22, 100046, "Trống" },
                    { 200529, null, null, 23, 100046, "Trống" },
                    { 200530, null, null, 16, 100047, "Trống" },
                    { 200531, null, null, 17, 100047, "Trống" },
                    { 200532, null, null, 18, 100047, "Trống" },
                    { 200533, null, null, 19, 100047, "Trống" },
                    { 200534, null, null, 20, 100047, "Trống" },
                    { 200535, null, null, 21, 100047, "Trống" },
                    { 200536, null, null, 22, 100047, "Trống" },
                    { 200537, null, null, 23, 100047, "Trống" },
                    { 200538, null, null, 1, 100048, "Trống" },
                    { 200539, null, null, 2, 100048, "Trống" },
                    { 200540, null, null, 3, 100048, "Trống" },
                    { 200541, null, null, 4, 100048, "Trống" },
                    { 200542, null, null, 5, 100048, "Trống" },
                    { 200543, null, null, 6, 100048, "Trống" },
                    { 200544, null, null, 7, 100048, "Trống" },
                    { 200545, null, null, 8, 100048, "Trống" },
                    { 200546, null, null, 9, 100048, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200547, null, null, 10, 100048, "Trống" },
                    { 200548, null, null, 11, 100048, "Trống" },
                    { 200549, null, null, 12, 100048, "Trống" },
                    { 200550, null, null, 13, 100048, "Trống" },
                    { 200551, null, null, 14, 100048, "Trống" },
                    { 200552, null, null, 15, 100048, "Trống" },
                    { 200553, null, null, 1, 100049, "Trống" },
                    { 200554, null, null, 2, 100049, "Trống" },
                    { 200555, null, null, 3, 100049, "Trống" },
                    { 200556, null, null, 4, 100049, "Trống" },
                    { 200557, null, null, 5, 100049, "Trống" },
                    { 200558, null, null, 6, 100049, "Trống" },
                    { 200559, null, null, 7, 100049, "Trống" },
                    { 200560, null, null, 8, 100049, "Trống" },
                    { 200561, null, null, 9, 100049, "Trống" },
                    { 200562, null, null, 10, 100049, "Trống" },
                    { 200563, null, null, 11, 100049, "Trống" },
                    { 200564, null, null, 12, 100049, "Trống" },
                    { 200565, null, null, 13, 100049, "Trống" },
                    { 200566, null, null, 14, 100049, "Trống" },
                    { 200567, null, null, 15, 100049, "Trống" },
                    { 200568, null, null, 16, 100050, "Trống" },
                    { 200569, null, null, 17, 100050, "Trống" },
                    { 200570, null, null, 18, 100050, "Trống" },
                    { 200571, null, null, 19, 100050, "Trống" },
                    { 200572, null, null, 20, 100050, "Trống" },
                    { 200573, null, null, 21, 100050, "Trống" },
                    { 200574, null, null, 22, 100050, "Trống" },
                    { 200575, null, null, 23, 100050, "Trống" },
                    { 200576, null, null, 16, 100051, "Trống" },
                    { 200577, null, null, 17, 100051, "Trống" },
                    { 200578, null, null, 18, 100051, "Trống" },
                    { 200579, null, null, 19, 100051, "Trống" },
                    { 200580, null, null, 20, 100051, "Trống" },
                    { 200581, null, null, 21, 100051, "Trống" },
                    { 200582, null, null, 22, 100051, "Trống" },
                    { 200583, null, null, 23, 100051, "Trống" },
                    { 200584, null, null, 1, 100052, "Trống" },
                    { 200585, null, null, 2, 100052, "Trống" },
                    { 200586, null, null, 3, 100052, "Trống" },
                    { 200587, null, null, 4, 100052, "Trống" },
                    { 200588, null, null, 5, 100052, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200589, null, null, 6, 100052, "Trống" },
                    { 200590, null, null, 7, 100052, "Trống" },
                    { 200591, null, null, 8, 100052, "Trống" },
                    { 200592, null, null, 9, 100052, "Trống" },
                    { 200593, null, null, 10, 100052, "Trống" },
                    { 200594, null, null, 11, 100052, "Trống" },
                    { 200595, null, null, 12, 100052, "Trống" },
                    { 200596, null, null, 13, 100052, "Trống" },
                    { 200597, null, null, 14, 100052, "Trống" },
                    { 200598, null, null, 15, 100052, "Trống" },
                    { 200599, null, null, 1, 100053, "Trống" },
                    { 200600, null, null, 2, 100053, "Trống" },
                    { 200601, null, null, 3, 100053, "Trống" },
                    { 200602, null, null, 4, 100053, "Trống" },
                    { 200603, null, null, 5, 100053, "Trống" },
                    { 200604, null, null, 6, 100053, "Trống" },
                    { 200605, null, null, 7, 100053, "Trống" },
                    { 200606, null, null, 8, 100053, "Trống" },
                    { 200607, null, null, 9, 100053, "Trống" },
                    { 200608, null, null, 10, 100053, "Trống" },
                    { 200609, null, null, 11, 100053, "Trống" },
                    { 200610, null, null, 12, 100053, "Trống" },
                    { 200611, null, null, 13, 100053, "Trống" },
                    { 200612, null, null, 14, 100053, "Trống" },
                    { 200613, null, null, 15, 100053, "Trống" },
                    { 200614, null, null, 16, 100054, "Trống" },
                    { 200615, null, null, 17, 100054, "Trống" },
                    { 200616, null, null, 18, 100054, "Trống" },
                    { 200617, null, null, 19, 100054, "Trống" },
                    { 200618, null, null, 20, 100054, "Trống" },
                    { 200619, null, null, 21, 100054, "Trống" },
                    { 200620, null, null, 22, 100054, "Trống" },
                    { 200621, null, null, 23, 100054, "Trống" },
                    { 200622, null, null, 16, 100055, "Trống" },
                    { 200623, null, null, 17, 100055, "Trống" },
                    { 200624, null, null, 18, 100055, "Trống" },
                    { 200625, null, null, 19, 100055, "Trống" },
                    { 200626, null, null, 20, 100055, "Trống" },
                    { 200627, null, null, 21, 100055, "Trống" },
                    { 200628, null, null, 22, 100055, "Trống" },
                    { 200629, null, null, 23, 100055, "Trống" },
                    { 200630, null, null, 1, 100056, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200631, null, null, 2, 100056, "Trống" },
                    { 200632, null, null, 3, 100056, "Trống" },
                    { 200633, null, null, 4, 100056, "Trống" },
                    { 200634, null, null, 5, 100056, "Trống" },
                    { 200635, null, null, 6, 100056, "Trống" },
                    { 200636, null, null, 7, 100056, "Trống" },
                    { 200637, null, null, 8, 100056, "Trống" },
                    { 200638, null, null, 9, 100056, "Trống" },
                    { 200639, null, null, 10, 100056, "Trống" },
                    { 200640, null, null, 11, 100056, "Trống" },
                    { 200641, null, null, 12, 100056, "Trống" },
                    { 200642, null, null, 13, 100056, "Trống" },
                    { 200643, null, null, 14, 100056, "Trống" },
                    { 200644, null, null, 15, 100056, "Trống" },
                    { 200645, null, null, 1, 100057, "Trống" },
                    { 200646, null, null, 2, 100057, "Trống" },
                    { 200647, null, null, 3, 100057, "Trống" },
                    { 200648, null, null, 4, 100057, "Trống" },
                    { 200649, null, null, 5, 100057, "Trống" },
                    { 200650, null, null, 6, 100057, "Trống" },
                    { 200651, null, null, 7, 100057, "Trống" },
                    { 200652, null, null, 8, 100057, "Trống" },
                    { 200653, null, null, 9, 100057, "Trống" },
                    { 200654, null, null, 10, 100057, "Trống" },
                    { 200655, null, null, 11, 100057, "Trống" },
                    { 200656, null, null, 12, 100057, "Trống" },
                    { 200657, null, null, 13, 100057, "Trống" },
                    { 200658, null, null, 14, 100057, "Trống" },
                    { 200659, null, null, 15, 100057, "Trống" },
                    { 200660, null, null, 16, 100058, "Trống" },
                    { 200661, null, null, 17, 100058, "Trống" },
                    { 200662, null, null, 18, 100058, "Trống" },
                    { 200663, null, null, 19, 100058, "Trống" },
                    { 200664, null, null, 20, 100058, "Trống" },
                    { 200665, null, null, 21, 100058, "Trống" },
                    { 200666, null, null, 22, 100058, "Trống" },
                    { 200667, null, null, 23, 100058, "Trống" },
                    { 200668, null, null, 1, 100059, "Trống" },
                    { 200669, null, null, 2, 100059, "Trống" },
                    { 200670, null, null, 3, 100059, "Trống" },
                    { 200671, null, null, 4, 100059, "Trống" },
                    { 200672, null, null, 5, 100059, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200673, null, null, 6, 100059, "Trống" },
                    { 200674, null, null, 7, 100059, "Trống" },
                    { 200675, null, null, 8, 100059, "Trống" },
                    { 200676, null, null, 9, 100059, "Trống" },
                    { 200677, null, null, 10, 100059, "Trống" },
                    { 200678, null, null, 11, 100059, "Trống" },
                    { 200679, null, null, 12, 100059, "Trống" },
                    { 200680, null, null, 13, 100059, "Trống" },
                    { 200681, null, null, 14, 100059, "Trống" },
                    { 200682, null, null, 15, 100059, "Trống" },
                    { 200683, null, null, 16, 100060, "Trống" },
                    { 200684, null, null, 17, 100060, "Trống" },
                    { 200685, null, null, 18, 100060, "Trống" },
                    { 200686, null, null, 19, 100060, "Trống" },
                    { 200687, null, null, 20, 100060, "Trống" },
                    { 200688, null, null, 21, 100060, "Trống" },
                    { 200689, null, null, 22, 100060, "Trống" },
                    { 200690, null, null, 23, 100060, "Trống" },
                    { 200691, null, null, 1, 100061, "Trống" },
                    { 200692, null, null, 2, 100061, "Trống" },
                    { 200693, null, null, 3, 100061, "Trống" },
                    { 200694, null, null, 4, 100061, "Trống" },
                    { 200695, null, null, 5, 100061, "Trống" },
                    { 200696, null, null, 6, 100061, "Trống" },
                    { 200697, null, null, 7, 100061, "Trống" },
                    { 200698, null, null, 8, 100061, "Trống" },
                    { 200699, null, null, 9, 100061, "Trống" },
                    { 200700, null, null, 10, 100061, "Trống" },
                    { 200701, null, null, 11, 100061, "Trống" },
                    { 200702, null, null, 12, 100061, "Trống" },
                    { 200703, null, null, 13, 100061, "Trống" },
                    { 200704, null, null, 14, 100061, "Trống" },
                    { 200705, null, null, 15, 100061, "Trống" },
                    { 200706, null, null, 16, 100062, "Trống" },
                    { 200707, null, null, 17, 100062, "Trống" },
                    { 200708, null, null, 18, 100062, "Trống" },
                    { 200709, null, null, 19, 100062, "Trống" },
                    { 200710, null, null, 20, 100062, "Trống" },
                    { 200711, null, null, 21, 100062, "Trống" },
                    { 200712, null, null, 22, 100062, "Trống" },
                    { 200713, null, null, 23, 100062, "Trống" },
                    { 200714, null, null, 1, 100063, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200715, null, null, 2, 100063, "Trống" },
                    { 200716, null, null, 3, 100063, "Trống" },
                    { 200717, null, null, 4, 100063, "Trống" },
                    { 200718, null, null, 5, 100063, "Trống" },
                    { 200719, null, null, 6, 100063, "Trống" },
                    { 200720, null, null, 7, 100063, "Trống" },
                    { 200721, null, null, 8, 100063, "Trống" },
                    { 200722, null, null, 9, 100063, "Trống" },
                    { 200723, null, null, 10, 100063, "Trống" },
                    { 200724, null, null, 11, 100063, "Trống" },
                    { 200725, null, null, 12, 100063, "Trống" },
                    { 200726, null, null, 13, 100063, "Trống" },
                    { 200727, null, null, 14, 100063, "Trống" },
                    { 200728, null, null, 15, 100063, "Trống" },
                    { 200729, null, null, 16, 100064, "Trống" },
                    { 200730, null, null, 17, 100064, "Trống" },
                    { 200731, null, null, 18, 100064, "Trống" },
                    { 200732, null, null, 19, 100064, "Trống" },
                    { 200733, null, null, 20, 100064, "Trống" },
                    { 200734, null, null, 21, 100064, "Trống" },
                    { 200735, null, null, 22, 100064, "Trống" },
                    { 200736, null, null, 23, 100064, "Trống" },
                    { 200737, null, null, 1, 100065, "Trống" },
                    { 200738, null, null, 2, 100065, "Trống" },
                    { 200739, null, null, 3, 100065, "Trống" },
                    { 200740, null, null, 4, 100065, "Trống" },
                    { 200741, null, null, 5, 100065, "Trống" },
                    { 200742, null, null, 6, 100065, "Trống" },
                    { 200743, null, null, 7, 100065, "Trống" },
                    { 200744, null, null, 8, 100065, "Trống" },
                    { 200745, null, null, 9, 100065, "Trống" },
                    { 200746, null, null, 10, 100065, "Trống" },
                    { 200747, null, null, 11, 100065, "Trống" },
                    { 200748, null, null, 12, 100065, "Trống" },
                    { 200749, null, null, 13, 100065, "Trống" },
                    { 200750, null, null, 14, 100065, "Trống" },
                    { 200751, null, null, 15, 100065, "Trống" },
                    { 200752, null, null, 16, 100066, "Trống" },
                    { 200753, null, null, 17, 100066, "Trống" },
                    { 200754, null, null, 18, 100066, "Trống" },
                    { 200755, null, null, 19, 100066, "Trống" },
                    { 200756, null, null, 20, 100066, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200757, null, null, 21, 100066, "Trống" },
                    { 200758, null, null, 22, 100066, "Trống" },
                    { 200759, null, null, 23, 100066, "Trống" },
                    { 200760, null, null, 1, 100067, "Trống" },
                    { 200761, null, null, 2, 100067, "Trống" },
                    { 200762, null, null, 3, 100067, "Trống" },
                    { 200763, null, null, 4, 100067, "Trống" },
                    { 200764, null, null, 5, 100067, "Trống" },
                    { 200765, null, null, 6, 100067, "Trống" },
                    { 200766, null, null, 7, 100067, "Trống" },
                    { 200767, null, null, 8, 100067, "Trống" },
                    { 200768, null, null, 9, 100067, "Trống" },
                    { 200769, null, null, 10, 100067, "Trống" },
                    { 200770, null, null, 11, 100067, "Trống" },
                    { 200771, null, null, 12, 100067, "Trống" },
                    { 200772, null, null, 13, 100067, "Trống" },
                    { 200773, null, null, 14, 100067, "Trống" },
                    { 200774, null, null, 15, 100067, "Trống" },
                    { 200775, null, null, 16, 100068, "Trống" },
                    { 200776, null, null, 17, 100068, "Trống" },
                    { 200777, null, null, 18, 100068, "Trống" },
                    { 200778, null, null, 19, 100068, "Trống" },
                    { 200779, null, null, 20, 100068, "Trống" },
                    { 200780, null, null, 21, 100068, "Trống" },
                    { 200781, null, null, 22, 100068, "Trống" },
                    { 200782, null, null, 23, 100068, "Trống" },
                    { 200783, null, null, 1, 100069, "Trống" },
                    { 200784, null, null, 2, 100069, "Trống" },
                    { 200785, null, null, 3, 100069, "Trống" },
                    { 200786, null, null, 4, 100069, "Trống" },
                    { 200787, null, null, 5, 100069, "Trống" },
                    { 200788, null, null, 6, 100069, "Trống" },
                    { 200789, null, null, 7, 100069, "Trống" },
                    { 200790, null, null, 8, 100069, "Trống" },
                    { 200791, null, null, 9, 100069, "Trống" },
                    { 200792, null, null, 10, 100069, "Trống" },
                    { 200793, null, null, 11, 100069, "Trống" },
                    { 200794, null, null, 12, 100069, "Trống" },
                    { 200795, null, null, 13, 100069, "Trống" },
                    { 200796, null, null, 14, 100069, "Trống" },
                    { 200797, null, null, 15, 100069, "Trống" },
                    { 200798, null, null, 16, 100070, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200799, null, null, 17, 100070, "Trống" },
                    { 200800, null, null, 18, 100070, "Trống" },
                    { 200801, null, null, 19, 100070, "Trống" },
                    { 200802, null, null, 20, 100070, "Trống" },
                    { 200803, null, null, 21, 100070, "Trống" },
                    { 200804, null, null, 22, 100070, "Trống" },
                    { 200805, null, null, 23, 100070, "Trống" },
                    { 200806, null, null, 1, 100071, "Trống" },
                    { 200807, null, null, 2, 100071, "Trống" },
                    { 200808, null, null, 3, 100071, "Trống" },
                    { 200809, null, null, 4, 100071, "Trống" },
                    { 200810, null, null, 5, 100071, "Trống" },
                    { 200811, null, null, 6, 100071, "Trống" },
                    { 200812, null, null, 7, 100071, "Trống" },
                    { 200813, null, null, 8, 100071, "Trống" },
                    { 200814, null, null, 9, 100071, "Trống" },
                    { 200815, null, null, 10, 100071, "Trống" },
                    { 200816, null, null, 11, 100071, "Trống" },
                    { 200817, null, null, 12, 100071, "Trống" },
                    { 200818, null, null, 13, 100071, "Trống" },
                    { 200819, null, null, 14, 100071, "Trống" },
                    { 200820, null, null, 15, 100071, "Trống" },
                    { 200821, null, null, 16, 100072, "Trống" },
                    { 200822, null, null, 17, 100072, "Trống" },
                    { 200823, null, null, 18, 100072, "Trống" },
                    { 200824, null, null, 19, 100072, "Trống" },
                    { 200825, null, null, 20, 100072, "Trống" },
                    { 200826, null, null, 21, 100072, "Trống" },
                    { 200827, null, null, 22, 100072, "Trống" },
                    { 200828, null, null, 23, 100072, "Trống" },
                    { 200829, null, null, 1, 100073, "Trống" },
                    { 200830, null, null, 2, 100073, "Trống" },
                    { 200831, null, null, 3, 100073, "Trống" },
                    { 200832, null, null, 4, 100073, "Trống" },
                    { 200833, null, null, 5, 100073, "Trống" },
                    { 200834, null, null, 6, 100073, "Trống" },
                    { 200835, null, null, 7, 100073, "Trống" },
                    { 200836, null, null, 8, 100073, "Trống" },
                    { 200837, null, null, 9, 100073, "Trống" },
                    { 200838, null, null, 10, 100073, "Trống" },
                    { 200839, null, null, 11, 100073, "Trống" },
                    { 200840, null, null, 12, 100073, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 200841, null, null, 13, 100073, "Trống" },
                    { 200842, null, null, 14, 100073, "Trống" },
                    { 200843, null, null, 15, 100073, "Trống" },
                    { 200844, null, null, 16, 100074, "Trống" },
                    { 200845, null, null, 17, 100074, "Trống" },
                    { 200846, null, null, 18, 100074, "Trống" },
                    { 200847, null, null, 19, 100074, "Trống" },
                    { 200848, null, null, 20, 100074, "Trống" },
                    { 200849, null, null, 21, 100074, "Trống" },
                    { 200850, null, null, 22, 100074, "Trống" },
                    { 200851, null, null, 23, 100074, "Trống" }
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
                name: "IX_PendingChanges_ReviewedBy",
                table: "PendingChanges",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PendingChanges_SubmittedBy",
                table: "PendingChanges",
                column: "SubmittedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefundRequests_AdminApprovedBy",
                table: "RefundRequests",
                column: "AdminApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefundRequests_CustomerId",
                table: "RefundRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundRequests_RejectedBy",
                table: "RefundRequests",
                column: "RejectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefundRequests_StaffApprovedBy",
                table: "RefundRequests",
                column: "StaffApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefundRequests_TicketId",
                table: "RefundRequests",
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
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PendingChanges");

            migrationBuilder.DropTable(
                name: "RefundRequests");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TicketCombos");

            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Users");

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

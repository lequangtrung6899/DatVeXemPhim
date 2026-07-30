using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    public partial class themsuatchieuvagiughe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeldBySessionId",
                table: "ShowtimeSeats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "ShowtimeId", "EndTime", "MovieId", "RoomId", "StartTime", "Status", "TicketPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 29, 11, 13, 0, 0, DateTimeKind.Unspecified), 6, 1, new DateTime(2026, 7, 29, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 2, new DateTime(2026, 8, 2, 11, 13, 0, 0, DateTimeKind.Unspecified), 6, 2, new DateTime(2026, 8, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 3, new DateTime(2026, 7, 30, 11, 40, 0, 0, DateTimeKind.Unspecified), 7, 2, new DateTime(2026, 7, 30, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 4, new DateTime(2026, 8, 3, 11, 40, 0, 0, DateTimeKind.Unspecified), 7, 1, new DateTime(2026, 8, 3, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 5, new DateTime(2026, 7, 31, 11, 16, 0, 0, DateTimeKind.Unspecified), 10, 1, new DateTime(2026, 7, 31, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 6, new DateTime(2026, 8, 4, 11, 16, 0, 0, DateTimeKind.Unspecified), 10, 2, new DateTime(2026, 8, 4, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 7, new DateTime(2026, 8, 1, 11, 51, 0, 0, DateTimeKind.Unspecified), 11, 2, new DateTime(2026, 8, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 8, new DateTime(2026, 7, 29, 13, 54, 0, 0, DateTimeKind.Unspecified), 11, 1, new DateTime(2026, 7, 29, 11, 33, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 9, new DateTime(2026, 8, 2, 11, 35, 0, 0, DateTimeKind.Unspecified), 13, 1, new DateTime(2026, 8, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 10, new DateTime(2026, 7, 30, 14, 5, 0, 0, DateTimeKind.Unspecified), 13, 2, new DateTime(2026, 7, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 11, new DateTime(2026, 8, 3, 11, 4, 0, 0, DateTimeKind.Unspecified), 18, 2, new DateTime(2026, 8, 3, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 12, new DateTime(2026, 7, 31, 13, 10, 0, 0, DateTimeKind.Unspecified), 18, 1, new DateTime(2026, 7, 31, 11, 36, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 13, new DateTime(2026, 8, 4, 11, 24, 0, 0, DateTimeKind.Unspecified), 21, 1, new DateTime(2026, 8, 4, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 14, new DateTime(2026, 8, 1, 14, 5, 0, 0, DateTimeKind.Unspecified), 21, 2, new DateTime(2026, 8, 1, 12, 11, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 15, new DateTime(2026, 7, 29, 11, 6, 0, 0, DateTimeKind.Unspecified), 25, 2, new DateTime(2026, 7, 29, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 16, new DateTime(2026, 8, 2, 13, 31, 0, 0, DateTimeKind.Unspecified), 25, 1, new DateTime(2026, 8, 2, 11, 55, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 17, new DateTime(2026, 7, 30, 12, 16, 0, 0, DateTimeKind.Unspecified), 26, 1, new DateTime(2026, 7, 30, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 18, new DateTime(2026, 8, 3, 14, 10, 0, 0, DateTimeKind.Unspecified), 26, 2, new DateTime(2026, 8, 3, 11, 24, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 19, new DateTime(2026, 7, 31, 11, 29, 0, 0, DateTimeKind.Unspecified), 28, 2, new DateTime(2026, 7, 31, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 20, new DateTime(2026, 8, 4, 13, 43, 0, 0, DateTimeKind.Unspecified), 28, 1, new DateTime(2026, 8, 4, 11, 44, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 21, new DateTime(2026, 8, 1, 11, 43, 0, 0, DateTimeKind.Unspecified), 29, 1, new DateTime(2026, 8, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 22, new DateTime(2026, 7, 29, 13, 39, 0, 0, DateTimeKind.Unspecified), 29, 2, new DateTime(2026, 7, 29, 11, 26, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 23, new DateTime(2026, 8, 2, 13, 54, 0, 0, DateTimeKind.Unspecified), 30, 2, new DateTime(2026, 8, 2, 11, 33, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 24, new DateTime(2026, 7, 30, 14, 57, 0, 0, DateTimeKind.Unspecified), 30, 1, new DateTime(2026, 7, 30, 12, 36, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 25, new DateTime(2026, 8, 3, 13, 34, 0, 0, DateTimeKind.Unspecified), 34, 1, new DateTime(2026, 8, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 26, new DateTime(2026, 7, 31, 13, 23, 0, 0, DateTimeKind.Unspecified), 34, 2, new DateTime(2026, 7, 31, 11, 49, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 27, new DateTime(2026, 8, 4, 13, 13, 0, 0, DateTimeKind.Unspecified), 35, 2, new DateTime(2026, 8, 4, 11, 36, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 28, new DateTime(2026, 8, 1, 13, 40, 0, 0, DateTimeKind.Unspecified), 35, 1, new DateTime(2026, 8, 1, 12, 3, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 29, new DateTime(2026, 8, 5, 11, 25, 0, 0, DateTimeKind.Unspecified), 2, 1, new DateTime(2026, 8, 5, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 30, new DateTime(2026, 8, 9, 11, 25, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(2026, 8, 9, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 31, new DateTime(2026, 8, 6, 11, 58, 0, 0, DateTimeKind.Unspecified), 4, 2, new DateTime(2026, 8, 6, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 32, new DateTime(2026, 8, 10, 11, 58, 0, 0, DateTimeKind.Unspecified), 4, 1, new DateTime(2026, 8, 10, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 33, new DateTime(2026, 8, 7, 11, 25, 0, 0, DateTimeKind.Unspecified), 9, 1, new DateTime(2026, 8, 7, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 34, new DateTime(2026, 8, 11, 11, 25, 0, 0, DateTimeKind.Unspecified), 9, 2, new DateTime(2026, 8, 11, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 35, new DateTime(2026, 8, 8, 11, 37, 0, 0, DateTimeKind.Unspecified), 12, 2, new DateTime(2026, 8, 8, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 36, new DateTime(2026, 8, 5, 13, 52, 0, 0, DateTimeKind.Unspecified), 12, 1, new DateTime(2026, 8, 5, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 37, new DateTime(2026, 8, 9, 11, 15, 0, 0, DateTimeKind.Unspecified), 14, 1, new DateTime(2026, 8, 9, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 38, new DateTime(2026, 8, 6, 14, 3, 0, 0, DateTimeKind.Unspecified), 14, 2, new DateTime(2026, 8, 6, 12, 18, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 39, new DateTime(2026, 8, 10, 11, 11, 0, 0, DateTimeKind.Unspecified), 15, 2, new DateTime(2026, 8, 10, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 40, new DateTime(2026, 8, 7, 13, 26, 0, 0, DateTimeKind.Unspecified), 15, 1, new DateTime(2026, 8, 7, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 41, new DateTime(2026, 8, 11, 11, 6, 0, 0, DateTimeKind.Unspecified), 16, 1, new DateTime(2026, 8, 11, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 42, new DateTime(2026, 8, 8, 13, 33, 0, 0, DateTimeKind.Unspecified), 16, 2, new DateTime(2026, 8, 8, 11, 57, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "ShowtimeId", "EndTime", "MovieId", "RoomId", "StartTime", "Status", "TicketPrice" },
                values: new object[,]
                {
                    { 43, new DateTime(2026, 8, 5, 11, 10, 0, 0, DateTimeKind.Unspecified), 17, 2, new DateTime(2026, 8, 5, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 44, new DateTime(2026, 8, 9, 13, 15, 0, 0, DateTimeKind.Unspecified), 17, 1, new DateTime(2026, 8, 9, 11, 35, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 45, new DateTime(2026, 8, 6, 11, 13, 0, 0, DateTimeKind.Unspecified), 22, 1, new DateTime(2026, 8, 6, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 46, new DateTime(2026, 8, 10, 13, 14, 0, 0, DateTimeKind.Unspecified), 22, 2, new DateTime(2026, 8, 10, 11, 31, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 47, new DateTime(2026, 8, 7, 11, 49, 0, 0, DateTimeKind.Unspecified), 23, 2, new DateTime(2026, 8, 7, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 48, new DateTime(2026, 8, 11, 13, 45, 0, 0, DateTimeKind.Unspecified), 23, 1, new DateTime(2026, 8, 11, 11, 26, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 49, new DateTime(2026, 8, 8, 11, 3, 0, 0, DateTimeKind.Unspecified), 24, 1, new DateTime(2026, 8, 8, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 50, new DateTime(2026, 8, 5, 13, 3, 0, 0, DateTimeKind.Unspecified), 24, 2, new DateTime(2026, 8, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 51, new DateTime(2026, 8, 9, 13, 40, 0, 0, DateTimeKind.Unspecified), 27, 2, new DateTime(2026, 8, 9, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 52, new DateTime(2026, 8, 6, 13, 28, 0, 0, DateTimeKind.Unspecified), 27, 1, new DateTime(2026, 8, 6, 11, 33, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 53, new DateTime(2026, 8, 10, 13, 43, 0, 0, DateTimeKind.Unspecified), 32, 1, new DateTime(2026, 8, 10, 12, 18, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 54, new DateTime(2026, 8, 7, 13, 34, 0, 0, DateTimeKind.Unspecified), 32, 2, new DateTime(2026, 8, 7, 12, 9, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 55, new DateTime(2026, 8, 11, 13, 25, 0, 0, DateTimeKind.Unspecified), 33, 2, new DateTime(2026, 8, 11, 11, 45, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m },
                    { 56, new DateTime(2026, 8, 8, 13, 3, 0, 0, DateTimeKind.Unspecified), 33, 1, new DateTime(2026, 8, 8, 11, 23, 0, 0, DateTimeKind.Unspecified), "Sắp chiếu", 75000m }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 1, null, null, 1, 1, "Trống" },
                    { 2, null, null, 2, 1, "Trống" },
                    { 3, null, null, 3, 1, "Trống" },
                    { 4, null, null, 4, 1, "Trống" },
                    { 5, null, null, 5, 1, "Trống" },
                    { 6, null, null, 6, 1, "Trống" },
                    { 7, null, null, 7, 1, "Trống" },
                    { 8, null, null, 8, 1, "Trống" },
                    { 9, null, null, 9, 1, "Trống" },
                    { 10, null, null, 10, 1, "Trống" },
                    { 11, null, null, 11, 1, "Trống" },
                    { 12, null, null, 12, 1, "Trống" },
                    { 13, null, null, 13, 1, "Trống" },
                    { 14, null, null, 14, 1, "Trống" },
                    { 15, null, null, 15, 1, "Trống" },
                    { 16, null, null, 16, 2, "Trống" },
                    { 17, null, null, 17, 2, "Trống" },
                    { 18, null, null, 18, 2, "Trống" },
                    { 19, null, null, 19, 2, "Trống" },
                    { 20, null, null, 20, 2, "Trống" },
                    { 21, null, null, 21, 2, "Trống" },
                    { 22, null, null, 22, 2, "Trống" },
                    { 23, null, null, 23, 2, "Trống" },
                    { 24, null, null, 16, 3, "Trống" },
                    { 25, null, null, 17, 3, "Trống" },
                    { 26, null, null, 18, 3, "Trống" },
                    { 27, null, null, 19, 3, "Trống" },
                    { 28, null, null, 20, 3, "Trống" },
                    { 29, null, null, 21, 3, "Trống" },
                    { 30, null, null, 22, 3, "Trống" },
                    { 31, null, null, 23, 3, "Trống" },
                    { 32, null, null, 1, 4, "Trống" },
                    { 33, null, null, 2, 4, "Trống" },
                    { 34, null, null, 3, 4, "Trống" },
                    { 35, null, null, 4, 4, "Trống" },
                    { 36, null, null, 5, 4, "Trống" },
                    { 37, null, null, 6, 4, "Trống" },
                    { 38, null, null, 7, 4, "Trống" },
                    { 39, null, null, 8, 4, "Trống" },
                    { 40, null, null, 9, 4, "Trống" },
                    { 41, null, null, 10, 4, "Trống" },
                    { 42, null, null, 11, 4, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 43, null, null, 12, 4, "Trống" },
                    { 44, null, null, 13, 4, "Trống" },
                    { 45, null, null, 14, 4, "Trống" },
                    { 46, null, null, 15, 4, "Trống" },
                    { 47, null, null, 1, 5, "Trống" },
                    { 48, null, null, 2, 5, "Trống" },
                    { 49, null, null, 3, 5, "Trống" },
                    { 50, null, null, 4, 5, "Trống" },
                    { 51, null, null, 5, 5, "Trống" },
                    { 52, null, null, 6, 5, "Trống" },
                    { 53, null, null, 7, 5, "Trống" },
                    { 54, null, null, 8, 5, "Trống" },
                    { 55, null, null, 9, 5, "Trống" },
                    { 56, null, null, 10, 5, "Trống" },
                    { 57, null, null, 11, 5, "Trống" },
                    { 58, null, null, 12, 5, "Trống" },
                    { 59, null, null, 13, 5, "Trống" },
                    { 60, null, null, 14, 5, "Trống" },
                    { 61, null, null, 15, 5, "Trống" },
                    { 62, null, null, 16, 6, "Trống" },
                    { 63, null, null, 17, 6, "Trống" },
                    { 64, null, null, 18, 6, "Trống" },
                    { 65, null, null, 19, 6, "Trống" },
                    { 66, null, null, 20, 6, "Trống" },
                    { 67, null, null, 21, 6, "Trống" },
                    { 68, null, null, 22, 6, "Trống" },
                    { 69, null, null, 23, 6, "Trống" },
                    { 70, null, null, 16, 7, "Trống" },
                    { 71, null, null, 17, 7, "Trống" },
                    { 72, null, null, 18, 7, "Trống" },
                    { 73, null, null, 19, 7, "Trống" },
                    { 74, null, null, 20, 7, "Trống" },
                    { 75, null, null, 21, 7, "Trống" },
                    { 76, null, null, 22, 7, "Trống" },
                    { 77, null, null, 23, 7, "Trống" },
                    { 78, null, null, 1, 8, "Trống" },
                    { 79, null, null, 2, 8, "Trống" },
                    { 80, null, null, 3, 8, "Trống" },
                    { 81, null, null, 4, 8, "Trống" },
                    { 82, null, null, 5, 8, "Trống" },
                    { 83, null, null, 6, 8, "Trống" },
                    { 84, null, null, 7, 8, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 85, null, null, 8, 8, "Trống" },
                    { 86, null, null, 9, 8, "Trống" },
                    { 87, null, null, 10, 8, "Trống" },
                    { 88, null, null, 11, 8, "Trống" },
                    { 89, null, null, 12, 8, "Trống" },
                    { 90, null, null, 13, 8, "Trống" },
                    { 91, null, null, 14, 8, "Trống" },
                    { 92, null, null, 15, 8, "Trống" },
                    { 93, null, null, 1, 9, "Trống" },
                    { 94, null, null, 2, 9, "Trống" },
                    { 95, null, null, 3, 9, "Trống" },
                    { 96, null, null, 4, 9, "Trống" },
                    { 97, null, null, 5, 9, "Trống" },
                    { 98, null, null, 6, 9, "Trống" },
                    { 99, null, null, 7, 9, "Trống" },
                    { 100, null, null, 8, 9, "Trống" },
                    { 101, null, null, 9, 9, "Trống" },
                    { 102, null, null, 10, 9, "Trống" },
                    { 103, null, null, 11, 9, "Trống" },
                    { 104, null, null, 12, 9, "Trống" },
                    { 105, null, null, 13, 9, "Trống" },
                    { 106, null, null, 14, 9, "Trống" },
                    { 107, null, null, 15, 9, "Trống" },
                    { 108, null, null, 16, 10, "Trống" },
                    { 109, null, null, 17, 10, "Trống" },
                    { 110, null, null, 18, 10, "Trống" },
                    { 111, null, null, 19, 10, "Trống" },
                    { 112, null, null, 20, 10, "Trống" },
                    { 113, null, null, 21, 10, "Trống" },
                    { 114, null, null, 22, 10, "Trống" },
                    { 115, null, null, 23, 10, "Trống" },
                    { 116, null, null, 16, 11, "Trống" },
                    { 117, null, null, 17, 11, "Trống" },
                    { 118, null, null, 18, 11, "Trống" },
                    { 119, null, null, 19, 11, "Trống" },
                    { 120, null, null, 20, 11, "Trống" },
                    { 121, null, null, 21, 11, "Trống" },
                    { 122, null, null, 22, 11, "Trống" },
                    { 123, null, null, 23, 11, "Trống" },
                    { 124, null, null, 1, 12, "Trống" },
                    { 125, null, null, 2, 12, "Trống" },
                    { 126, null, null, 3, 12, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 127, null, null, 4, 12, "Trống" },
                    { 128, null, null, 5, 12, "Trống" },
                    { 129, null, null, 6, 12, "Trống" },
                    { 130, null, null, 7, 12, "Trống" },
                    { 131, null, null, 8, 12, "Trống" },
                    { 132, null, null, 9, 12, "Trống" },
                    { 133, null, null, 10, 12, "Trống" },
                    { 134, null, null, 11, 12, "Trống" },
                    { 135, null, null, 12, 12, "Trống" },
                    { 136, null, null, 13, 12, "Trống" },
                    { 137, null, null, 14, 12, "Trống" },
                    { 138, null, null, 15, 12, "Trống" },
                    { 139, null, null, 1, 13, "Trống" },
                    { 140, null, null, 2, 13, "Trống" },
                    { 141, null, null, 3, 13, "Trống" },
                    { 142, null, null, 4, 13, "Trống" },
                    { 143, null, null, 5, 13, "Trống" },
                    { 144, null, null, 6, 13, "Trống" },
                    { 145, null, null, 7, 13, "Trống" },
                    { 146, null, null, 8, 13, "Trống" },
                    { 147, null, null, 9, 13, "Trống" },
                    { 148, null, null, 10, 13, "Trống" },
                    { 149, null, null, 11, 13, "Trống" },
                    { 150, null, null, 12, 13, "Trống" },
                    { 151, null, null, 13, 13, "Trống" },
                    { 152, null, null, 14, 13, "Trống" },
                    { 153, null, null, 15, 13, "Trống" },
                    { 154, null, null, 16, 14, "Trống" },
                    { 155, null, null, 17, 14, "Trống" },
                    { 156, null, null, 18, 14, "Trống" },
                    { 157, null, null, 19, 14, "Trống" },
                    { 158, null, null, 20, 14, "Trống" },
                    { 159, null, null, 21, 14, "Trống" },
                    { 160, null, null, 22, 14, "Trống" },
                    { 161, null, null, 23, 14, "Trống" },
                    { 162, null, null, 16, 15, "Trống" },
                    { 163, null, null, 17, 15, "Trống" },
                    { 164, null, null, 18, 15, "Trống" },
                    { 165, null, null, 19, 15, "Trống" },
                    { 166, null, null, 20, 15, "Trống" },
                    { 167, null, null, 21, 15, "Trống" },
                    { 168, null, null, 22, 15, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 169, null, null, 23, 15, "Trống" },
                    { 170, null, null, 1, 16, "Trống" },
                    { 171, null, null, 2, 16, "Trống" },
                    { 172, null, null, 3, 16, "Trống" },
                    { 173, null, null, 4, 16, "Trống" },
                    { 174, null, null, 5, 16, "Trống" },
                    { 175, null, null, 6, 16, "Trống" },
                    { 176, null, null, 7, 16, "Trống" },
                    { 177, null, null, 8, 16, "Trống" },
                    { 178, null, null, 9, 16, "Trống" },
                    { 179, null, null, 10, 16, "Trống" },
                    { 180, null, null, 11, 16, "Trống" },
                    { 181, null, null, 12, 16, "Trống" },
                    { 182, null, null, 13, 16, "Trống" },
                    { 183, null, null, 14, 16, "Trống" },
                    { 184, null, null, 15, 16, "Trống" },
                    { 185, null, null, 1, 17, "Trống" },
                    { 186, null, null, 2, 17, "Trống" },
                    { 187, null, null, 3, 17, "Trống" },
                    { 188, null, null, 4, 17, "Trống" },
                    { 189, null, null, 5, 17, "Trống" },
                    { 190, null, null, 6, 17, "Trống" },
                    { 191, null, null, 7, 17, "Trống" },
                    { 192, null, null, 8, 17, "Trống" },
                    { 193, null, null, 9, 17, "Trống" },
                    { 194, null, null, 10, 17, "Trống" },
                    { 195, null, null, 11, 17, "Trống" },
                    { 196, null, null, 12, 17, "Trống" },
                    { 197, null, null, 13, 17, "Trống" },
                    { 198, null, null, 14, 17, "Trống" },
                    { 199, null, null, 15, 17, "Trống" },
                    { 200, null, null, 16, 18, "Trống" },
                    { 201, null, null, 17, 18, "Trống" },
                    { 202, null, null, 18, 18, "Trống" },
                    { 203, null, null, 19, 18, "Trống" },
                    { 204, null, null, 20, 18, "Trống" },
                    { 205, null, null, 21, 18, "Trống" },
                    { 206, null, null, 22, 18, "Trống" },
                    { 207, null, null, 23, 18, "Trống" },
                    { 208, null, null, 16, 19, "Trống" },
                    { 209, null, null, 17, 19, "Trống" },
                    { 210, null, null, 18, 19, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 211, null, null, 19, 19, "Trống" },
                    { 212, null, null, 20, 19, "Trống" },
                    { 213, null, null, 21, 19, "Trống" },
                    { 214, null, null, 22, 19, "Trống" },
                    { 215, null, null, 23, 19, "Trống" },
                    { 216, null, null, 1, 20, "Trống" },
                    { 217, null, null, 2, 20, "Trống" },
                    { 218, null, null, 3, 20, "Trống" },
                    { 219, null, null, 4, 20, "Trống" },
                    { 220, null, null, 5, 20, "Trống" },
                    { 221, null, null, 6, 20, "Trống" },
                    { 222, null, null, 7, 20, "Trống" },
                    { 223, null, null, 8, 20, "Trống" },
                    { 224, null, null, 9, 20, "Trống" },
                    { 225, null, null, 10, 20, "Trống" },
                    { 226, null, null, 11, 20, "Trống" },
                    { 227, null, null, 12, 20, "Trống" },
                    { 228, null, null, 13, 20, "Trống" },
                    { 229, null, null, 14, 20, "Trống" },
                    { 230, null, null, 15, 20, "Trống" },
                    { 231, null, null, 1, 21, "Trống" },
                    { 232, null, null, 2, 21, "Trống" },
                    { 233, null, null, 3, 21, "Trống" },
                    { 234, null, null, 4, 21, "Trống" },
                    { 235, null, null, 5, 21, "Trống" },
                    { 236, null, null, 6, 21, "Trống" },
                    { 237, null, null, 7, 21, "Trống" },
                    { 238, null, null, 8, 21, "Trống" },
                    { 239, null, null, 9, 21, "Trống" },
                    { 240, null, null, 10, 21, "Trống" },
                    { 241, null, null, 11, 21, "Trống" },
                    { 242, null, null, 12, 21, "Trống" },
                    { 243, null, null, 13, 21, "Trống" },
                    { 244, null, null, 14, 21, "Trống" },
                    { 245, null, null, 15, 21, "Trống" },
                    { 246, null, null, 16, 22, "Trống" },
                    { 247, null, null, 17, 22, "Trống" },
                    { 248, null, null, 18, 22, "Trống" },
                    { 249, null, null, 19, 22, "Trống" },
                    { 250, null, null, 20, 22, "Trống" },
                    { 251, null, null, 21, 22, "Trống" },
                    { 252, null, null, 22, 22, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 253, null, null, 23, 22, "Trống" },
                    { 254, null, null, 16, 23, "Trống" },
                    { 255, null, null, 17, 23, "Trống" },
                    { 256, null, null, 18, 23, "Trống" },
                    { 257, null, null, 19, 23, "Trống" },
                    { 258, null, null, 20, 23, "Trống" },
                    { 259, null, null, 21, 23, "Trống" },
                    { 260, null, null, 22, 23, "Trống" },
                    { 261, null, null, 23, 23, "Trống" },
                    { 262, null, null, 1, 24, "Trống" },
                    { 263, null, null, 2, 24, "Trống" },
                    { 264, null, null, 3, 24, "Trống" },
                    { 265, null, null, 4, 24, "Trống" },
                    { 266, null, null, 5, 24, "Trống" },
                    { 267, null, null, 6, 24, "Trống" },
                    { 268, null, null, 7, 24, "Trống" },
                    { 269, null, null, 8, 24, "Trống" },
                    { 270, null, null, 9, 24, "Trống" },
                    { 271, null, null, 10, 24, "Trống" },
                    { 272, null, null, 11, 24, "Trống" },
                    { 273, null, null, 12, 24, "Trống" },
                    { 274, null, null, 13, 24, "Trống" },
                    { 275, null, null, 14, 24, "Trống" },
                    { 276, null, null, 15, 24, "Trống" },
                    { 277, null, null, 1, 25, "Trống" },
                    { 278, null, null, 2, 25, "Trống" },
                    { 279, null, null, 3, 25, "Trống" },
                    { 280, null, null, 4, 25, "Trống" },
                    { 281, null, null, 5, 25, "Trống" },
                    { 282, null, null, 6, 25, "Trống" },
                    { 283, null, null, 7, 25, "Trống" },
                    { 284, null, null, 8, 25, "Trống" },
                    { 285, null, null, 9, 25, "Trống" },
                    { 286, null, null, 10, 25, "Trống" },
                    { 287, null, null, 11, 25, "Trống" },
                    { 288, null, null, 12, 25, "Trống" },
                    { 289, null, null, 13, 25, "Trống" },
                    { 290, null, null, 14, 25, "Trống" },
                    { 291, null, null, 15, 25, "Trống" },
                    { 292, null, null, 16, 26, "Trống" },
                    { 293, null, null, 17, 26, "Trống" },
                    { 294, null, null, 18, 26, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 295, null, null, 19, 26, "Trống" },
                    { 296, null, null, 20, 26, "Trống" },
                    { 297, null, null, 21, 26, "Trống" },
                    { 298, null, null, 22, 26, "Trống" },
                    { 299, null, null, 23, 26, "Trống" },
                    { 300, null, null, 16, 27, "Trống" },
                    { 301, null, null, 17, 27, "Trống" },
                    { 302, null, null, 18, 27, "Trống" },
                    { 303, null, null, 19, 27, "Trống" },
                    { 304, null, null, 20, 27, "Trống" },
                    { 305, null, null, 21, 27, "Trống" },
                    { 306, null, null, 22, 27, "Trống" },
                    { 307, null, null, 23, 27, "Trống" },
                    { 308, null, null, 1, 28, "Trống" },
                    { 309, null, null, 2, 28, "Trống" },
                    { 310, null, null, 3, 28, "Trống" },
                    { 311, null, null, 4, 28, "Trống" },
                    { 312, null, null, 5, 28, "Trống" },
                    { 313, null, null, 6, 28, "Trống" },
                    { 314, null, null, 7, 28, "Trống" },
                    { 315, null, null, 8, 28, "Trống" },
                    { 316, null, null, 9, 28, "Trống" },
                    { 317, null, null, 10, 28, "Trống" },
                    { 318, null, null, 11, 28, "Trống" },
                    { 319, null, null, 12, 28, "Trống" },
                    { 320, null, null, 13, 28, "Trống" },
                    { 321, null, null, 14, 28, "Trống" },
                    { 322, null, null, 15, 28, "Trống" },
                    { 323, null, null, 1, 29, "Trống" },
                    { 324, null, null, 2, 29, "Trống" },
                    { 325, null, null, 3, 29, "Trống" },
                    { 326, null, null, 4, 29, "Trống" },
                    { 327, null, null, 5, 29, "Trống" },
                    { 328, null, null, 6, 29, "Trống" },
                    { 329, null, null, 7, 29, "Trống" },
                    { 330, null, null, 8, 29, "Trống" },
                    { 331, null, null, 9, 29, "Trống" },
                    { 332, null, null, 10, 29, "Trống" },
                    { 333, null, null, 11, 29, "Trống" },
                    { 334, null, null, 12, 29, "Trống" },
                    { 335, null, null, 13, 29, "Trống" },
                    { 336, null, null, 14, 29, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 337, null, null, 15, 29, "Trống" },
                    { 338, null, null, 16, 30, "Trống" },
                    { 339, null, null, 17, 30, "Trống" },
                    { 340, null, null, 18, 30, "Trống" },
                    { 341, null, null, 19, 30, "Trống" },
                    { 342, null, null, 20, 30, "Trống" },
                    { 343, null, null, 21, 30, "Trống" },
                    { 344, null, null, 22, 30, "Trống" },
                    { 345, null, null, 23, 30, "Trống" },
                    { 346, null, null, 16, 31, "Trống" },
                    { 347, null, null, 17, 31, "Trống" },
                    { 348, null, null, 18, 31, "Trống" },
                    { 349, null, null, 19, 31, "Trống" },
                    { 350, null, null, 20, 31, "Trống" },
                    { 351, null, null, 21, 31, "Trống" },
                    { 352, null, null, 22, 31, "Trống" },
                    { 353, null, null, 23, 31, "Trống" },
                    { 354, null, null, 1, 32, "Trống" },
                    { 355, null, null, 2, 32, "Trống" },
                    { 356, null, null, 3, 32, "Trống" },
                    { 357, null, null, 4, 32, "Trống" },
                    { 358, null, null, 5, 32, "Trống" },
                    { 359, null, null, 6, 32, "Trống" },
                    { 360, null, null, 7, 32, "Trống" },
                    { 361, null, null, 8, 32, "Trống" },
                    { 362, null, null, 9, 32, "Trống" },
                    { 363, null, null, 10, 32, "Trống" },
                    { 364, null, null, 11, 32, "Trống" },
                    { 365, null, null, 12, 32, "Trống" },
                    { 366, null, null, 13, 32, "Trống" },
                    { 367, null, null, 14, 32, "Trống" },
                    { 368, null, null, 15, 32, "Trống" },
                    { 369, null, null, 1, 33, "Trống" },
                    { 370, null, null, 2, 33, "Trống" },
                    { 371, null, null, 3, 33, "Trống" },
                    { 372, null, null, 4, 33, "Trống" },
                    { 373, null, null, 5, 33, "Trống" },
                    { 374, null, null, 6, 33, "Trống" },
                    { 375, null, null, 7, 33, "Trống" },
                    { 376, null, null, 8, 33, "Trống" },
                    { 377, null, null, 9, 33, "Trống" },
                    { 378, null, null, 10, 33, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 379, null, null, 11, 33, "Trống" },
                    { 380, null, null, 12, 33, "Trống" },
                    { 381, null, null, 13, 33, "Trống" },
                    { 382, null, null, 14, 33, "Trống" },
                    { 383, null, null, 15, 33, "Trống" },
                    { 384, null, null, 16, 34, "Trống" },
                    { 385, null, null, 17, 34, "Trống" },
                    { 386, null, null, 18, 34, "Trống" },
                    { 387, null, null, 19, 34, "Trống" },
                    { 388, null, null, 20, 34, "Trống" },
                    { 389, null, null, 21, 34, "Trống" },
                    { 390, null, null, 22, 34, "Trống" },
                    { 391, null, null, 23, 34, "Trống" },
                    { 392, null, null, 16, 35, "Trống" },
                    { 393, null, null, 17, 35, "Trống" },
                    { 394, null, null, 18, 35, "Trống" },
                    { 395, null, null, 19, 35, "Trống" },
                    { 396, null, null, 20, 35, "Trống" },
                    { 397, null, null, 21, 35, "Trống" },
                    { 398, null, null, 22, 35, "Trống" },
                    { 399, null, null, 23, 35, "Trống" },
                    { 400, null, null, 1, 36, "Trống" },
                    { 401, null, null, 2, 36, "Trống" },
                    { 402, null, null, 3, 36, "Trống" },
                    { 403, null, null, 4, 36, "Trống" },
                    { 404, null, null, 5, 36, "Trống" },
                    { 405, null, null, 6, 36, "Trống" },
                    { 406, null, null, 7, 36, "Trống" },
                    { 407, null, null, 8, 36, "Trống" },
                    { 408, null, null, 9, 36, "Trống" },
                    { 409, null, null, 10, 36, "Trống" },
                    { 410, null, null, 11, 36, "Trống" },
                    { 411, null, null, 12, 36, "Trống" },
                    { 412, null, null, 13, 36, "Trống" },
                    { 413, null, null, 14, 36, "Trống" },
                    { 414, null, null, 15, 36, "Trống" },
                    { 415, null, null, 1, 37, "Trống" },
                    { 416, null, null, 2, 37, "Trống" },
                    { 417, null, null, 3, 37, "Trống" },
                    { 418, null, null, 4, 37, "Trống" },
                    { 419, null, null, 5, 37, "Trống" },
                    { 420, null, null, 6, 37, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 421, null, null, 7, 37, "Trống" },
                    { 422, null, null, 8, 37, "Trống" },
                    { 423, null, null, 9, 37, "Trống" },
                    { 424, null, null, 10, 37, "Trống" },
                    { 425, null, null, 11, 37, "Trống" },
                    { 426, null, null, 12, 37, "Trống" },
                    { 427, null, null, 13, 37, "Trống" },
                    { 428, null, null, 14, 37, "Trống" },
                    { 429, null, null, 15, 37, "Trống" },
                    { 430, null, null, 16, 38, "Trống" },
                    { 431, null, null, 17, 38, "Trống" },
                    { 432, null, null, 18, 38, "Trống" },
                    { 433, null, null, 19, 38, "Trống" },
                    { 434, null, null, 20, 38, "Trống" },
                    { 435, null, null, 21, 38, "Trống" },
                    { 436, null, null, 22, 38, "Trống" },
                    { 437, null, null, 23, 38, "Trống" },
                    { 438, null, null, 16, 39, "Trống" },
                    { 439, null, null, 17, 39, "Trống" },
                    { 440, null, null, 18, 39, "Trống" },
                    { 441, null, null, 19, 39, "Trống" },
                    { 442, null, null, 20, 39, "Trống" },
                    { 443, null, null, 21, 39, "Trống" },
                    { 444, null, null, 22, 39, "Trống" },
                    { 445, null, null, 23, 39, "Trống" },
                    { 446, null, null, 1, 40, "Trống" },
                    { 447, null, null, 2, 40, "Trống" },
                    { 448, null, null, 3, 40, "Trống" },
                    { 449, null, null, 4, 40, "Trống" },
                    { 450, null, null, 5, 40, "Trống" },
                    { 451, null, null, 6, 40, "Trống" },
                    { 452, null, null, 7, 40, "Trống" },
                    { 453, null, null, 8, 40, "Trống" },
                    { 454, null, null, 9, 40, "Trống" },
                    { 455, null, null, 10, 40, "Trống" },
                    { 456, null, null, 11, 40, "Trống" },
                    { 457, null, null, 12, 40, "Trống" },
                    { 458, null, null, 13, 40, "Trống" },
                    { 459, null, null, 14, 40, "Trống" },
                    { 460, null, null, 15, 40, "Trống" },
                    { 461, null, null, 1, 41, "Trống" },
                    { 462, null, null, 2, 41, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 463, null, null, 3, 41, "Trống" },
                    { 464, null, null, 4, 41, "Trống" },
                    { 465, null, null, 5, 41, "Trống" },
                    { 466, null, null, 6, 41, "Trống" },
                    { 467, null, null, 7, 41, "Trống" },
                    { 468, null, null, 8, 41, "Trống" },
                    { 469, null, null, 9, 41, "Trống" },
                    { 470, null, null, 10, 41, "Trống" },
                    { 471, null, null, 11, 41, "Trống" },
                    { 472, null, null, 12, 41, "Trống" },
                    { 473, null, null, 13, 41, "Trống" },
                    { 474, null, null, 14, 41, "Trống" },
                    { 475, null, null, 15, 41, "Trống" },
                    { 476, null, null, 16, 42, "Trống" },
                    { 477, null, null, 17, 42, "Trống" },
                    { 478, null, null, 18, 42, "Trống" },
                    { 479, null, null, 19, 42, "Trống" },
                    { 480, null, null, 20, 42, "Trống" },
                    { 481, null, null, 21, 42, "Trống" },
                    { 482, null, null, 22, 42, "Trống" },
                    { 483, null, null, 23, 42, "Trống" },
                    { 484, null, null, 16, 43, "Trống" },
                    { 485, null, null, 17, 43, "Trống" },
                    { 486, null, null, 18, 43, "Trống" },
                    { 487, null, null, 19, 43, "Trống" },
                    { 488, null, null, 20, 43, "Trống" },
                    { 489, null, null, 21, 43, "Trống" },
                    { 490, null, null, 22, 43, "Trống" },
                    { 491, null, null, 23, 43, "Trống" },
                    { 492, null, null, 1, 44, "Trống" },
                    { 493, null, null, 2, 44, "Trống" },
                    { 494, null, null, 3, 44, "Trống" },
                    { 495, null, null, 4, 44, "Trống" },
                    { 496, null, null, 5, 44, "Trống" },
                    { 497, null, null, 6, 44, "Trống" },
                    { 498, null, null, 7, 44, "Trống" },
                    { 499, null, null, 8, 44, "Trống" },
                    { 500, null, null, 9, 44, "Trống" },
                    { 501, null, null, 10, 44, "Trống" },
                    { 502, null, null, 11, 44, "Trống" },
                    { 503, null, null, 12, 44, "Trống" },
                    { 504, null, null, 13, 44, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 505, null, null, 14, 44, "Trống" },
                    { 506, null, null, 15, 44, "Trống" },
                    { 507, null, null, 1, 45, "Trống" },
                    { 508, null, null, 2, 45, "Trống" },
                    { 509, null, null, 3, 45, "Trống" },
                    { 510, null, null, 4, 45, "Trống" },
                    { 511, null, null, 5, 45, "Trống" },
                    { 512, null, null, 6, 45, "Trống" },
                    { 513, null, null, 7, 45, "Trống" },
                    { 514, null, null, 8, 45, "Trống" },
                    { 515, null, null, 9, 45, "Trống" },
                    { 516, null, null, 10, 45, "Trống" },
                    { 517, null, null, 11, 45, "Trống" },
                    { 518, null, null, 12, 45, "Trống" },
                    { 519, null, null, 13, 45, "Trống" },
                    { 520, null, null, 14, 45, "Trống" },
                    { 521, null, null, 15, 45, "Trống" },
                    { 522, null, null, 16, 46, "Trống" },
                    { 523, null, null, 17, 46, "Trống" },
                    { 524, null, null, 18, 46, "Trống" },
                    { 525, null, null, 19, 46, "Trống" },
                    { 526, null, null, 20, 46, "Trống" },
                    { 527, null, null, 21, 46, "Trống" },
                    { 528, null, null, 22, 46, "Trống" },
                    { 529, null, null, 23, 46, "Trống" },
                    { 530, null, null, 16, 47, "Trống" },
                    { 531, null, null, 17, 47, "Trống" },
                    { 532, null, null, 18, 47, "Trống" },
                    { 533, null, null, 19, 47, "Trống" },
                    { 534, null, null, 20, 47, "Trống" },
                    { 535, null, null, 21, 47, "Trống" },
                    { 536, null, null, 22, 47, "Trống" },
                    { 537, null, null, 23, 47, "Trống" },
                    { 538, null, null, 1, 48, "Trống" },
                    { 539, null, null, 2, 48, "Trống" },
                    { 540, null, null, 3, 48, "Trống" },
                    { 541, null, null, 4, 48, "Trống" },
                    { 542, null, null, 5, 48, "Trống" },
                    { 543, null, null, 6, 48, "Trống" },
                    { 544, null, null, 7, 48, "Trống" },
                    { 545, null, null, 8, 48, "Trống" },
                    { 546, null, null, 9, 48, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 547, null, null, 10, 48, "Trống" },
                    { 548, null, null, 11, 48, "Trống" },
                    { 549, null, null, 12, 48, "Trống" },
                    { 550, null, null, 13, 48, "Trống" },
                    { 551, null, null, 14, 48, "Trống" },
                    { 552, null, null, 15, 48, "Trống" },
                    { 553, null, null, 1, 49, "Trống" },
                    { 554, null, null, 2, 49, "Trống" },
                    { 555, null, null, 3, 49, "Trống" },
                    { 556, null, null, 4, 49, "Trống" },
                    { 557, null, null, 5, 49, "Trống" },
                    { 558, null, null, 6, 49, "Trống" },
                    { 559, null, null, 7, 49, "Trống" },
                    { 560, null, null, 8, 49, "Trống" },
                    { 561, null, null, 9, 49, "Trống" },
                    { 562, null, null, 10, 49, "Trống" },
                    { 563, null, null, 11, 49, "Trống" },
                    { 564, null, null, 12, 49, "Trống" },
                    { 565, null, null, 13, 49, "Trống" },
                    { 566, null, null, 14, 49, "Trống" },
                    { 567, null, null, 15, 49, "Trống" },
                    { 568, null, null, 16, 50, "Trống" },
                    { 569, null, null, 17, 50, "Trống" },
                    { 570, null, null, 18, 50, "Trống" },
                    { 571, null, null, 19, 50, "Trống" },
                    { 572, null, null, 20, 50, "Trống" },
                    { 573, null, null, 21, 50, "Trống" },
                    { 574, null, null, 22, 50, "Trống" },
                    { 575, null, null, 23, 50, "Trống" },
                    { 576, null, null, 16, 51, "Trống" },
                    { 577, null, null, 17, 51, "Trống" },
                    { 578, null, null, 18, 51, "Trống" },
                    { 579, null, null, 19, 51, "Trống" },
                    { 580, null, null, 20, 51, "Trống" },
                    { 581, null, null, 21, 51, "Trống" },
                    { 582, null, null, 22, 51, "Trống" },
                    { 583, null, null, 23, 51, "Trống" },
                    { 584, null, null, 1, 52, "Trống" },
                    { 585, null, null, 2, 52, "Trống" },
                    { 586, null, null, 3, 52, "Trống" },
                    { 587, null, null, 4, 52, "Trống" },
                    { 588, null, null, 5, 52, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 589, null, null, 6, 52, "Trống" },
                    { 590, null, null, 7, 52, "Trống" },
                    { 591, null, null, 8, 52, "Trống" },
                    { 592, null, null, 9, 52, "Trống" },
                    { 593, null, null, 10, 52, "Trống" },
                    { 594, null, null, 11, 52, "Trống" },
                    { 595, null, null, 12, 52, "Trống" },
                    { 596, null, null, 13, 52, "Trống" },
                    { 597, null, null, 14, 52, "Trống" },
                    { 598, null, null, 15, 52, "Trống" },
                    { 599, null, null, 1, 53, "Trống" },
                    { 600, null, null, 2, 53, "Trống" },
                    { 601, null, null, 3, 53, "Trống" },
                    { 602, null, null, 4, 53, "Trống" },
                    { 603, null, null, 5, 53, "Trống" },
                    { 604, null, null, 6, 53, "Trống" },
                    { 605, null, null, 7, 53, "Trống" },
                    { 606, null, null, 8, 53, "Trống" },
                    { 607, null, null, 9, 53, "Trống" },
                    { 608, null, null, 10, 53, "Trống" },
                    { 609, null, null, 11, 53, "Trống" },
                    { 610, null, null, 12, 53, "Trống" },
                    { 611, null, null, 13, 53, "Trống" },
                    { 612, null, null, 14, 53, "Trống" },
                    { 613, null, null, 15, 53, "Trống" },
                    { 614, null, null, 16, 54, "Trống" },
                    { 615, null, null, 17, 54, "Trống" },
                    { 616, null, null, 18, 54, "Trống" },
                    { 617, null, null, 19, 54, "Trống" },
                    { 618, null, null, 20, 54, "Trống" },
                    { 619, null, null, 21, 54, "Trống" },
                    { 620, null, null, 22, 54, "Trống" },
                    { 621, null, null, 23, 54, "Trống" },
                    { 622, null, null, 16, 55, "Trống" },
                    { 623, null, null, 17, 55, "Trống" },
                    { 624, null, null, 18, 55, "Trống" },
                    { 625, null, null, 19, 55, "Trống" },
                    { 626, null, null, 20, 55, "Trống" },
                    { 627, null, null, 21, 55, "Trống" },
                    { 628, null, null, 22, 55, "Trống" },
                    { 629, null, null, 23, 55, "Trống" },
                    { 630, null, null, 1, 56, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
                    { 631, null, null, 2, 56, "Trống" },
                    { 632, null, null, 3, 56, "Trống" },
                    { 633, null, null, 4, 56, "Trống" },
                    { 634, null, null, 5, 56, "Trống" },
                    { 635, null, null, 6, 56, "Trống" },
                    { 636, null, null, 7, 56, "Trống" },
                    { 637, null, null, 8, 56, "Trống" },
                    { 638, null, null, 9, 56, "Trống" },
                    { 639, null, null, 10, 56, "Trống" },
                    { 640, null, null, 11, 56, "Trống" },
                    { 641, null, null, 12, 56, "Trống" },
                    { 642, null, null, 13, 56, "Trống" },
                    { 643, null, null, 14, 56, "Trống" },
                    { 644, null, null, 15, 56, "Trống" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 613);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 614);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 615);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 616);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 617);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 618);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 619);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 620);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 621);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 622);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 623);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 624);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 625);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 626);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 627);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 628);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 629);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 630);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 631);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 632);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 633);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 634);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 635);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 636);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 637);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 638);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 639);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 640);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 641);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 642);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 643);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 644);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 56);

            migrationBuilder.DropColumn(
                name: "HeldBySessionId",
                table: "ShowtimeSeats");
        }
    }
}

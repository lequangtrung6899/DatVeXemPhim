using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    public partial class themsuatchieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "ShowtimeId", "EndTime", "MovieId", "RoomId", "StartTime", "Status", "TicketPrice" },
                values: new object[,]
                {
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
                    { 100068, new DateTime(2026, 8, 9, 15, 45, 0, 0, DateTimeKind.Unspecified), 18, 2, new DateTime(2026, 8, 9, 14, 11, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100069, new DateTime(2026, 8, 9, 17, 28, 0, 0, DateTimeKind.Unspecified), 21, 1, new DateTime(2026, 8, 9, 15, 34, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100070, new DateTime(2026, 8, 9, 17, 41, 0, 0, DateTimeKind.Unspecified), 25, 2, new DateTime(2026, 8, 9, 16, 5, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100071, new DateTime(2026, 8, 9, 20, 34, 0, 0, DateTimeKind.Unspecified), 26, 1, new DateTime(2026, 8, 9, 17, 48, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100072, new DateTime(2026, 8, 9, 20, 0, 0, 0, DateTimeKind.Unspecified), 28, 2, new DateTime(2026, 8, 9, 18, 1, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100073, new DateTime(2026, 8, 9, 23, 7, 0, 0, DateTimeKind.Unspecified), 29, 1, new DateTime(2026, 8, 9, 20, 54, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m },
                    { 100074, new DateTime(2026, 8, 9, 22, 41, 0, 0, DateTimeKind.Unspecified), 30, 2, new DateTime(2026, 8, 9, 20, 20, 0, 0, DateTimeKind.Unspecified), "Đang chiếu", 75000m }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
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
                    { 200672, null, null, 5, 100059, "Trống" },
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
                    { 200686, null, null, 19, 100060, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
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
                    { 200714, null, null, 1, 100063, "Trống" },
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
                    { 200728, null, null, 15, 100063, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
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
                    { 200756, null, null, 20, 100066, "Trống" },
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
                    { 200770, null, null, 11, 100067, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
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
                    { 200798, null, null, 16, 100070, "Trống" },
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
                    { 200812, null, null, 7, 100071, "Trống" }
                });

            migrationBuilder.InsertData(
                table: "ShowtimeSeats",
                columns: new[] { "ShowtimeSeatId", "HeldBySessionId", "HoldExpiredAt", "SeatId", "ShowtimeId", "Status" },
                values: new object[,]
                {
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
                    { 200840, null, null, 12, 100073, "Trống" },
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200645);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200646);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200647);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200648);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200649);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200650);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200651);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200652);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200653);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200654);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200655);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200656);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200657);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200658);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200659);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200660);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200661);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200662);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200663);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200664);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200665);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200666);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200667);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200668);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200669);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200670);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200671);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200672);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200673);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200674);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200675);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200676);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200677);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200678);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200679);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200680);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200681);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200682);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200683);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200684);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200685);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200686);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200687);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200688);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200689);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200690);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200691);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200692);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200693);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200694);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200695);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200696);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200697);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200698);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200699);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200700);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200701);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200702);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200703);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200704);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200705);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200706);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200707);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200708);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200709);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200710);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200711);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200712);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200713);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200714);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200715);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200716);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200717);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200718);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200719);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200720);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200721);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200722);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200723);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200724);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200725);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200726);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200727);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200728);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200729);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200730);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200731);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200732);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200733);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200734);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200735);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200736);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200737);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200738);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200739);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200740);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200741);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200742);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200743);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200744);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200745);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200746);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200747);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200748);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200749);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200750);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200751);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200752);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200753);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200754);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200755);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200756);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200757);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200758);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200759);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200760);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200761);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200762);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200763);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200764);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200765);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200766);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200767);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200768);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200769);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200770);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200771);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200772);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200773);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200774);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200775);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200776);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200777);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200778);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200779);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200780);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200781);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200782);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200783);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200784);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200785);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200786);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200787);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200788);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200789);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200790);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200791);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200792);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200793);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200794);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200795);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200796);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200797);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200798);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200799);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200800);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200801);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200802);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200803);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200804);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200805);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200806);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200807);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200808);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200809);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200810);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200811);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200812);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200813);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200814);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200815);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200816);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200817);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200818);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200819);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200820);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200821);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200822);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200823);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200824);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200825);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200826);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200827);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200828);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200829);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200830);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200831);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200832);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200833);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200834);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200835);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200836);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200837);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200838);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200839);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200840);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200841);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200842);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200843);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200844);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200845);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200846);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200847);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200848);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200849);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200850);

            migrationBuilder.DeleteData(
                table: "ShowtimeSeats",
                keyColumn: "ShowtimeSeatId",
                keyValue: 200851);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100057);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100058);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100059);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100060);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100061);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100062);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100063);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100064);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100065);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100066);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100067);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100068);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100069);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100070);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100071);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100072);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100073);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "ShowtimeId",
                keyValue: 100074);
        }
    }
}

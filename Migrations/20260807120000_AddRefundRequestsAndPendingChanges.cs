using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    public partial class AddRefundRequestsAndPendingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Giỏ hàng, thanh toán QR ngân hàng và voucher đều tái sử dụng các bảng
            // Tickets / Payments / Vouchers sẵn có (chỉ khác ở luồng xử lý trong code,
            // không cần đổi schema). Riêng luồng hoàn tiền cần 2 cấp duyệt (Nhân viên
            // rồi Admin) nên cần thêm bảng RefundRequests mới.
            migrationBuilder.CreateTable(
                name: "RefundRequests",
                columns: table => new
                {
                    RefundRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminApprovedBy = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedBy = table.Column<int>(type: "int", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StaffApprovedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
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

            // Hàng đợi "chờ Admin duyệt" dùng chung cho các trang quản lý mà Nhân viên
            // thao tác (Combo, Thể loại, Voucher, Phòng chiếu, Suất chiếu, Thanh toán,
            // Khách hàng, Hủy vé hỗ trợ) — xem Models/PendingChange.cs.
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
                        name: "FK_PendingChanges_Users_SubmittedBy",
                        column: x => x.SubmittedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingChanges_Users_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_PendingChanges_SubmittedBy",
                table: "PendingChanges",
                column: "SubmittedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PendingChanges_ReviewedBy",
                table: "PendingChanges",
                column: "ReviewedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefundRequests");

            migrationBuilder.DropTable(
                name: "PendingChanges");
        }
    }
}

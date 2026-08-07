using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using DatVeXemPhim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Duyệt hoàn tiền": nơi Nhân viên và Admin xét duyệt các yêu cầu hoàn
// tiền do khách hàng gửi (từ trang "Vé của tôi") hoặc do Nhân viên khởi tạo giúp
// khách (từ trang "Hỗ trợ khách hàng"). Tiền CHỈ thực sự được hoàn sau khi đủ 2
// cấp duyệt: Nhân viên duyệt trước, Admin duyệt lần cuối (xem Services/RefundService.cs).
public class AdminRefundController : AdminBaseController
{
    private readonly RefundService _refundService;
    private const int PageSize = 8;

    public AdminRefundController(ApplicationDbContext db, RefundService refundService) : base(db)
    {
        _refundService = refundService;
    }

    // GET /quan-tri/hoan-tien?status=...
    [HttpGet, Route("/quan-tri/hoan-tien")]
    public async Task<IActionResult> Index(string? status, int page = 1)
    {
        var staff = await GetCurrentStaffAsync();
        var isAdmin = staff?.Role?.RoleName == "Admin";

        var query = Db.RefundRequests
            .Include(r => r.Ticket).ThenInclude(t => t.Customer)
            .Include(r => r.Ticket).ThenInclude(t => t.Showtime).ThenInclude(s => s.Movie)
            .Include(r => r.StaffApprover)
            .Include(r => r.AdminApprover)
            .AsQueryable();

        var effectiveStatus = string.IsNullOrWhiteSpace(status) ? "Chờ nhân viên duyệt" : status;
        if (effectiveStatus != "Tất cả")
        {
            query = query.Where(r => r.Status == effectiveStatus);
        }

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var rows = await query.OrderByDescending(r => r.RequestedAt)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .Select(r => new AdminRefundRow
            {
                RefundRequestId = r.RefundRequestId,
                TicketId = r.TicketId,
                CustomerName = r.Ticket.Customer.FullName,
                MovieTitle = r.Ticket.Showtime.Movie.Title,
                Amount = r.Amount,
                Reason = r.Reason,
                RequestedAt = r.RequestedAt,
                Status = r.Status,
                StaffApproverName = r.StaffApprover != null ? r.StaffApprover.FullName : null,
                StaffApprovedAt = r.StaffApprovedAt,
                AdminApproverName = r.AdminApprover != null ? r.AdminApprover.FullName : null,
                AdminApprovedAt = r.AdminApprovedAt,
                RejectReason = r.RejectReason
            })
            .ToListAsync();

        foreach (var row in rows)
        {
            row.CanAct = row.Status == "Chờ nhân viên duyệt" || (row.Status == "Chờ admin duyệt" && isAdmin);
        }

        // Số yêu cầu đang chờ Nhân viên duyệt — hiển thị badge nhắc trên sidebar.
        ViewBag.PendingStaffCount = await Db.RefundRequests.CountAsync(r => r.Status == "Chờ nhân viên duyệt");
        ViewBag.PendingAdminCount = await Db.RefundRequests.CountAsync(r => r.Status == "Chờ admin duyệt");
        ViewBag.Status = effectiveStatus;
        ViewBag.IsAdmin = isAdmin;
        ViewBag.Pagination = new PaginationVM
        {
            Page = page,
            TotalPages = totalPages,
            BaseUrl = "/quan-tri/hoan-tien?status=" + Uri.EscapeDataString(effectiveStatus) + "&"
        };
        return View(rows);
    }

    // POST /quan-tri/hoan-tien/{id}/duyet
    [HttpPost, Route("/quan-tri/hoan-tien/{id:int}/duyet")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id, string? status)
    {
        var staff = await GetCurrentStaffAsync();
        if (staff is null) return Redirect("/quan-tri/dang-nhap");

        var (ok, message) = await _refundService.ApproveAsync(id, staff);
        TempData[ok ? "Success" : "Error"] = message;
        return Redirect("/quan-tri/hoan-tien?status=" + Uri.EscapeDataString(status ?? "Chờ nhân viên duyệt"));
    }

    // POST /quan-tri/hoan-tien/{id}/tu-choi
    [HttpPost, Route("/quan-tri/hoan-tien/{id:int}/tu-choi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int id, string? reason, string? status)
    {
        var staff = await GetCurrentStaffAsync();
        if (staff is null) return Redirect("/quan-tri/dang-nhap");

        var (ok, message) = await _refundService.RejectAsync(id, staff, reason);
        TempData[ok ? "Success" : "Error"] = message;
        return Redirect("/quan-tri/hoan-tien?status=" + Uri.EscapeDataString(status ?? "Chờ nhân viên duyệt"));
    }
}

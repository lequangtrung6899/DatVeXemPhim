using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Base controller for the staff/admin area ("Quản lý ...", "Kiểm tra ...", "Thống kê báo cáo", ...).
// Mirrors BaseController but authenticates against the Users/Roles tables instead of Customers,
// since staff and customers are distinct actors in the system (see report section 2.6).
public abstract class AdminBaseController : Controller
{
    protected readonly ApplicationDbContext Db;
    private const string SessionKey = "StaffId";

    // Roles allowed to sign in to the admin area at all. Individual actions can further
    // restrict to "Admin" only via RequireAdminAsync().
    protected AdminBaseController(ApplicationDbContext db)
    {
        Db = db;
    }

    protected async Task<User?> GetCurrentStaffAsync()
    {
        var id = HttpContext.Session.GetInt32(SessionKey);
        if (id is null) return null;
        return await Db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id && u.IsActive);
    }

    protected void SignIn(int userId)
    {
        HttpContext.Session.SetInt32(SessionKey, userId);
    }

    protected void SignOutStaff()
    {
        HttpContext.Session.Remove(SessionKey);
    }

    // Equivalent to BaseController's FormatVND, duplicated here so the admin area
    // doesn't need to depend on the customer-facing controller hierarchy.
    protected static string FormatVND(decimal n)
    {
        return Math.Round(n).ToString("N0", new System.Globalization.CultureInfo("vi-VN")) + "\u20ab";
    }

    public override async Task OnActionExecutionAsync(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context, Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate next)
    {
        var staff = await GetCurrentStaffAsync();

        // AdminAccountController itself must remain reachable while signed out.
        var isAccountController = context.Controller is AdminAccountController;

        if (staff is null && !isAccountController)
        {
            context.Result = Redirect("/quan-tri/dang-nhap?next=" + Uri.EscapeDataString(Request.Path + Request.QueryString));
            return;
        }

        ViewBag.Staff = staff;
        ViewBag.AdminPath = Request.Path.Value ?? "/";
        ViewBag.FormatVND = (Func<decimal, string>)FormatVND;

        // Hiển thị số phim đang "Chờ duyệt" trên sidebar (chỉ Admin mới thấy) để nhắc
        // Admin xử lý các thay đổi do Nhân viên gửi lên — hỗ trợ luồng duyệt ở "Quản lý phim".
        if (staff != null && staff.Role.RoleName == "Admin")
        {
            ViewBag.PendingMovieCount = await Db.Movies.CountAsync(m => m.ApprovalStatus == "Chờ duyệt" || m.HasPendingEdit);

            // Số đề xuất (thêm/sửa/xóa) từ các trang quản lý khác (Combo, Thể loại, Voucher,
            // Phòng chiếu, Suất chiếu, Thanh toán, Khách hàng, Hủy vé hỗ trợ) đang chờ Admin
            // duyệt — xem AdminApprovalController và Models/PendingChange.cs.
            ViewBag.PendingApprovalCount = await Db.PendingChanges.CountAsync(pc => pc.Status == "Chờ duyệt");
        }

        // Số yêu cầu hoàn tiền đang chờ tài khoản hiện tại xử lý — nhắc nhở trên sidebar
        // (Staff chỉ thấy các yêu cầu "Chờ nhân viên duyệt"; Admin thấy cả 2 bước).
        if (staff != null)
        {
            ViewBag.PendingRefundCount = staff.Role.RoleName == "Admin"
                ? await Db.RefundRequests.CountAsync(r => r.Status == "Chờ nhân viên duyệt" || r.Status == "Chờ admin duyệt")
                : await Db.RefundRequests.CountAsync(r => r.Status == "Chờ nhân viên duyệt");
        }

        await next();
    }

    // Some actions (role management, staff management) are restricted to the "Admin" role;
    // "Staff" role can use booking-support / catalog screens but not manage other accounts.
    protected async Task<bool> IsAdminRoleAsync()
    {
        var staff = await GetCurrentStaffAsync();
        return staff != null && staff.Role.RoleName == "Admin";
    }

    // Ghi 1 đề xuất chờ Admin duyệt thay vì áp dụng thay đổi ngay — dùng ở mọi trang quản
    // lý mà Nhân viên (không phải Admin) thao tác, để chống lạm quyền. `data` được JSON
    // hoá vào PendingChange.ChangesJson (truyền null cho các đề xuất Xóa/Hủy vì không có
    // field nào cần lưu). Xem AdminApprovalController để biết cách các đề xuất này được
    // áp dụng vào dữ liệu thật khi Admin bấm Duyệt.
    protected async Task SubmitPendingChangeAsync<T>(string entityType, int? entityId, string actionType, T? data, string summary)
    {
        var staff = await GetCurrentStaffAsync();
        Db.PendingChanges.Add(new PendingChange
        {
            EntityType = entityType,
            EntityId = entityId,
            ActionType = actionType,
            ChangesJson = data is null ? null : System.Text.Json.JsonSerializer.Serialize(data),
            Summary = summary,
            SubmittedBy = staff!.UserId,
            SubmittedAt = DateTime.Now,
            Status = "Chờ duyệt"
        });
        await Db.SaveChangesAsync();
    }
}

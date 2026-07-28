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
        await next();
    }

    // Some actions (role management, staff management) are restricted to the "Admin" role;
    // "Staff" role can use booking-support / catalog screens but not manage other accounts.
    protected async Task<bool> IsAdminRoleAsync()
    {
        var staff = await GetCurrentStaffAsync();
        return staff != null && staff.Role.RoleName == "Admin";
    }
}

using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public abstract class BaseController : Controller
{
    protected readonly ApplicationDbContext Db;
    private const string SessionKey = "CustomerId";

    protected BaseController(ApplicationDbContext db)
    {
        Db = db;
    }

    // Equivalent to the Express app's `currentCustomer(req)` helper.
    protected async Task<Customer?> GetCurrentCustomerAsync()
    {
        var id = HttpContext.Session.GetInt32(SessionKey);
        if (id is null) return null;
        return await Db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    protected void SignIn(int customerId)
    {
        HttpContext.Session.SetInt32(SessionKey, customerId);
    }

    protected void SignOutCustomer()
    {
        HttpContext.Session.Remove(SessionKey);
    }

    // ASP.NET Core Session.Id changes on every request until something is actually
    // written to the session at least once. Used to identify "who" is temporarily
    // holding a seat (Ca sử dụng "Chọn ghế") — works even before the customer logs in.
    protected string EnsureBrowserSessionId()
    {
        const string marker = "_sid";
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(marker)))
        {
            HttpContext.Session.SetString(marker, "1");
        }
        return HttpContext.Session.Id;
    }

    // Equivalent to the Express app's `formatVND(n)` helper.
    protected static string FormatVND(decimal n)
    {
        return Math.Round(n).ToString("N0", new System.Globalization.CultureInfo("vi-VN")) + "\u20ab";
    }

    public override async Task OnActionExecutionAsync(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context, Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate next)
    {
        // Mirrors the res.locals middleware in server.js: expose the current customer and
        // request path to every view via ViewBag.
        var customer = await GetCurrentCustomerAsync();
        ViewBag.Customer = customer;

        // Số món trong giỏ hàng (vé "Chờ thanh toán") — hiển thị badge trên icon giỏ hàng ở header.
        ViewBag.CartCount = customer != null
            ? await Db.Tickets.CountAsync(t => t.CustomerId == customer.CustomerId && t.Status == "Chờ thanh toán")
            : 0;

        ViewBag.Path = Request.Path.Value ?? "/";
        ViewBag.FormatVND = (Func<decimal, string>)FormatVND;
        await next();
    }
}

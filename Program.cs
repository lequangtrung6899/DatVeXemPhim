using DatVeXemPhim.Data;
using DatVeXemPhim.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----
builder.Services.AddControllersWithViews();

// Cho phép gửi antiforgery token qua header (dùng cho các lời gọi AJAX/JSON, ví dụ
// endpoint giữ ghế tạm thời trong BookingController.Hold), thay vì chỉ chấp nhận
// token trong form field __RequestVerificationToken như mặc định.
builder.Services.AddAntiforgery(options => options.HeaderName = "RequestVerificationToken");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.Name = "DatVeXemPhim.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// Powers the IMDb-style detail info on the movie detail page (director, cast,
// country, IMDb rating, ...) fetched live from the OMDb API by movie title.
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<OmdbService>();

// Dịch Plot/Genre/Awards từ OMDb (tiếng Anh) sang tiếng Việt qua MyMemory (miễn phí, không cần key).
builder.Services.AddHttpClient<TranslationService>();

// Xử lý luồng duyệt hoàn tiền 2 cấp (Nhân viên -> Admin) dùng chung giữa
// TicketController, AdminSupportController và AdminRefundController.
builder.Services.AddScoped<RefundService>();

var app = builder.Build();

// ---- Code First: apply any pending EF Core migrations automatically on startup ----
// The database no longer needs to be created manually from Database/DatVeXemPhim.sql;
// running the app (after `dotnet ef migrations add InitialCreate`, see README) is enough
// to create the schema and seed data from the model itself.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// ---- Middleware pipeline ----

// QUAN TRỌNG: ép văn hóa (culture) "en-US" cho MỌI request, bất kể máy chủ/máy dev
// đang đặt vùng miền gì (vd: Windows tiếng Việt = "vi-VN"). Nếu không có dòng này,
// khi máy chạy ở "vi-VN" (dùng dấu PHẨY làm dấu thập phân, dấu CHẤM làm dấu phân
// cách nghìn), ASP.NET Core sẽ hiểu sai các giá trị decimal gửi lên từ input số
// (input number của HTML luôn dùng dấu CHẤM làm dấu thập phân, không phụ thuộc
// ngôn ngữ) — vd chuỗi "2000000.00" bị đọc thành 200000000 (thêm 2 số 0, x100)
// thay vì đúng là 2000000.00. Đây là nguyên nhân gốc của lỗi "giá tiền tự nhân
// thêm số 0 mỗi lần lưu" ở Combo/Voucher/Suất chiếu.
var invariantCulture = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(invariantCulture),
    SupportedCultures = new[] { invariantCulture },
    SupportedUICultures = new[] { invariantCulture }
};
app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Renders Views/Shared/NotFound.cshtml (mirrors the Express app's 404.ejs) for any 404 response.
app.UseStatusCodePagesWithReExecute("/Home/NotFoundPage");

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// ---- Routes (mirrors the original Express/EJS URL structure) ----
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

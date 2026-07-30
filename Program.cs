using DatVeXemPhim.Data;
using DatVeXemPhim.Services;
using Microsoft.EntityFrameworkCore;

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

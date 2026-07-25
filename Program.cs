using DatVeXemPhim.Data;
using DatVeXemPhim.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----
builder.Services.AddControllersWithViews();

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

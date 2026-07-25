using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class HomeController : BaseController
{
    public HomeController(ApplicationDbContext db) : base(db) { }

    // GET /
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var nowShowing = await MovieQueryHelper.GetMoviesWithGenresAsync(
            Db, Db.Movies.Where(m => m.Status == "Đang chiếu").OrderByDescending(m => m.ReleaseDate));

        var comingSoon = await MovieQueryHelper.GetMoviesWithGenresAsync(
            Db, Db.Movies.Where(m => m.Status == "Sắp chiếu").OrderBy(m => m.ReleaseDate));

        var vm = new HomeVM
        {
            Title = "Trang chủ",
            NowShowing = nowShowing,
            ComingSoon = comingSoon,
            Hero = nowShowing.Take(5).ToList()
        };

        return View(vm);
    }

    [Route("/Home/Error")]
    public IActionResult Error() => View();

    // Rendered by UseStatusCodePagesWithReExecute for any 404 (mirrors 404.ejs).
    [Route("/Home/NotFoundPage")]
    public IActionResult NotFoundPage()
    {
        Response.StatusCode = 404;
        ViewData["Title"] = "Không tìm thấy trang";
        return View("NotFound");
    }
}

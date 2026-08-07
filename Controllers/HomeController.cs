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
        // Chỉ hiển thị phim đã được Admin duyệt cho khách hàng (phim do Nhân viên thêm/sửa
        // đang "Chờ duyệt" hoặc bị "Từ chối" sẽ không xuất hiện ở đây — xem AdminMovieController).
        var nowShowing = await MovieQueryHelper.GetMoviesWithGenresAsync(
            Db, Db.Movies.Where(m => m.Status == "Đang chiếu" && m.ApprovalStatus == "Đã duyệt")
                .OrderByDescending(m => m.ReleaseDate));

        var comingSoon = await MovieQueryHelper.GetMoviesWithGenresAsync(
            Db, Db.Movies.Where(m => m.Status == "Sắp chiếu" && m.ApprovalStatus == "Đã duyệt")
                .OrderBy(m => m.ReleaseDate));

        // Banner (hero) trang chủ: ưu tiên các phim được Admin/Nhân viên chọn thủ công
        // (Movie.ShowOnBanner) trong "Quản lý phim". Nếu chưa ai chọn phim nào, tự động
        // lấy tạm 5 phim đang chiếu mới nhất để banner không bị trống.
        var hero = nowShowing.Where(m => m.ShowOnBanner).Take(5).ToList();
        if (hero.Count == 0)
        {
            hero = nowShowing.Take(5).ToList();
        }

        var vm = new HomeVM
        {
            Title = "Trang chủ",
            NowShowing = nowShowing,
            ComingSoon = comingSoon,
            Hero = hero
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

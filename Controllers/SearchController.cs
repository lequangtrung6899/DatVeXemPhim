using DatVeXemPhim.Data;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

public class SearchController : BaseController
{
    public SearchController(ApplicationDbContext db) : base(db) { }

    // GET /tim-kiem?q=...
    [Route("/tim-kiem")]
    public async Task<IActionResult> Index(string? q)
    {
        var query = (q ?? string.Empty).Trim();
        var vm = new SearchVM { Q = query };

        if (!string.IsNullOrEmpty(query))
        {
            var movies = await Db.Movies
                .Where(m => EF.Functions.Like(m.Title, $"%{query}%"))
                .OrderByDescending(m => m.ReleaseDate)
                .ToListAsync();

            foreach (var m in movies)
                vm.Movies.Add(await MovieQueryHelper.AttachGenresAndRatingAsync(Db, m));
        }

        return View(vm);
    }
}

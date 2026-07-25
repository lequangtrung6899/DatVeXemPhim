using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers;

// Mirrors the Node app's movieWithGenres() helper: attaches genre names + review stats to a movie.
public static class MovieQueryHelper
{
    public static async Task<MovieCardVM> AttachGenresAndRatingAsync(ApplicationDbContext db, Movie movie)
    {
        var genres = await db.MovieGenres
            .Where(mg => mg.MovieId == movie.MovieId)
            .Select(mg => mg.Genre.GenreName)
            .ToListAsync();

        var approvedReviews = db.Reviews.Where(r => r.MovieId == movie.MovieId && r.Status == "Đã duyệt");
        var reviewCount = await approvedReviews.CountAsync();
        double? avg = reviewCount > 0
            ? Math.Round(await approvedReviews.AverageAsync(r => (double)r.Rating), 1)
            : null;

        return MovieCardVM.FromMovie(movie, genres, avg, reviewCount);
    }

    public static async Task<List<MovieCardVM>> GetMoviesWithGenresAsync(ApplicationDbContext db, IQueryable<Movie> query)
    {
        var movies = await query.ToListAsync();
        var result = new List<MovieCardVM>(movies.Count);
        foreach (var m in movies)
        {
            result.Add(await AttachGenresAndRatingAsync(db, m));
        }
        return result;
    }
}

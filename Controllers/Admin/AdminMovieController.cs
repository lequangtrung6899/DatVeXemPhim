using System.Text.Json;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DatVeXemPhim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers.Admin;

// Ca sử dụng "Quản lý phim".
//
// Quyền hạn & luồng duyệt (chống Nhân viên lạm quyền):
//   - Admin thao tác thì áp dụng ngay lập tức, không cần duyệt.
//   - Nhân viên THÊM phim mới: bản ghi tạo với ApprovalStatus = "Chờ duyệt", ẩn khỏi trang khách
//     (MoviesController/HomeController/SearchController chỉ hiển thị "Đã duyệt") cho tới khi Admin
//     Duyệt/Từ chối trong tab "Chờ duyệt" bên dưới.
//   - Nhân viên SỬA một phim ĐÃ được duyệt trước đó: KHÔNG ghi đè trực tiếp lên dữ liệu đang hiển
//     thị công khai (nếu không phim sẽ biến mất khỏi trang chủ ngay khi bấm Lưu, trước cả khi Admin
//     kịp xem). Thay vào đó đề xuất sửa được lưu tạm vào Movie.PendingChangesJson (HasPendingEdit =
//     true); phim vẫn hiển thị bình thường với dữ liệu CŨ. Khi Admin Duyệt → áp dụng thay đổi vào
//     dữ liệu thật. Khi Admin Từ chối → chỉ xóa đề xuất, dữ liệu cũ giữ nguyên, phim KHÔNG bị ẩn.
//   - Xóa phim chỉ Admin mới được phép (thao tác không thể hoàn tác).
public class AdminMovieController : AdminBaseController
{
    private readonly IWebHostEnvironment _env;

    public AdminMovieController(ApplicationDbContext db, IWebHostEnvironment env) : base(db)
    {
        _env = env;
    }

    private const int PageSize = 6;
    private static readonly string[] AllowedImageExt = { ".jpg", ".jpeg", ".png", ".webp" };
    private const long MaxImageBytes = 5 * 1024 * 1024; // 5MB

    [HttpGet, Route("/quan-tri/phim")]
    public async Task<IActionResult> Index(string? q, string? duyet, int page = 1)
    {
        IQueryable<Movie> query = Db.Movies.OrderByDescending(m => m.CreatedAt);
        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(m => EF.Functions.Like(m.Title, $"%{q}%"));
        }

        var effectiveDuyet = string.IsNullOrWhiteSpace(duyet) ? "Tất cả" : duyet;
        if (effectiveDuyet == "Chờ duyệt")
        {
            // "Chờ duyệt" gồm cả phim MỚI đang chờ duyệt lẫn phim CŨ đang có đề xuất sửa chờ duyệt.
            query = query.Where(m => m.ApprovalStatus == "Chờ duyệt" || m.HasPendingEdit);
        }
        else if (effectiveDuyet != "Tất cả")
        {
            query = query.Where(m => m.ApprovalStatus == effectiveDuyet);
        }

        var totalCount = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var movies = await query.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

        ViewBag.Q = q;
        ViewBag.Duyet = effectiveDuyet;
        ViewBag.PendingCount = await Db.Movies.CountAsync(m => m.ApprovalStatus == "Chờ duyệt" || m.HasPendingEdit);
        ViewBag.IsAdmin = await IsAdminRoleAsync();
        ViewBag.Pagination = new PaginationVM
        {
            Page = page,
            TotalPages = totalPages,
            BaseUrl = "/quan-tri/phim?" +
                (string.IsNullOrEmpty(q) ? "" : $"q={Uri.EscapeDataString(q)}&") +
                (effectiveDuyet == "Tất cả" ? "" : $"duyet={Uri.EscapeDataString(effectiveDuyet)}&")
        };
        return View(movies);
    }

    // GET /quan-tri/phim/goi-y?q=... — gợi ý (autocomplete) khi nhân viên gõ tìm phim trong
    // trang quản trị. Khác với /tim-kiem/goi-y dành cho khách: ở đây tìm trên TẤT CẢ phim,
    // kể cả phim đang "Chờ duyệt"/"Từ chối", vì nhân viên/Admin cần thấy cả các phim đó.
    [HttpGet, Route("/quan-tri/phim/goi-y")]
    public async Task<IActionResult> Suggest(string? q)
    {
        var query = (q ?? string.Empty).Trim();
        if (query.Length < 2) return Json(Array.Empty<object>());

        var results = await Db.Movies
            .Where(m => EF.Functions.Like(m.Title, $"%{query}%"))
            .OrderByDescending(m => m.CreatedAt)
            .Take(8)
            .Select(m => new
            {
                id = m.MovieId,
                title = m.Title,
                posterUrl = m.PosterUrl,
                status = m.Status,
                approvalStatus = m.ApprovalStatus
            })
            .ToListAsync();

        return Json(results);
    }

    [HttpGet, Route("/quan-tri/phim/them")]
    public async Task<IActionResult> Create()
    {
        var vm = new AdminMovieEditVM
        {
            Movie = new Movie(),
            AllGenres = await Db.Genres.OrderBy(g => g.GenreName).ToListAsync(),
            SelectedGenreIds = new List<int>()
        };
        ViewBag.IsAdmin = await IsAdminRoleAsync();
        return View("Edit", vm);
    }

    [HttpGet, Route("/quan-tri/phim/{id:int}/sua")]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await Db.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        var selectedGenreIds = await Db.MovieGenres.Where(mg => mg.MovieId == id).Select(mg => mg.GenreId).ToListAsync();

        var vm = new AdminMovieEditVM
        {
            Movie = movie,
            AllGenres = await Db.Genres.OrderBy(g => g.GenreName).ToListAsync(),
            SelectedGenreIds = selectedGenreIds
        };
        ViewBag.IsAdmin = await IsAdminRoleAsync();

        // Nếu phim đang có đề xuất sửa chờ duyệt, hiển thị thêm thông tin đề xuất đó (chỉ để xem,
        // form vẫn hiển thị/sửa trên dữ liệu ĐANG hiển thị công khai, không phải bản đang chờ duyệt).
        if (movie.HasPendingEdit && !string.IsNullOrEmpty(movie.PendingChangesJson))
        {
            try
            {
                ViewBag.PendingChange = JsonSerializer.Deserialize<MoviePendingChangeDto>(movie.PendingChangesJson);
            }
            catch { /* dữ liệu JSON hỏng (không nên xảy ra) — bỏ qua, không chặn trang sửa */ }

            if (movie.SubmittedBy.HasValue)
            {
                ViewBag.PendingSubmittedByName = (await Db.Users.FindAsync(movie.SubmittedBy.Value))?.FullName;
            }
        }

        return View(vm);
    }

    [HttpPost, Route("/quan-tri/phim/luu")]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(20_000_000)]
    public async Task<IActionResult> Save(
        int movieId, string title, string? description, int duration,
        string? posterUrl, string? bannerUrl, IFormFile? posterFile, IFormFile? bannerFile,
        DateTime releaseDate, DateTime? endDate, string status, bool showOnBanner,
        [FromForm(Name = "genreIds")] List<int>? genreIds)
    {
        genreIds ??= new List<int>();
        var staff = await GetCurrentStaffAsync();
        var isAdmin = staff != null && staff.Role.RoleName == "Admin";

        // ---- Upload ảnh (nếu có chọn file) — tự lưu vào wwwroot/posters hoặc wwwroot/banners ----
        string? uploadedPosterUrl;
        string? uploadedBannerUrl;
        try
        {
            uploadedPosterUrl = await SaveUploadedImageAsync(posterFile, "posters");
            uploadedBannerUrl = await SaveUploadedImageAsync(bannerFile, "banners");
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(movieId == 0 ? "/quan-tri/phim/them" : $"/quan-tri/phim/{movieId}/sua");
        }

        var finalPosterUrl = uploadedPosterUrl ?? posterUrl;
        var finalBannerUrl = uploadedBannerUrl ?? bannerUrl;

        // Nếu chọn hiển thị trên banner trang chủ, bắt buộc phải có ảnh banner (ảnh ngang) —
        // không cho "rơi" về dùng tạm Poster (ảnh dọc) vì hiển thị trong khung ngang sẽ rất xấu.
        if (showOnBanner && string.IsNullOrWhiteSpace(finalBannerUrl))
        {
            TempData["Error"] = "Đã chọn \"Hiển thị trên banner\" nhưng chưa có ảnh Banner — hãy tải lên ảnh Banner (ảnh ngang) trước khi lưu.";
            return Redirect(movieId == 0 ? "/quan-tri/phim/them" : $"/quan-tri/phim/{movieId}/sua");
        }

        var isNew = movieId == 0;
        genreIds = genreIds.Distinct().ToList();

        // ---- Trường hợp 1: Thêm phim mới ----
        // Luôn ghi trực tiếp (chưa từng hiển thị công khai nên không có gì để "làm mất").
        // Admin thì "Đã duyệt" ngay; Nhân viên thì "Chờ duyệt" và ẩn khỏi trang khách.
        if (isNew)
        {
            var movie = new Movie
            {
                CreatedAt = DateTime.Now,
                Title = title.Trim(),
                Description = description,
                Duration = duration,
                PosterUrl = finalPosterUrl,
                BannerUrl = finalBannerUrl,
                ReleaseDate = releaseDate,
                EndDate = endDate,
                Status = status,
                ShowOnBanner = showOnBanner
            };

            if (isAdmin)
            {
                movie.ApprovalStatus = "Đã duyệt";
                movie.ReviewedBy = staff!.UserId;
                movie.ReviewedAt = DateTime.Now;
            }
            else
            {
                movie.ApprovalStatus = "Chờ duyệt";
                movie.SubmittedBy = staff?.UserId;
            }

            Db.Movies.Add(movie);
            await Db.SaveChangesAsync(); // để có MovieId cho MovieGenres bên dưới

            foreach (var gId in genreIds)
            {
                Db.MovieGenres.Add(new MovieGenre { MovieId = movie.MovieId, GenreId = gId });
            }
            await Db.SaveChangesAsync();

            TempData["Success"] = isAdmin
                ? "Đã thêm phim mới."
                : "Đã gửi yêu cầu thêm phim mới — phim sẽ hiển thị cho khách hàng sau khi Admin duyệt.";
            return Redirect("/quan-tri/phim");
        }

        // ---- Trường hợp 2: Sửa phim đã tồn tại ----
        var existing = await Db.Movies.FindAsync(movieId);
        if (existing is null) return NotFound();

        var oldGenreIds = (await Db.MovieGenres.Where(mg => mg.MovieId == movieId)
            .Select(mg => mg.GenreId).ToListAsync()).OrderBy(x => x).ToList();
        var newGenreIdsSorted = genreIds.OrderBy(x => x).ToList();

        var noRealChange =
            existing.Title == title.Trim() &&
            (existing.Description ?? "") == (description ?? "") &&
            existing.Duration == duration &&
            (existing.PosterUrl ?? "") == (finalPosterUrl ?? "") &&
            (existing.BannerUrl ?? "") == (finalBannerUrl ?? "") &&
            existing.ReleaseDate == releaseDate &&
            existing.EndDate == endDate &&
            existing.Status == status &&
            existing.ShowOnBanner == showOnBanner &&
            oldGenreIds.SequenceEqual(newGenreIdsSorted);

        if (noRealChange)
        {
            TempData["Success"] = "Không có thay đổi nào so với dữ liệu hiện tại — không cần Admin duyệt lại.";
            return Redirect("/quan-tri/phim");
        }

        if (isAdmin)
        {
            // Admin sửa: áp dụng thẳng vào dữ liệu thật, hủy luôn mọi đề xuất sửa cũ của
            // Nhân viên đang chờ (nếu có) vì bản Admin vừa lưu mới là bản mới nhất/đáng tin nhất.
            existing.Title = title.Trim();
            existing.Description = description;
            existing.Duration = duration;
            existing.PosterUrl = finalPosterUrl;
            existing.BannerUrl = finalBannerUrl;
            existing.ReleaseDate = releaseDate;
            existing.EndDate = endDate;
            existing.Status = status;
            existing.ShowOnBanner = showOnBanner;
            existing.ApprovalStatus = "Đã duyệt";
            existing.ReviewedBy = staff!.UserId;
            existing.ReviewedAt = DateTime.Now;
            existing.HasPendingEdit = false;
            existing.PendingChangesJson = null;

            var currentLinks = await Db.MovieGenres.Where(mg => mg.MovieId == movieId).ToListAsync();
            Db.MovieGenres.RemoveRange(currentLinks.Where(l => !genreIds.Contains(l.GenreId)));
            foreach (var gId in genreIds.Except(currentLinks.Select(l => l.GenreId)))
            {
                Db.MovieGenres.Add(new MovieGenre { MovieId = movieId, GenreId = gId });
            }

            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật phim.";
        }
        else
        {
            // Nhân viên sửa phim đã duyệt: KHÔNG đụng vào dữ liệu đang hiển thị công khai.
            // Lưu đề xuất vào PendingChangesJson — phim tiếp tục hiển thị bình thường với dữ liệu
            // CŨ cho tới khi Admin Duyệt (áp dụng) hoặc Từ chối (hủy đề xuất, không mất gì).
            var dto = new MoviePendingChangeDto
            {
                Title = title.Trim(),
                Description = description,
                Duration = duration,
                PosterUrl = finalPosterUrl,
                BannerUrl = finalBannerUrl,
                ReleaseDate = releaseDate,
                EndDate = endDate,
                Status = status,
                ShowOnBanner = showOnBanner,
                GenreIds = genreIds
            };

            existing.HasPendingEdit = true;
            existing.PendingChangesJson = JsonSerializer.Serialize(dto);
            existing.SubmittedBy = staff?.UserId;

            await Db.SaveChangesAsync();
            TempData["Success"] = "Đã gửi đề xuất chỉnh sửa — phim vẫn hiển thị bình thường với dữ liệu hiện tại cho tới khi Admin duyệt.";
        }

        return Redirect("/quan-tri/phim");
    }

    // POST /quan-tri/phim/{id}/duyet — chỉ Admin.
    // Xử lý cả 2 loại: phim MỚI đang "Chờ duyệt", hoặc phim CŨ đang có đề xuất sửa (HasPendingEdit).
    [HttpPost, Route("/quan-tri/phim/{id:int}/duyet")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền duyệt phim.";
            return Redirect("/quan-tri/phim");
        }

        var staff = await GetCurrentStaffAsync();
        var movie = await Db.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        if (movie.HasPendingEdit && !string.IsNullOrEmpty(movie.PendingChangesJson))
        {
            // Áp dụng đề xuất sửa vào dữ liệu thật.
            var dto = JsonSerializer.Deserialize<MoviePendingChangeDto>(movie.PendingChangesJson);
            if (dto != null)
            {
                movie.Title = dto.Title;
                movie.Description = dto.Description;
                movie.Duration = dto.Duration;
                movie.PosterUrl = dto.PosterUrl;
                movie.BannerUrl = dto.BannerUrl;
                movie.ReleaseDate = dto.ReleaseDate;
                movie.EndDate = dto.EndDate;
                movie.Status = dto.Status;
                movie.ShowOnBanner = dto.ShowOnBanner;

                var currentLinks = await Db.MovieGenres.Where(mg => mg.MovieId == movie.MovieId).ToListAsync();
                Db.MovieGenres.RemoveRange(currentLinks.Where(l => !dto.GenreIds.Contains(l.GenreId)));
                foreach (var gId in dto.GenreIds.Except(currentLinks.Select(l => l.GenreId)))
                {
                    Db.MovieGenres.Add(new MovieGenre { MovieId = movie.MovieId, GenreId = gId });
                }
            }

            movie.HasPendingEdit = false;
            movie.PendingChangesJson = null;
        }

        // Với phim mới ("Chờ duyệt") thì đơn giản là chuyển trạng thái sang "Đã duyệt".
        movie.ApprovalStatus = "Đã duyệt";
        movie.ReviewedBy = staff?.UserId;
        movie.ReviewedAt = DateTime.Now;
        await Db.SaveChangesAsync();

        TempData["Success"] = $"Đã duyệt phim \"{movie.Title}\".";
        return Redirect("/quan-tri/phim?duyet=" + Uri.EscapeDataString("Chờ duyệt"));
    }

    // POST /quan-tri/phim/{id}/tu-choi — chỉ Admin.
    // - Phim MỚI đang "Chờ duyệt": chuyển sang "Từ chối" (tiếp tục ẩn khỏi trang khách).
    // - Phim CŨ có đề xuất sửa (HasPendingEdit): CHỈ hủy đề xuất, dữ liệu đang hiển thị công khai
    //   giữ nguyên hoàn toàn — phim KHÔNG biến mất khỏi trang chủ.
    [HttpPost, Route("/quan-tri/phim/{id:int}/tu-choi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int id)
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền từ chối phim.";
            return Redirect("/quan-tri/phim");
        }

        var staff = await GetCurrentStaffAsync();
        var movie = await Db.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        if (movie.HasPendingEdit)
        {
            movie.HasPendingEdit = false;
            movie.PendingChangesJson = null;
            movie.ReviewedBy = staff?.UserId;
            movie.ReviewedAt = DateTime.Now;
            await Db.SaveChangesAsync();

            TempData["Success"] = $"Đã từ chối đề xuất chỉnh sửa của phim \"{movie.Title}\" — dữ liệu đang hiển thị cho khách được giữ nguyên.";
        }
        else
        {
            movie.ApprovalStatus = "Từ chối";
            movie.ReviewedBy = staff?.UserId;
            movie.ReviewedAt = DateTime.Now;
            await Db.SaveChangesAsync();

            TempData["Success"] = $"Đã từ chối phim \"{movie.Title}\".";
        }

        return Redirect("/quan-tri/phim?duyet=" + Uri.EscapeDataString("Chờ duyệt"));
    }

    // Xóa phim là thao tác không thể hoàn tác — chỉ Admin mới được xóa (Nhân viên không có nút này).
    [HttpPost, Route("/quan-tri/phim/{id:int}/xoa")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await IsAdminRoleAsync())
        {
            TempData["Error"] = "Chỉ Quản trị viên mới có quyền xóa phim.";
            return Redirect("/quan-tri/phim");
        }

        var movie = await Db.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        var hasShowtimes = await Db.Showtimes.AnyAsync(s => s.MovieId == id);
        if (hasShowtimes)
        {
            TempData["Error"] = "Không thể xóa: phim đang có suất chiếu liên kết. Hãy chuyển trạng thái sang \"Ngừng chiếu\" thay vì xóa.";
            return Redirect("/quan-tri/phim");
        }

        var genreLinks = Db.MovieGenres.Where(mg => mg.MovieId == id);
        Db.MovieGenres.RemoveRange(genreLinks);
        Db.Movies.Remove(movie);
        await Db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa phim.";
        return Redirect("/quan-tri/phim");
    }

    // Lưu file ảnh upload vào wwwroot/{folder}/, trả về đường dẫn tương đối (vd "/posters/ten-file.jpg")
    // để gán vào PosterUrl/BannerUrl. Trả về null nếu người dùng không chọn file nào (giữ nguyên ảnh cũ).
    private async Task<string?> SaveUploadedImageAsync(IFormFile? file, string folder)
    {
        if (file is null || file.Length == 0) return null;

        if (file.Length > MaxImageBytes)
            throw new InvalidOperationException($"Ảnh \"{file.FileName}\" vượt quá dung lượng cho phép (tối đa 5MB).");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedImageExt.Contains(ext))
            throw new InvalidOperationException($"Ảnh \"{file.FileName}\" không đúng định dạng cho phép (chỉ .jpg, .jpeg, .png, .webp).");

        var targetDir = Path.Combine(_env.WebRootPath, folder);
        Directory.CreateDirectory(targetDir);

        var safeName = Slugify(Path.GetFileNameWithoutExtension(file.FileName));
        if (safeName.Length > 40) safeName = safeName.Substring(0, 40).Trim('-');
        var uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 8);
        var fileName = $"{safeName}-{uniqueSuffix}{ext}";
        var fullPath = Path.Combine(targetDir, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/{folder}/{fileName}";
    }

    // Chuyển tên file (kể cả tiếng Việt có dấu, vd "Mắt Biếc.jpg") thành slug ASCII an toàn
    // cho URL/hệ thống file, vd "mat-biec". Bỏ dấu bằng cách tách tổ hợp Unicode (NFD) rồi
    // loại các dấu kết hợp (combining marks), giống cách các trang tiếng Việt vẫn làm.
    private static string Slugify(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "anh";

        var normalized = input.Trim().ToLowerInvariant()
            .Normalize(System.Text.NormalizationForm.FormD);

        var sb = new System.Text.StringBuilder();
        foreach (var ch in normalized)
        {
            var category = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch);
            if (category == System.Globalization.UnicodeCategory.NonSpacingMark) continue;

            if (ch >= 'a' && ch <= 'z') sb.Append(ch);
            else if (ch >= '0' && ch <= '9') sb.Append(ch);
            else if (ch == 'đ') sb.Append('d');
            else if (ch == ' ' || ch == '-' || ch == '_') sb.Append('-');
        }

        var slug = sb.ToString().Normalize(System.Text.NormalizationForm.FormC).Trim('-');
        while (slug.Contains("--")) slug = slug.Replace("--", "-");
        return string.IsNullOrEmpty(slug) ? "anh" : slug;
    }
}

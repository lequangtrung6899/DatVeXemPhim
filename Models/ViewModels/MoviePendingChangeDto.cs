namespace DatVeXemPhim.Models.ViewModels;

// Gói dữ liệu đề xuất chỉnh sửa của Nhân viên trên một phim ĐÃ được duyệt trước đó.
// Được lưu tạm dưới dạng JSON vào Movie.PendingChangesJson (xem AdminMovieController) —
// KHÔNG áp dụng vào các cột thật của Movie cho tới khi Admin bấm "Duyệt".
public class MoviePendingChangeDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string? PosterUrl { get; set; }
    public string? BannerUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool ShowOnBanner { get; set; }
    public List<int> GenreIds { get; set; } = new();
}

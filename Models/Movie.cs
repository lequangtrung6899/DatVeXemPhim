using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models;

public class Movie
{
    public int MovieId { get; set; }

    [Required, MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Duration { get; set; }

    [MaxLength(500)]
    public string? PosterUrl { get; set; }

    // Ảnh ngang riêng cho banner trang chủ (khác tỉ lệ với PosterUrl - ảnh áp phích dọc).
    // Nếu để trống, banner sẽ dùng PosterUrl (nhưng ảnh dọc hiển thị trong khung ngang sẽ không đẹp).
    [MaxLength(500)]
    public string? BannerUrl { get; set; }

    public DateTime ReleaseDate { get; set; }

    public DateTime? EndDate { get; set; }

    // 'Đang chiếu' | 'Sắp chiếu' | 'Ngừng chiếu'
    [Required, MaxLength(50)]
    public string Status { get; set; } = "Sắp chiếu";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Có xuất hiện trên banner (hero carousel) trang chủ hay không — do Admin/Nhân viên
    // chọn thủ công trong màn "Quản lý phim". Nếu không có phim nào được chọn, trang chủ
    // sẽ tự động lấy tạm các phim đang chiếu mới nhất (xem HomeController).
    public bool ShowOnBanner { get; set; } = false;

    // ---- Duyệt nội dung (chống nhân viên lạm quyền) ----
    // 'Chờ duyệt' | 'Đã duyệt' | 'Từ chối'. Khi Nhân viên (không phải Admin) thêm/sửa phim,
    // bản ghi sẽ chuyển về 'Chờ duyệt' và bị ẩn khỏi trang khách hàng cho tới khi Admin duyệt.
    // Admin thao tác thì luôn tự động 'Đã duyệt'.
    [Required, MaxLength(20)]
    public string ApprovalStatus { get; set; } = "Đã duyệt";

    // UserId của nhân viên đã gửi lần thêm/sửa gần nhất đang chờ duyệt (null nếu do Admin làm).
    public int? SubmittedBy { get; set; }

    // UserId của Admin đã duyệt/từ chối gần nhất, và thời điểm duyệt.
    public int? ReviewedBy { get; set; }
    public DateTime? ReviewedAt { get; set; }

    // ---- Chỉnh sửa đang chờ duyệt trên MỘT phim ĐÃ được duyệt trước đó ----
    // Khác với phim MỚI (dùng ApprovalStatus ở trên): khi Nhân viên sửa một phim đang chiếu/đang
    // hiển thị công khai, KHÔNG được ghi đè trực tiếp lên dữ liệu đang hiển thị cho khách (nếu
    // không phim sẽ biến mất khỏi trang chủ ngay khi Nhân viên bấm Lưu, trước cả khi Admin xem
    // xét). Thay vào đó, đề xuất sửa được lưu tạm ở đây (JSON), phim vẫn hiển thị bình thường với
    // dữ liệu CŨ cho tới khi Admin bấm Duyệt (áp dụng thay đổi) hoặc Từ chối (hủy đề xuất, dữ liệu
    // cũ giữ nguyên, phim KHÔNG bị ẩn/mất khỏi trang chủ).
    public bool HasPendingEdit { get; set; } = false;
    public string? PendingChangesJson { get; set; }

    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

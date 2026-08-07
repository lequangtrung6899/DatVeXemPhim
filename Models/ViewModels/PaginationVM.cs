namespace DatVeXemPhim.Models.ViewModels;

// Dùng chung cho phần phân trang ở các trang danh sách trong khu vực quản trị.
// BaseUrl phải kết thúc bằng "?" hoặc "&" — partial view sẽ nối thêm "page=N" vào sau.
public class PaginationVM
{
    public int Page { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public string BaseUrl { get; set; } = "?";
}

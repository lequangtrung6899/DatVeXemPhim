namespace DatVeXemPhim.Models.ViewModels;

public class SearchVM
{
    public string Title { get; set; } = "Tìm kiếm";
    public string Q { get; set; } = string.Empty;
    public List<MovieCardVM> Movies { get; set; } = new();

    // Gợi ý hiển thị khi khách chưa nhập từ khóa, hoặc không tìm thấy phim nào phù hợp
    // (danh sách phim đang chiếu được quan tâm nhiều, để khách vẫn có lựa chọn thay vì màn hình trống).
    public List<MovieCardVM> SuggestedMovies { get; set; } = new();
    public List<string> PopularKeywords { get; set; } = new();
}

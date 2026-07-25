namespace DatVeXemPhim.Models.ViewModels;

public class HomeVM
{
    public string Title { get; set; } = "Trang chủ";
    public List<MovieCardVM> Hero { get; set; } = new();
    public List<MovieCardVM> NowShowing { get; set; } = new();
    public List<MovieCardVM> ComingSoon { get; set; } = new();
}

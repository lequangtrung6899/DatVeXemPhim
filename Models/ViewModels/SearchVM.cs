namespace DatVeXemPhim.Models.ViewModels;

public class SearchVM
{
    public string Title { get; set; } = "Tìm kiếm";
    public string Q { get; set; } = string.Empty;
    public List<MovieCardVM> Movies { get; set; } = new();
}

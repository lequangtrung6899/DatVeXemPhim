namespace DatVeXemPhim.Models.ViewModels;

public class LoginVM
{
    public string Title { get; set; } = "Đăng nhập";
    public string? Error { get; set; }
    public string Next { get; set; } = "/";
}

public class RegisterVM
{
    public string Title { get; set; } = "Đăng ký";
    public string? Error { get; set; }
}

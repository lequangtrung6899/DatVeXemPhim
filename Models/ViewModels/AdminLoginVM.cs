using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models.ViewModels;

public class AdminLoginVM
{
    public string? Error { get; set; }
    public string Next { get; set; } = "/quan-tri";

    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    public string Password { get; set; } = string.Empty;
}

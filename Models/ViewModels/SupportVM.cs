using System.ComponentModel.DataAnnotations;

namespace DatVeXemPhim.Models.ViewModels;

public class ContactFormVM
{
    public string Title { get; set; } = "Liên hệ";

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(150, ErrorMessage = "Họ tên tối đa 150 ký tự.")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(255, ErrorMessage = "Email tối đa 255 ký tự.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "Số điện thoại không đúng định dạng.")]
    [StringLength(20, ErrorMessage = "Số điện thoại tối đa 20 ký tự.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập chủ đề.")]
    [StringLength(200, ErrorMessage = "Chủ đề tối đa 200 ký tự.")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
    [StringLength(2000, ErrorMessage = "Nội dung tối đa 2000 ký tự.")]
    public string? Message { get; set; }

    public string? Error { get; set; }
    public bool Sent { get; set; }
}

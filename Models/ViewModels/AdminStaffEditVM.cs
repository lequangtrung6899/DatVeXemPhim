namespace DatVeXemPhim.Models.ViewModels;

public class AdminStaffEditVM
{
    public User User { get; set; } = null!;
    public List<Role> Roles { get; set; } = new();
}

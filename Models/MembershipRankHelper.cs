namespace DatVeXemPhim.Models;

// Ca sử dụng "Đặt vé": sau khi khách tích thêm điểm, hạng thành viên được tính lại
// tương ứng — dùng ngưỡng điểm cố định, đơn giản, dễ giải thích trong báo cáo.
public static class MembershipRankHelper
{
    public const string ThanhVienMoi = "Thành viên mới";
    public const string Bac = "Thành viên Bạc";
    public const string Vang = "Thành viên Vàng";
    public const string KimCuong = "Thành viên Kim Cương";

    public static void RecalculateRank(Customer customer)
    {
        customer.MembershipRank = customer.LoyaltyPoint switch
        {
            >= 5000 => KimCuong,
            >= 2000 => Vang,
            >= 500 => Bac,
            _ => ThanhVienMoi
        };
    }
}

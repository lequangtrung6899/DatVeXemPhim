using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models;

public class Voucher
{
    public int VoucherId { get; set; }

    [Required, MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    // 'Phần trăm' | 'Tiền mặt'
    [Required, MaxLength(20)]
    public string DiscountType { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountValue { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal MinOrderAmount { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int UsageLimit { get; set; }
    public int UsedCount { get; set; }

    public bool IsActive { get; set; } = true;
}

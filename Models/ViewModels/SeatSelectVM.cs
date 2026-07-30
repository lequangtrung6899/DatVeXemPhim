using DatVeXemPhim.Models;

namespace DatVeXemPhim.Models.ViewModels;

public class SeatCell
{
    public int ShowtimeSeatId { get; set; }
    public string RowLabel { get; set; } = string.Empty;
    public int ColumnNumber { get; set; }
    public string SeatType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // 'Trống' | 'Đang giữ' | 'Đã đặt'

    // Ghế "Đang giữ" nhưng do chính người xem trang này giữ (vẫn chọn được, không phải màu xám).
    public bool IsHeldByMe { get; set; }
    public int? HoldSecondsLeft { get; set; }
}

public class SeatSelectVM
{
    public string Title { get; set; } = string.Empty;
    public int ShowtimeId { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }

    public int MovieId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;

    // rows keyed by RowLabel, ordered
    public SortedDictionary<string, List<SeatCell>> Rows { get; set; } = new();

    public Dictionary<string, decimal> PriceByType { get; set; } = new();
    public List<Combo> Combos { get; set; } = new();

    public bool IsLoggedIn { get; set; }
    public int HoldMinutes { get; set; }
    public string? ErrorMessage { get; set; }
}

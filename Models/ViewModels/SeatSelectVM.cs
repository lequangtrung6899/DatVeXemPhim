using DatVeXemPhim.Models;

namespace DatVeXemPhim.Models.ViewModels;

public class SeatCell
{
    public int ShowtimeSeatId { get; set; }
    public string RowLabel { get; set; } = string.Empty;
    public int ColumnNumber { get; set; }
    public string SeatType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // 'Trống' | 'Đang giữ' | 'Đã đặt'
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
    public string? ErrorMessage { get; set; }
}

using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace DatVeXemPhim.Services;

// Dịch các đoạn text tiếng Anh lấy từ OMDb (Plot, Genre, Awards...) sang tiếng Việt.
// Dùng MyMemory Translation API (https://mymemory.translated.net/) — miễn phí hoàn
// toàn, KHÔNG cần đăng ký hay API key, phù hợp cho đồ án/demo.
// Luôn cache 7 ngày và không bao giờ ném lỗi ra ngoài — dịch thất bại thì trả về
// nguyên văn tiếng Anh, không làm hỏng trang.
public class TranslationService
{
    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;
    private readonly ILogger<TranslationService> _logger;

    public TranslationService(HttpClient http, IMemoryCache cache, ILogger<TranslationService> logger)
    {
        _http = http;
        _cache = cache;
        _logger = logger;
    }

    private class MyMemoryResponse
    {
        [JsonPropertyName("responseData")] public MyMemoryData? ResponseData { get; set; }
        [JsonPropertyName("responseStatus")] public int ResponseStatus { get; set; }
    }

    private class MyMemoryData
    {
        [JsonPropertyName("translatedText")] public string? TranslatedText { get; set; }
    }

    // Trả về bản dịch tiếng Việt, hoặc nguyên văn gốc nếu dịch lỗi/rỗng.
    public async Task<string> ToVietnameseAsync(string? text)
    {
        if (string.IsNullOrWhiteSpace(text) || text == "N/A") return text ?? string.Empty;

        var cacheKey = $"translate-vi:{text}";
        if (_cache.TryGetValue(cacheKey, out string? cached) && cached != null)
        {
            return cached;
        }

        try
        {
            // MyMemory giới hạn khoảng 500 ký tự mỗi lần gọi ở bản miễn phí; Plot phim
            // hiếm khi dài hơn nên không cần chia nhỏ.
            var url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair=en|vi";
            var result = await _http.GetFromJsonAsync<MyMemoryResponse>(url);
            var translated = result?.ResponseData?.TranslatedText;

            if (string.IsNullOrWhiteSpace(translated))
            {
                return text; // dịch thất bại -> hiển thị tạm bản gốc, không chặn trang
            }

            _cache.Set(cacheKey, translated, TimeSpan.FromDays(7));
            return translated;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Translation failed for text starting with '{Snippet}'", text.Length > 40 ? text[..40] : text);
            return text;
        }
    }
}

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace DatVeXemPhim.Services;

// Extra "giống IMDb" movie info (director, cast, country, IMDb rating, ...)
// fetched live from the OMDb API (https://www.omdbapi.com/), which sources its
// data from IMDb. Nothing here is stored in our own database — it's looked up
// on the fly by movie title and cached in memory for a while to avoid hitting
// OMDb's rate limit.
public class OmdbInfo
{
    [JsonPropertyName("Title")] public string? Title { get; set; }
    [JsonPropertyName("Year")] public string? Year { get; set; }
    [JsonPropertyName("Released")] public string? Released { get; set; }   // e.g. "21 Jul 2023"
    [JsonPropertyName("Rated")] public string? Rated { get; set; }        // e.g. "PG-13"
    [JsonPropertyName("Runtime")] public string? Runtime { get; set; }
    [JsonPropertyName("Genre")] public string? Genre { get; set; }
    [JsonPropertyName("Director")] public string? Director { get; set; }
    [JsonPropertyName("Writer")] public string? Writer { get; set; }
    [JsonPropertyName("Actors")] public string? Actors { get; set; }
    [JsonPropertyName("Plot")] public string? Plot { get; set; }
    [JsonPropertyName("Language")] public string? Language { get; set; }
    [JsonPropertyName("Country")] public string? Country { get; set; }
    [JsonPropertyName("Awards")] public string? Awards { get; set; }
    [JsonPropertyName("Poster")] public string? Poster { get; set; }
    [JsonPropertyName("imdbRating")] public string? ImdbRating { get; set; }
    [JsonPropertyName("imdbVotes")] public string? ImdbVotes { get; set; }
    [JsonPropertyName("imdbID")] public string? ImdbId { get; set; }
    [JsonPropertyName("BoxOffice")] public string? BoxOffice { get; set; }
    [JsonPropertyName("Response")] public string? Response { get; set; } // "True" | "False"
}

public class OmdbService
{
    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;
    private readonly string? _apiKey;
    private readonly ILogger<OmdbService> _logger;

    public OmdbService(HttpClient http, IMemoryCache cache, IConfiguration config, ILogger<OmdbService> logger)
    {
        _http = http;
        _cache = cache;
        _apiKey = config["Omdb:ApiKey"];
        _logger = logger;
    }

    // Returns null if no API key is configured, the movie isn't found, or the request fails.
    // Failures are swallowed (and logged) so a slow/unavailable third-party API never breaks the page.
    public async Task<OmdbInfo?> GetByTitleAsync(string title, int? year = null)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            _logger.LogWarning("Omdb:ApiKey is not configured; skipping OMDb lookup.");
            return null;
        }

        var cacheKey = $"omdb:{title.Trim().ToLowerInvariant()}:{year}";
        if (_cache.TryGetValue(cacheKey, out OmdbInfo? cached))
        {
            return cached;
        }

        try
        {
            var url = $"https://www.omdbapi.com/?apikey={Uri.EscapeDataString(_apiKey)}&t={Uri.EscapeDataString(title)}";
            if (year.HasValue) url += $"&y={year.Value}";

            var result = await _http.GetFromJsonAsync<OmdbInfo>(url);
            if (result?.Response != "True")
            {
                result = null;
            }

            // Cache both hits and misses for a while so a bad/missing title doesn't get
            // re-requested on every page load.
            _cache.Set(cacheKey, result, TimeSpan.FromHours(12));
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "OMDb lookup failed for title '{Title}'", title);
            return null;
        }
    }
}

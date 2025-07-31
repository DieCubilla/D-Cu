using System.Globalization;
using System.Text.Json;

public class NominatimGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;

    public NominatimGeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Coordinates?> GetCoordinatesAsync(Address address, CancellationToken cancellationToken = default)
    {
        var query = $"{address.Street} {address.StreetNumber}, {address.City}, {address.Department}, Uruguay";
        var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(query)}&format=json&limit=1";

        var response = await _httpClient.GetAsync(url, cancellationToken);
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var results = JsonSerializer.Deserialize<List<NominatimResult>>(content);

        if (results == null || results.Count == 0) return null;

        var lat = decimal.Parse(results[0].lat, CultureInfo.InvariantCulture);
        var lon = decimal.Parse(results[0].lon, CultureInfo.InvariantCulture);
        return new Coordinates(lat, lon);
    }

    private class NominatimResult
    {
        public string lat { get; set; } = "";
        public string lon { get; set; } = "";
    }
}

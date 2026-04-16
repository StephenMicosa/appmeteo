using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherApp.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _geoBaseUrl;

        public GeocodingService()
        {
            _httpClient = new HttpClient();
            _apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY") ?? string.Empty;
            _geoBaseUrl = Environment.GetEnvironmentVariable("OPENWEATHER_GEO_URL") ?? "https://api.openweathermap.org";

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("La variable d'environnement OPENWEATHER_API_KEY est manquante.");
            }
        }

        public async Task<(double Lat, double Lon)> GetCoordinates(string city)
        {
            string url = $"{_geoBaseUrl}/geo/1.0/direct?q={Uri.EscapeDataString(city)}&limit=1&appid={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erreur géocodage: {response.StatusCode}");
            }

            string json = await response.Content.ReadAsStringAsync();
            JArray results = JArray.Parse(json);

            JObject firstResult = results.OfType<JObject>().FirstOrDefault()
                ?? throw new Exception($"Ville '{city}' introuvable.");

            double lat = GetRequiredDouble(firstResult, "lat");
            double lon = GetRequiredDouble(firstResult, "lon");
            return (lat, lon);
        }

        private static double GetRequiredDouble(JToken root, string path)
        {
            JToken? token = root.SelectToken(path);
            double? value = token?.Value<double?>();

            if (!value.HasValue)
            {
                throw new Exception($"Donnée API manquante: {path}");
            }

            return value.Value;
        }
    }
}

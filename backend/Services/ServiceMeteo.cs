using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly GeocodingService _geocodingService;
        private readonly string _apiKey;
        private readonly string _weatherBaseUrl;
        private readonly string _geoBaseUrl;

        public WeatherService()
        {
            _httpClient = new HttpClient();
            _geocodingService = new GeocodingService();
            _apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY") ?? string.Empty;
            _weatherBaseUrl = Environment.GetEnvironmentVariable("OPENWEATHER_WEATHER_URL") ?? "https://api.openweathermap.org";
            _geoBaseUrl = Environment.GetEnvironmentVariable("OPENWEATHER_GEO_URL") ?? "https://api.openweathermap.org";

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("La variable d'environnement OPENWEATHER_API_KEY est manquante.");
            }
        }

        // Récupère la météo actuelle pour une ville
        public async Task<WeatherData> GetCurrentWeather(string city)
        {
            (double lat, double lon) = await _geocodingService.GetCoordinates(city);
            string url = $"{_weatherBaseUrl}/data/2.5/weather?lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&appid={_apiKey}&units=metric&lang=fr";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new Exception($"Ville '{city}' introuvable.");
                else
                    throw new Exception($"Erreur API: {response.StatusCode}");
            }

            string json = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(json);

            WeatherData weather = new WeatherData
            {
                CityName = GetRequiredString(data, "name"),
                Country = GetRequiredString(data, "sys.country"),
                Temperature = GetRequiredDouble(data, "main.temp"),
                FeelsLike = GetRequiredDouble(data, "main.feels_like"),
                Humidity = GetRequiredInt(data, "main.humidity"),
                WindSpeed = GetRequiredDouble(data, "wind.speed"),
                Description = GetRequiredString(data, "weather[0].description"),
                Icon = GetRequiredString(data, "weather[0].icon"),
                CloudCoverage = GetRequiredInt(data, "clouds.all"),
                Date = DateTime.Now
            };

            return weather;
        }

        // Récupère les prévisions sur 5 jours (données toutes les 3h, on prend une par jour)
        public async Task<List<ForecastDay>> GetForecast(string city)
        {
            JObject data = await GetForecastPayload(city);

            List<ForecastDay> forecasts = new List<ForecastDay>();
            JArray list = GetRequiredArray(data, "list");

            var forecastEntries = list
                .OfType<JObject>()
                .Select(item => new
                {
                    Item = item,
                    DateTimeText = GetRequiredString(item, "dt_txt")
                })
                .Select(x =>
                {
                    bool isValidDate = DateTime.TryParseExact(
                        x.DateTimeText,
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime parsedDate
                    );

                    return new
                    {
                        x.Item,
                        IsValidDate = isValidDate,
                        Date = parsedDate.Date,
                        DateTime = parsedDate
                    };
                })
                .Where(x => x.IsValidDate && x.Date > DateTime.Today)
                .GroupBy(x => x.Date)
                .OrderBy(g => g.Key)
                .Take(5);

            foreach (var dayGroup in forecastEntries)
            {
                DateTime targetMidday = dayGroup.Key.AddHours(12);

                // On prend l'entrée la plus proche de midi pour l'icône et la description.
                var representativeEntry = dayGroup
                    .OrderBy(x => Math.Abs((x.DateTime - targetMidday).TotalMinutes))
                    .First();

                ForecastDay day = new ForecastDay
                {
                    Date = dayGroup.Key,
                    TempMin = dayGroup.Min(x => GetRequiredDouble(x.Item, "main.temp_min")),
                    TempMax = dayGroup.Max(x => GetRequiredDouble(x.Item, "main.temp_max")),
                    Description = GetRequiredString(representativeEntry.Item, "weather[0].description"),
                    Icon = GetRequiredString(representativeEntry.Item, "weather[0].icon"),
                    Humidity = GetRequiredInt(representativeEntry.Item, "main.humidity"),
                    WindSpeed = GetRequiredDouble(representativeEntry.Item, "wind.speed"),
                    CloudCoverage = GetRequiredInt(representativeEntry.Item, "clouds.all")
                };

                forecasts.Add(day);
            }

            return forecasts;
        }

        // Récupère les prochaines 24h (pas de 3h) via l'endpoint forecast gratuit
        public async Task<List<HourlyForecast>> GetHourlyForecast24h(string city)
        {
            JObject data = await GetForecastPayload(city);

            List<HourlyForecast> hourlyForecasts = new List<HourlyForecast>();
            JArray list = GetRequiredArray(data, "list");
            DateTime now = DateTime.Now;
            DateTime maxDate = now.AddHours(24);

            foreach (JObject item in list.OfType<JObject>())
            {
                string dt = GetRequiredString(item, "dt_txt");
                if (!DateTime.TryParseExact(dt, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime forecastDate))
                {
                    continue;
                }

                if (forecastDate <= now || forecastDate > maxDate)
                {
                    continue;
                }

                HourlyForecast hourly = new HourlyForecast
                {
                    DateTime = forecastDate,
                    Temperature = GetRequiredDouble(item, "main.temp"),
                    Description = GetRequiredString(item, "weather[0].description"),
                    Icon = GetRequiredString(item, "weather[0].icon"),
                    Humidity = GetRequiredInt(item, "main.humidity"),
                    WindSpeed = GetRequiredDouble(item, "wind.speed")
                };

                hourlyForecasts.Add(hourly);

                if (hourlyForecasts.Count >= 8)
                {
                    break;
                }
            }

            return hourlyForecasts;
        }

        private async Task<JObject> GetForecastPayload(string city)
        {
            (double lat, double lon) = await _geocodingService.GetCoordinates(city);
            string url = $"{_weatherBaseUrl}/data/2.5/forecast?lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&appid={_apiKey}&units=metric&lang=fr";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erreur lors de la récupération des prévisions.");
            }

            string json = await response.Content.ReadAsStringAsync();
            return JObject.Parse(json);
        }

        private static JArray GetRequiredArray(JToken root, string path)
        {
            JToken? token = root.SelectToken(path);
            if (token is JArray array)
            {
                return array;
            }

            throw new Exception($"Donnée API manquante: {path}");
        }

        private static string GetRequiredString(JToken root, string path)
        {
            JToken? token = root.SelectToken(path);
            string? value = token?.Value<string>();

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception($"Donnée API manquante: {path}");
            }

            return value;
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

        private static int GetRequiredInt(JToken root, string path)
        {
            JToken? token = root.SelectToken(path);
            int? value = token?.Value<int?>();

            if (!value.HasValue)
            {
                throw new Exception($"Donnée API manquante: {path}");
            }

            return value.Value;
        }

        // Télécharge l'icône météo depuis OpenWeather
        public async Task<System.Drawing.Image> GetWeatherIcon(string iconCode)
        {
            string url = $"{_geoBaseUrl}/img/wn/{iconCode}@2x.png";
            byte[] bytes = await _httpClient.GetByteArrayAsync(url);

            using (var ms = new System.IO.MemoryStream(bytes))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }
    }
}

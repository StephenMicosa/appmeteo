using System;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        public string CityName { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int CloudCoverage { get; set; }
        public DateTime Date { get; set; }
    }
}

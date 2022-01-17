using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class DailyForecast
    {
        [JsonPropertyName("dt")]
        public long Dt { get; set; }
        [JsonPropertyName("temp")]
        public  Temperature Temperature { get; set; }
        [JsonPropertyName("feels_like")]
        public Temperature FeelsLike { get; set; }
        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }
        [JsonPropertyName("weather")]
        public Weather[] Weathers { get; set; }
    }
}

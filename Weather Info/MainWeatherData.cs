using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class MainWeatherData
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class DailyRepository
    {
        [JsonPropertyName("daily")]
        public DailyForecast[] DailyForecasts { get; set; }
    }
}

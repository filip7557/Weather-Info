using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class Temperature
    {
        [JsonPropertyName("night")]
        public double Min { get; set; }
        [JsonPropertyName("day")]
        public double Max { get; set; }
    }
}

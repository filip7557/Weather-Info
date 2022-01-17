using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }
}

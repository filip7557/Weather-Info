using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class Coord
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }
}

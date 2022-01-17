using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Weather_Info
{
    public class Repository
    {
        [JsonPropertyName("cod")]
        public int Code { get; set; }
        [JsonPropertyName("main")]
        public MainWeatherData Main { get; set; }
        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }
        [JsonPropertyName("weather")]
        public Weather[] Weather { get; set; }
        [JsonPropertyName("coord")]
        public Coord Coord { get; set; }
    }
}

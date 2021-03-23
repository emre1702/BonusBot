using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.Models
{
    public class StateInfo
    {
        [JsonPropertyName("volume")]
        public int Volume { get; init; }
    }
}

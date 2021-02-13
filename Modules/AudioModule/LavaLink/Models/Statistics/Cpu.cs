using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Statistics
{
    internal class Cpu
    {
        [JsonPropertyName("cores")]
        public int Cores { get; init; }

        [JsonPropertyName("systemLoad")]
        public double SystemLoad { get; init; }

        [JsonPropertyName("lavalinkLoad")]
        public double LavalinkLoad { get; init; }
    }
}
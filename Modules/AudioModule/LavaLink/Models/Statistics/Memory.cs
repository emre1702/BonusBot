using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Statistics
{
    internal class Memory
    {
        [JsonPropertyName("reservable")]
        public long Reservable { get; init; }

        [JsonPropertyName("used")]
        public long Used { get; init; }

        [JsonPropertyName("free")]
        public long Free { get; init; }

        [JsonPropertyName("allocated")]
        public long Allocated { get; init; }
    }
}
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Lyrics.Suggest
{
    internal class LyricsSuggestResponse
    {
        [JsonPropertyName("total")]
        public int? Total { get; init; }

        [JsonPropertyName("data")]
        public List<LyricsSuggestResponseData> Data { get; init; } = new();
    }
}
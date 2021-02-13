using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Lyrics.SearchExact
{
    internal class LyricsSearchExactResponse
    {
        [JsonPropertyName("lyrics")]
        public string Lyrics { get; init; } = string.Empty;
    }
}
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Lyrics.Suggest
{
    internal class LyricsSuggestResponseDataBase
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; } = string.Empty; // "artist", "album", "track"
    }
}
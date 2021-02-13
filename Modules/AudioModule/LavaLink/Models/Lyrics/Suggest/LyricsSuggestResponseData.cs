using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Lyrics.Suggest
{
    internal class LyricsSuggestResponseData : LyricsSuggestResponseDataBase
    {
        [JsonPropertyName("album")]
        public LyricsSuggestResponseDataAlbum Album { get; init; } = new();

        [JsonPropertyName("artist")]
        public LyricsSuggestResponseDataArtist Artist { get; init; } = new();

        [JsonPropertyName("duration")]
        public int DurationSeconds { get; init; }

        [JsonPropertyName("preview")]
        public string PreviewUrl { get; init; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;

        [JsonPropertyName("title_short")]
        public string TitleShort { get; init; } = string.Empty;
    }
}
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Lyrics.Suggest
{
    internal class LyricsSuggestResponseDataAlbum : LyricsSuggestResponseDataBase
    {
        [JsonPropertyName("cover")]
        public string CoverUrl { get; init; } = string.Empty;

        [JsonPropertyName("cover_big")]
        public string CoverBigUrl { get; init; } = string.Empty;

        [JsonPropertyName("cover_medium")]
        public string CoverMediumUrl { get; init; } = string.Empty;

        [JsonPropertyName("cover_small")]
        public string CoverSmallUrl { get; init; } = string.Empty;

        [JsonPropertyName("cover_xl")]
        public string CoverXlUrl { get; init; } = string.Empty;

        [JsonPropertyName("md5_image")]
        public string Md5ImageUrl { get; init; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;

        [JsonPropertyName("tracklist")]
        public string TracklistUrl { get; init; } = string.Empty;
    }
}
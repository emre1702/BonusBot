using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Lyrics.Suggest
{
    internal class LyricsSuggestResponseDataArtist : LyricsSuggestResponseDataBase
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("picture")]
        public string PictureUrl { get; init; } = string.Empty;

        [JsonPropertyName("picture_big")]
        public string PictureBigUrl { get; init; } = string.Empty;

        [JsonPropertyName("picture_medium")]
        public string PictureMediumUrl { get; init; } = string.Empty;

        [JsonPropertyName("picture_small")]
        public string PictureSmallUrl { get; init; } = string.Empty;

        [JsonPropertyName("picture_xl")]
        public string PictureXlUrl { get; init; } = string.Empty;

        [JsonPropertyName("tracklist")]
        public string TracklistUrl { get; init; } = string.Empty;
    }
}
using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Thumbnails
{
    public class VimeoOEmbedResponse
    {
        [JsonPropertyName("provider_url")]
        public string ProviderUrl { get; init; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; init; } = string.Empty;

        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; init; } = string.Empty;

        [JsonPropertyName("upload_date")]
        public DateTime? UploadDate { get; init; }

        [JsonPropertyName("video_id")]
        public int VideoId { get; init; }

        [JsonPropertyName("uri")]
        public string VideoUriSuffix { get; init; } = string.Empty;

        public string VideoUrl => ProviderUrl[0..^1] + VideoUriSuffix;
    }
}
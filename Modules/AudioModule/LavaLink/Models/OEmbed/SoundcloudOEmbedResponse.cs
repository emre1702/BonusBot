using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Thumbnails
{
    public class SoundcloudOEmbedResponse
    {
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; init; } = string.Empty;
    }
}
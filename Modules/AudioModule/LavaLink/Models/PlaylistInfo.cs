using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models
{
    internal class PlaylistInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("selectedTrack")]
        public int SelectedTrack { get; init; }
    }
}
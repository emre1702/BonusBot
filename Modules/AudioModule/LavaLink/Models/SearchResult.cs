using BonusBot.AudioModule.LavaLink.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models
{
    internal class SearchResult
    {
        [JsonPropertyName("playlistInfo")]
        public PlaylistInfo? PlaylistInfo { get; init; }

        [JsonPropertyName("loadType")]
        public LoadType LoadType { get; init; }

        [JsonPropertyName("tracks")]
        public IEnumerable<LavaLinkTrack> Tracks { get; set; } = new List<LavaLinkTrack>();
    }
}
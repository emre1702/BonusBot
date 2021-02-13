using BonusBot.AudioModule.LavaLink.Models.Statistics;
using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models
{
    internal class ServerStats
    {
        [JsonPropertyName("playingPlayers")]
        public int PlayingPlayers { get; init; }

        [JsonPropertyName("memory")]
        public Memory Memory { get; init; } = new();

        [JsonPropertyName("players")]
        public int PlayerCount { get; init; }

        [JsonPropertyName("cpu")]
        public Cpu Cpu { get; init; } = new();

        [JsonPropertyName("uptime")]
#pragma warning disable IDE1006 // Naming Styles
        private long _uptime { get; init; }

#pragma warning restore IDE1006 // Naming Styles

        [JsonIgnore]
        public TimeSpan Uptime => TimeSpan.FromMilliseconds(_uptime);

        [JsonPropertyName("frameStats")]
        public Frames Frames { get; init; } = new();
    }
}
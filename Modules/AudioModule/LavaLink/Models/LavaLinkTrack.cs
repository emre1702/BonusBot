using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models
{
    public class LavaLinkTrack : IEquatable<LavaLinkTrack>
    {
        [JsonPropertyName("track")]
        public string Hash { get; init; } = string.Empty;

        [JsonPropertyName("info")]
        public LavaLinkTrackInfo Info { get; init; } = new();

        public bool Equals(LavaLinkTrack? other) => Hash == other?.Hash;

        public override bool Equals(object? obj) => obj is LavaLinkTrack other && Equals(other);

        public override int GetHashCode() => Hash.GetHashCode();

        public override string ToString() => Info.ToString();
    }
}
using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models
{
    public class LavaLinkTrackInfo
    {
        [JsonPropertyName("identifier")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("isSeekable")]
        public bool IsSeekable { get; init; }

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("isStream")]
        public bool IsStream { get; set; }

        [JsonPropertyName("length")]
        public long TrackLength { get; set; }

        [JsonIgnore]
        public TimeSpan Length => TimeSpan.MaxValue.TotalMilliseconds >= TrackLength ? TimeSpan.FromMilliseconds(TrackLength) : TimeSpan.MaxValue;

        [JsonPropertyName("position")]
        public long TrackPosition { get; set; }

        [JsonIgnore]
        public TimeSpan Position
        {
            get => TimeSpan.FromTicks(TrackPosition);
            set => TrackPosition = value.Ticks;
        }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

#nullable disable

        [JsonPropertyName("uri")]
        public Uri Uri { get; set; }

#nullable restore

        public void ResetPosition() => Position = TimeSpan.Zero;

        public override string ToString() => $"{Title} ({Author})";
    }
}
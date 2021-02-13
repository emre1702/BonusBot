using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Responses
{
    internal struct PlayerState
    {
        [JsonIgnore]
        public DateTimeOffset Time
            => DateTimeOffset.FromUnixTimeMilliseconds(LongTime);

        [JsonPropertyName("time")]
        private long LongTime { get; set; }

        [JsonIgnore]
        public TimeSpan Position
            => TimeSpan.FromMilliseconds(LongPosition);

        [JsonPropertyName("position")]
        private long LongPosition { get; set; }
    }
}
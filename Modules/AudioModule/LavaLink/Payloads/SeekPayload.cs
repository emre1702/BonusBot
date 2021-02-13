using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class SeekPayload : LavaPayload
    {
        [JsonPropertyName("position")]
        public long Position { get; init; }

        public SeekPayload(ulong guildId, TimeSpan position) : base(guildId, "seek")
            => Position = (long)position.TotalMilliseconds;
    }
}
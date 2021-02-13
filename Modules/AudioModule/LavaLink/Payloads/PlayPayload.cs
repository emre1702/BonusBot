using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class PlayPayload : LavaPayload
    {
        [JsonPropertyName("track")]
        public string Hash { get; }

        [JsonPropertyName("startTime")]
        public int StartTime { get; }

        [JsonPropertyName("endTime")]
        public int EndTime { get; }

        [JsonPropertyName("noReplace")]
        public bool NoReplace { get; }

        public PlayPayload(ulong guildId, string trackHash,
                              TimeSpan start, TimeSpan end,
                              bool noReplace) : base(guildId, "play")
        {
            Hash = trackHash;
            StartTime = (int)start.TotalMilliseconds;
            EndTime = (int)end.TotalMilliseconds;
            NoReplace = noReplace;
        }

        public PlayPayload(ulong guildId, string trackHash, bool noReplace) : base(guildId, "play")
            => (Hash, NoReplace) = (trackHash, noReplace);
    }
}
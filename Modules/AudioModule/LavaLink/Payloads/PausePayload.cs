using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class PausePayload : LavaPayload
    {
        [JsonPropertyName("pause")]
        public bool Pause { get; set; }

        public PausePayload(ulong guildId, bool pause) : base(guildId, "pause")
            => Pause = pause;
    }
}
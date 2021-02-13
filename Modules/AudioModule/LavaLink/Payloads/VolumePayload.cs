using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class VolumePayload : LavaPayload
    {
        [JsonPropertyName("volume")]
        public int Volume { get; }

        public VolumePayload(ulong guildId, int volume) : base(guildId, "volume")
        {
            Volume = volume;
        }
    }
}
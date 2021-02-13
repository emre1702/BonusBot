using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class LavaPayload : BasePayload
    {
        [JsonPropertyName("guildId")]
        public string GuildId { get; init; }

        protected LavaPayload(ulong guildId, string op) : base(op)
            => GuildId = $"{guildId}";
    }
}
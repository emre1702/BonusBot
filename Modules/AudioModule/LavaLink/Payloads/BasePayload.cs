using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal abstract class BasePayload
    {
        [JsonPropertyName("op")]
        public string Op { get; init; }

        protected BasePayload(string op) => Op = op;
    }
}
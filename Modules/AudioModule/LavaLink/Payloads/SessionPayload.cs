using System;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class SessionPayload : BasePayload
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("timeout")]
        public int Timeout { get; set; }

        public SessionPayload(string key, TimeSpan time) : base("configureResuming")
            => (Key, Timeout) = (key, (int)time.TotalMilliseconds);
    }
}
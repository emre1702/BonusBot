using BonusBot.AudioModule.LavaLink.Models;
using Discord.WebSocket;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class VoiceServerPayload : LavaPayload
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; init; } = string.Empty;

        [JsonPropertyName("event")]
        public VoiceServerUpdate VoiceServerUpdate { get; init; }

        public VoiceServerPayload(SocketVoiceServer server, string voiceSessionId) : base(server.Guild.Id, "voiceUpdate")
            => (SessionId, VoiceServerUpdate) = (voiceSessionId, new VoiceServerUpdate(server));
    }
}
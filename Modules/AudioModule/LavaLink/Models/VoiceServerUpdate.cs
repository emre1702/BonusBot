using Discord.WebSocket;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models
{
    internal class VoiceServerUpdate
    {
        [JsonPropertyName("token")]
        public string Token { get; }

        [JsonPropertyName("guildid")]
        public string GuildId { get; }

        [JsonPropertyName("endpoint")]
        public string Endpoint { get; }

        public VoiceServerUpdate(SocketVoiceServer server)
        {
            Token = server.Token;
            Endpoint = server.Endpoint;
            GuildId = $"{server.Guild.Id}";
        }
    }
}
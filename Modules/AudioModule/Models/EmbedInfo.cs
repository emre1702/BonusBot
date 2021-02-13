using Discord;
using Discord.WebSocket;

namespace BonusBot.AudioModule.Models
{
    internal class EmbedInfo
    {
        public ISocketMessageChannel Channel { get; init; }
        public Embed? Embed { get; set; }
        public IMessage? Message { get; init; }

        public EmbedInfo(ISocketMessageChannel channel, Embed? embed = null, IMessage? message = null)
            => (Channel, Embed, Message) = (channel, embed, message);
    }
}
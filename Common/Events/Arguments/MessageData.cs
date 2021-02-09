using Discord.WebSocket;

namespace BonusBot.Common.Events.Arguments
{
    public class MessageData
    {
        public SocketMessage Message { get; init; }
        public bool IsCommand { get; set; }
        public bool NeedsDelete { get; set; }
        public int CommandPrefixLength { get; set; }

        public MessageData(SocketMessage msg) => Message = msg;
    }
}
using Discord.WebSocket;

namespace BonusBot.Common.Extensions
{
    public static class SocketUserExtensions
    {
        public static string GetFullNameInfo(this SocketUser socketGuildUser)
            => socketGuildUser.Username + "#" + socketGuildUser.Discriminator;
    }
}

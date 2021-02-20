using Discord.WebSocket;

namespace BonusBot.Common.Extensions
{
    public static class SocketGuildUserExtensions
    {
        public static string GetFullNameInfo(this SocketGuildUser socketGuildUser)
        {
            var userName = socketGuildUser.Username + "#" + socketGuildUser.Discriminator;
            if (!string.IsNullOrWhiteSpace(socketGuildUser.Nickname))
                userName = socketGuildUser.Nickname + " / " + userName;
            return userName;
        }
    }
}
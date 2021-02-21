using Discord;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IGuildsHandler
    {
        IBonusGuild? GetGuild(IGuild? guild);

        IBonusGuild? GetGuild(ulong? guildId);
    }
}
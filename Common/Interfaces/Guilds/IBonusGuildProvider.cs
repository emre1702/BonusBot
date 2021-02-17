using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IBonusGuildProvider
    {
        Task<IBonusGuild> Create();
    }
}
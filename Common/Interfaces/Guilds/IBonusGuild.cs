using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Guilds
{
    public interface IBonusGuild
    {
        IGuildSettingsHandler Settings { get; }

        Task Initialize();
    }
}
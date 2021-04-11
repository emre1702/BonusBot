using Discord;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Discord
{
    public interface IErrorChannel
    {
        Task<IUserMessage> SendErrorMessageAsync(string text);
    }
}

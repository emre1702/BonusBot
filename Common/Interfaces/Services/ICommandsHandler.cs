using Discord.Commands;

namespace BonusBot.Common.Interfaces.Services
{
    public interface ICommandsHandler
    {
        CommandService CommandService { get; init; }

        string GetCommandErrorText(IResult result, string content);
    }
}
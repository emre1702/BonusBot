using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    public record PositionArgs(string Position) : ICommandHandlerArgs;
}
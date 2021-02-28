using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record NumberArgs(int Number) : ICommandHandlerArgs;
}
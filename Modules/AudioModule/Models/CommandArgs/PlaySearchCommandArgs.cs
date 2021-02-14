using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record PlaySearchCommandArgs(int Number) : ICommandHandlerArgsBase;
}
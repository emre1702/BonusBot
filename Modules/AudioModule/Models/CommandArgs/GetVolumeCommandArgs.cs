using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record GetVolumeCommandArgs(bool PlainOutput) : ICommandHandlerArgs;
}

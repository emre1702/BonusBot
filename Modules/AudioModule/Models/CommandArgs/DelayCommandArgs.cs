using BonusBot.Common.Commands;
using System;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record DelayCommandArgs(TimeSpan? Delay) : ICommandHandlerArgs;
}
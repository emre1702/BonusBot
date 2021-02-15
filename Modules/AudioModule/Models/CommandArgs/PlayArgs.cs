using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record PlayArgs(string Query) : ICommandHandlerArgs, IQueryArgs;
}
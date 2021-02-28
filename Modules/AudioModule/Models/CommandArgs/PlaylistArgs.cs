using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record PlaylistArgs(string Query, int Limit) : ICommandHandlerArgs, IQueryArgs;
}
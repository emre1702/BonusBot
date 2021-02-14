using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal interface IQueryArgs : ICommandHandlerArgsBase
    {
        public string Query { get; init; }
    }
}
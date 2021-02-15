using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal interface IQueryArgs : ICommandHandlerArgs
    {
        public string Query { get; init; }
    }
}
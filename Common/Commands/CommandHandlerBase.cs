using BonusBot.Common.Extensions;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands
{
    public abstract class CommandHandlerBase<ClassT, ArgsT>
        where ClassT : CommandBase
        where ArgsT : ICommandHandlerArgsBase
    {
        public ClassT Class { get; }

        public CommandHandlerBase(ClassT c) => Class = c;

        public abstract Task Do(ArgsT args);
    }
}
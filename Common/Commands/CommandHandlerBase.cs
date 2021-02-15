using BonusBot.Common.Extensions;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands
{
    public abstract class CommandHandlerBase<ClassT, ArgsT>
        where ClassT : CommandBase
        where ArgsT : ICommandHandlerArgs
    {
        public ClassT Class { get; }

        protected CommandHandlerBase(ClassT c) => Class = c;

        public abstract Task Do(ArgsT args);
    }
}
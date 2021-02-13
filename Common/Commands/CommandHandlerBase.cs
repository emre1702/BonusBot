using BonusBot.Common.Extensions;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands
{
    public abstract class CommandHandlerBase<ClassT, ArgsT>
        where ClassT : CommandBase
        where ArgsT : CommandHandlerArgsBase
    {
        public ClassT Main { get; }

        public CommandHandlerBase(ClassT main) => Main = main;

        public abstract Task Do(ArgsT args);
    }
}
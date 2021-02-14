using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Stop : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Stop(Main main) : base(main)
        {
        }

        public override Task Do(EmptyCommandArgs _)
            => Class.Player!.Stop();
    }
}
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Stop : CommandHandlerBase<Main, DelayCommandArgs>
    {
        public Stop(Main main) : base(main)
        {
        }

        public override async Task Do(DelayCommandArgs args)
        {
            if (args.Delay.HasValue)
                await Task.Delay(args.Delay.Value);
            await Class.Player!.Stop();
        }
    }
}
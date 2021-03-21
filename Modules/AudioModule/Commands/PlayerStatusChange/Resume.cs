using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Resume : CommandHandlerBase<Main, DelayCommandArgs>
    {
        public Resume(Main main) : base(main)
        {
        }

        public override async Task Do(DelayCommandArgs args)
        {
            if (args.Delay.HasValue)
                await Task.Delay(args.Delay.Value);
            if (Class.Player!.Status == PlayerStatus.Paused)
                await Class.Player.Resume();
        }
    }
}
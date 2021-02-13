using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Stop : CommandHandlerBase<Main, CommandHandlerArgsBase>
    {
        public Stop(Main main) : base(main)
        {
        }

        public override Task Do(CommandHandlerArgsBase _)
        {
            if (Main.Player is { })
                return Main.Player.Stop();
            return Task.CompletedTask;
        }
    }
}
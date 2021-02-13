using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Resume : CommandHandlerBase<Main, CommandHandlerArgsBase>
    {
        public Resume(Main main) : base(main)
        {
        }

        public override Task Do(CommandHandlerArgsBase _)
        {
            if (Main.Player?.Status == PlayerStatus.Paused)
                return Main.Player.Resume();
            return Task.CompletedTask;
        }
    }
}
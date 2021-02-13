using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Pause : CommandHandlerBase<Main, CommandHandlerArgsBase>
    {
        public Pause(Main main) : base(main)
        {
        }

        public override Task Do(CommandHandlerArgsBase _)
        {
            if (Main.Player is { } && Main.Player.Status != PlayerStatus.Paused)
                return Main.Player.Pause();
            return Task.CompletedTask;
        }
    }
}
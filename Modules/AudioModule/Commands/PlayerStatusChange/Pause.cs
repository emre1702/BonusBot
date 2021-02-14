using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Pause : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Pause(Main main) : base(main)
        {
        }

        public override Task Do(EmptyCommandArgs _)
        {
            if (Class.Player!.Status != PlayerStatus.Paused)
                return Class.Player.Pause();
            return Task.CompletedTask;
        }
    }
}
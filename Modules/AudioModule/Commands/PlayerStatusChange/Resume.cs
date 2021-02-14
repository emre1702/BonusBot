using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.PlayerStatusChange
{
    internal class Resume : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Resume(Main main) : base(main)
        {
        }

        public override Task Do(EmptyCommandArgs _)
        {
            if (Class.Player!.Status == PlayerStatus.Paused)
                return Class.Player.Resume();
            return Task.CompletedTask;
        }
    }
}
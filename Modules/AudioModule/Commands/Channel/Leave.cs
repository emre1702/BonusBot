using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Channel
{
    internal class Leave : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Leave(Main main) : base(main)
        {
        }

        public override Task Do(EmptyCommandArgs _)
            => LavaSocketClient.Instance.DisconnectPlayer(Class.Player!.VoiceChannel);
    }
}
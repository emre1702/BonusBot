using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.Common.Extensions;
using Discord.Commands;

namespace BonusBot.AudioModule.Models
{
    public class AudioCommandBase : CommandBase
    {
        internal LavaPlayer? Player { get; private set; }

        protected override void BeforeExecute(CommandInfo command)
        {
            Player = LavaSocketClient.Instance.GetPlayer(Context.Guild.Id);

            base.BeforeExecute(command);
        }

        protected override void AfterExecute(CommandInfo command)
        {
            Context.MessageData.NeedsDelete = false;

            base.AfterExecute(command);
        }
    }
}
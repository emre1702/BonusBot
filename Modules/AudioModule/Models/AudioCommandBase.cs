using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using Discord.Commands;

namespace BonusBot.AudioModule.Models
{
    public class AudioCommandBase : CommandBase
    {
        internal LavaPlayer? Player { get; private set; }

        protected override void BeforeExecute(CommandInfo command)
        {
            base.BeforeExecute(command);

            Player = LavaSocketClient.Instance.GetPlayer(Context.Guild.Id);
        }

        protected override void AfterExecute(CommandInfo command)
        {
            if (Context is DiscordCommandContext discordCommandContext)
                discordCommandContext.MessageData.NeedsDelete = false;

            base.AfterExecute(command);
        }
    }
}
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.Common;
using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using Discord.Commands;
using System.Globalization;

namespace BonusBot.AudioModule.Models
{
    public class AudioCommandBase : CommandBase
    {
        internal LavaPlayer? Player { get; private set; }

        protected override void BeforeExecute(CommandInfo command)
        {
            Player = LavaSocketClient.Instance.GetPlayer(Context.Guild.Id);
            ModuleTexts.Culture = Context.BonusGuild.Settings.CultureInfo;

            base.BeforeExecute(command);
        }

        protected override void AfterExecute(CommandInfo command)
        {
            Context.MessageData.NeedsDelete = false;

            base.AfterExecute(command);
        }
    }
}
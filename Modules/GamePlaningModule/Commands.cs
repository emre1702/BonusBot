using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    [RequireContext(ContextType.Guild)]
    public partial class GamePlaning : CommandBase
    {
        [Command("treffen")]
        [Alias("meet", "meetup", "gameplan", "gameplaning", "playplan", "playplaning", "plangame", "planinggame", "planplay", "planingplay", "treff", "termin")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        [RequireEmoteSetting(Settings.AnnouncementEmoteId)]
        [RequireSetting(Settings.AnnouncementMentionEveryone, typeof(bool))]
        public async Task PlanMeetup(string game, DateTime time)
        {
            var emote = Context.GetRequiredEmoteSetting(Settings.AnnouncementEmoteId);
            var mentionEveryone = Context.GetRequiredSettingValue<bool>(Settings.AnnouncementMentionEveryone);

            var embedBuilder = Helpers.CreateAnnouncementEmbedBuilder(game, time.ToString(), string.Empty, 0, emote).WithAuthor(Context.SocketUser);
            if (mentionEveryone)
                embedBuilder.Description = Context.Guild.EveryoneRole.Mention + " " + embedBuilder.Description;

            var message = await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());
            await message.AddReactionAsync(emote);
        }
    }
}
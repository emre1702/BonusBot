using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.GamePlaningModule.Models;
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
        [RequireEmoteSetting(Settings.ParticipationEmoteId)]
        [RequireEmoteSetting(Settings.LateParticipationEmoteId)]
        [RequireEmoteSetting(Settings.CancellationEmoteId)]
        [RequireEmoteSetting(Settings.MaybeEmoteId)]
        [RequireSetting(Settings.MentionEveryone, typeof(bool))]
        public async Task PlanMeetup(string game, DateTime time)
        {
            var participationEmote = Context.GetRequiredEmoteSetting(Settings.ParticipationEmoteId);
            var lateParticipationEmote = Context.GetRequiredEmoteSetting(Settings.LateParticipationEmoteId);
            var cancellationEmote = Context.GetRequiredEmoteSetting(Settings.CancellationEmoteId);
            var maybeEmote = Context.GetRequiredEmoteSetting(Settings.MaybeEmoteId);
            var mentionEveryone = Context.GetRequiredSettingValue<bool>(Settings.MentionEveryone);

            var embedData = new AnnouncementEmbedData(game, time.ToString(), new() { }, participationEmote, new() { }, maybeEmote, new(), lateParticipationEmote, new(), cancellationEmote);
            var embedBuilder = Helpers.CreateAnnouncementEmbedBuilder(embedData)
                .WithAuthor(Context.SocketUser);

            var text = mentionEveryone ? Context.Guild.EveryoneRole.Mention : string.Empty;
            var message = await Context.Channel.SendMessageAsync(text, embed: embedBuilder.Build());
            await message.AddReactionAsync(participationEmote);
            await message.AddReactionAsync(lateParticipationEmote);
            await message.AddReactionAsync(cancellationEmote);
        }
    }
}
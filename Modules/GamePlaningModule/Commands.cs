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
        [RequireSetting(Settings.MentionEveryone)]
        public async Task PlanMeetup(string game, DateTime time)
        {
            var moduleName = GetType().Assembly.ToModuleName();
            var participationEmote = await Context.BonusGuild!.Settings.Get<Emote>(moduleName, Settings.ParticipationEmoteId);
            var lateParticipationEmote = await Context.BonusGuild.Settings.Get<Emote>(moduleName, Settings.LateParticipationEmoteId);
            var cancellationEmote = await Context.BonusGuild.Settings.Get<Emote>(moduleName, Settings.CancellationEmoteId);
            var maybeEmote = await Context.BonusGuild.Settings.Get<Emote>(moduleName, Settings.MaybeEmoteId);
            var mentionEveryone = await Context.BonusGuild.Settings.Get<bool>(moduleName, Settings.MentionEveryone);

            var embedData = new AnnouncementEmbedData(game, time.ToString(),
                new(participationEmote, new()),
                new(lateParticipationEmote, new()),
                new(maybeEmote, new()),
                new(cancellationEmote, new()));
            var embedBuilder = Helpers.CreateAnnouncementEmbedBuilder(embedData)
                .WithAuthor(Context.SocketUser);

            var text = mentionEveryone ? Context.Guild.EveryoneRole.Mention : string.Empty;
            var message = await Context.Channel.SendMessageAsync(text, embed: embedBuilder.Build());
            await message.AddReactionAsync(participationEmote);
            await message.AddReactionAsync(lateParticipationEmote);
            await message.AddReactionAsync(maybeEmote);
            await message.AddReactionAsync(cancellationEmote);
        }
    }
}
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.Common.Languages;
using BonusBot.GamePlaningModule.Language;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    [RequireContext(ContextType.Guild)]
    public partial class GamePlaning : CommandBase
    {
        private static GuildEmote? _lastAnnouncementEmote;
        private static readonly HashSet<ulong> _announcementMessageIds = new HashSet<ulong>();

        [Command("treffen")]
        [Alias("meet", "meetup", "gameplan", "gameplaning", "playplan", "playplaning", "plangame", "planinggame", "planplay", "planingplay", "treff", "termin")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        //[RequireTextChannelSetting(Settings.AnnouncementChannelId)]
        [RequireEmoteSetting(Settings.AnnouncementEmoteId)]
        [RequireSetting(Settings.AnnouncementMentionEveryone, typeof(bool))]
        public async Task PlanMeetup(string game, DateTime time)
        {
            var emote = Context.GetRequiredEmoteSetting(Settings.AnnouncementEmoteId);
            var mentionEveryone = Context.GetRequiredSettingValue<bool>(Settings.AnnouncementMentionEveryone);

            _lastAnnouncementEmote = emote;

            var messageContent = string.Format(ModuleTexts.MeetupAnnouncement.Replace("\n", Environment.NewLine), Context.SocketUser.Mention, game, time.ToString(), emote.ToString());
            if (mentionEveryone)
                messageContent = Context.Guild.EveryoneRole.Mention + Environment.NewLine + messageContent;
            var message = await Context.Channel.SendMessageAsync(messageContent);
            lock (_announcementMessageIds)
            {
                _announcementMessageIds.Add(message.Id);
            }
            await message.AddReactionAsync(emote);
        }
    }
}
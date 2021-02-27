using BonusBot.Common.Enums;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Workers;
using BonusBot.Database.Entities.Cases;
using BonusBot.GamePlaningModule.Language;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule.Jobs
{
    public class RemindJob : JobBase
    {
        protected override TimeSpan DelayTime => TimeSpan.FromSeconds(5);

        private readonly IDiscordClientHandler _discordClientHandler;
        private readonly ITimedActionsHandler _timedActionsHandler;
        private readonly IGuildsHandler _guildsHandler;

        public RemindJob(IDiscordClientHandler discordClientHandler, ITimedActionsHandler timedActionsHandler, IGuildsHandler guildsHandler)
            => (_discordClientHandler, _timedActionsHandler, _guildsHandler) = (discordClientHandler, timedActionsHandler, guildsHandler);

        protected override async ValueTask DoWork()
        {
            var client = await _discordClientHandler.ClientSource.Task;

            var remindActions = _timedActionsHandler.Get(ActionType.Remind, GetType());
            if (remindActions.Count == 0) return;

            foreach (var remindAction in remindActions)
            {
                if (!remindAction.MaxDelay.HasValue || remindAction.AtDateTime + remindAction.MaxDelay >= DateTime.UtcNow)
                    await HandleAction(remindAction, client);
                _timedActionsHandler.Remove(remindAction);
            }
            await _timedActionsHandler.Save();
        }

        private async Task HandleAction(TimedActions action, DiscordSocketClient client)
        {
            try
            {
                var (message, channel) = await GetMessageAndChannel(action.GuildId, action.AdditionalId!.Value, action.TargetId, client);
                if (message is null || channel is null) return;

                var userToRemind = await GetUserToRemind(action.GuildId, message);
                if (userToRemind?.Any() != true) return;

                var remindText = string.Format(ModuleTexts.RemindInfo, string.Join(", ", userToRemind.Select(u => u.Mention)));
                await channel.SendMessageAsync(remindText, messageReference: new(action.TargetId, action.AdditionalId!.Value, action.GuildId));
            }
            catch (Exception ex)
            {
                ConsoleHelper.Log(LogSeverity.Error, LogSource.Job, $"HandleAction for RemindJob failed. TimedAction:{Environment.NewLine}{JsonSerializer.Serialize(action)}", ex);
            }
        }

        private async Task<(IMessage? Message, SocketTextChannel? Channel)> GetMessageAndChannel(ulong guildId, ulong channelId, ulong messageId, DiscordSocketClient client)
        {
            var guild = client.Guilds.FirstOrDefault(g => g.Id == guildId);
            if (guild is null)
                return (null, null);

            var channel = guild.GetTextChannel(channelId);
            if (channel is null)
                return (null, channel);

            return (await channel.GetMessageAsync(messageId), channel);
        }

        private async Task<IEnumerable<IUser>?> GetUserToRemind(ulong guildId, IMessage message)
        {
            var bonusGuild = _guildsHandler.GetGuild(guildId);
            if (bonusGuild is null)
                return null;

            var participationEmote = await bonusGuild.Settings.Get<Emote>(GetType().Assembly, Settings.ParticipationEmoteId);
            var maybeEmote = await bonusGuild.Settings.Get<Emote>(GetType().Assembly, Settings.MaybeEmoteId);
            var lateParticipationEmote = await bonusGuild.Settings.Get<Emote>(GetType().Assembly, Settings.LateParticipationEmoteId);

            return (await Helpers.GetReactedUsers(message, participationEmote))
                .Union(await Helpers.GetReactedUsers(message, maybeEmote))
                .Union(await Helpers.GetReactedUsers(message, lateParticipationEmote))
            ;
        }
    }
}
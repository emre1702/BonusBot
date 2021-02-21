using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.GamePlaningModule.Language;
using BonusBot.GamePlaningModule.Models;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    public partial class GamePlaning
    {
        private static bool _initialized;

        private readonly IDiscordClientHandler _discordClientHandler;
        private readonly IGuildsHandler _guildsHandler;

        public GamePlaning(IDiscordClientHandler discordClientHandler, IGuildsHandler guildsHandler)
        {
            _discordClientHandler = discordClientHandler;
            _guildsHandler = guildsHandler;

            AddEvents();
        }

        private async void AddEvents()
        {
            if (_initialized) return;

            _initialized = true;
            var client = await _discordClientHandler.ClientSource.Task;
            client.ReactionAdded += SetParticipantsToMessage;
            client.ReactionRemoved += SetParticipantsToMessage;
        }

        private async Task SetParticipantsToMessage(Cacheable<IUserMessage, ulong> cachedMessageOrId, ISocketMessageChannel channel, SocketReaction reaction)
        {
            var message = await cachedMessageOrId.DownloadAsync();
            if (!message.Author.IsBot)
                return;
            var embed = message.Embeds.FirstOrDefault();
            if (embed is null)
                return;
            if (!embed.Footer.HasValue || embed.Footer.Value.Text != Helpers.GetAnnouncementFooter())
                return;

            var guildChannel = (SocketGuildChannel)channel;
            var bonusGuild = _guildsHandler.GetGuild(guildChannel.Guild);
            if (bonusGuild is null)
                return;

            Thread.CurrentThread.CurrentUICulture = bonusGuild.Settings.CultureInfo;
            var participationData = await GetEmoteData(bonusGuild, message, Settings.ParticipationEmoteId);
            var lateParticipationData = await GetEmoteData(bonusGuild, message, Settings.LateParticipationEmoteId);
            var maybeData = await GetEmoteData(bonusGuild, message, Settings.MaybeEmoteId);
            var cancellationData = await GetEmoteData(bonusGuild, message, Settings.CancellationEmoteId);
            var mentionEveryone = await bonusGuild.Settings.Get<bool>(GetType().Assembly, Settings.MentionEveryone);

            var author = embed.Author!.Value;
            var embedData = new AnnouncementEmbedData(embed.Fields[0].Value, embed.Fields[1].Value, participationData, lateParticipationData, maybeData, cancellationData);
            var newEmbedBuilder = Helpers.CreateAnnouncementEmbedBuilder(embedData)
                .WithAuthor(author.Name, author.IconUrl, author.Url);
            await message.ModifyAsync(prop =>
            {
                prop.Embed = newEmbedBuilder.Build();
                if (mentionEveryone)
                    prop.Content = guildChannel.Guild.EveryoneRole.Mention;
            });

            var msgText = Helpers.GetWithoutParticipantsMessage(message.Content);

            if (participationData.Reactors.Count == 0)
                await message.AddReactionAsync(participationData.Emote);
            if (lateParticipationData.Reactors.Count == 0)
                await message.AddReactionAsync(lateParticipationData.Emote);
            if (maybeData.Reactors.Count == 0)
                await message.AddReactionAsync(maybeData.Emote);
            if (cancellationData.Reactors.Count == 0)
                await message.AddReactionAsync(cancellationData.Emote);
        }

        private async Task<EmoteData> GetEmoteData(IBonusGuild bonusGuild, IUserMessage message, string key)
        {
            var emote = await bonusGuild.Settings.Get<Emote>(GetType().Assembly, key);
            var reactors = await Helpers.GetReactedUsers(message, emote, bonusGuild.DiscordGuild, _discordClientHandler);
            return new(emote, reactors);
        }
    }
}
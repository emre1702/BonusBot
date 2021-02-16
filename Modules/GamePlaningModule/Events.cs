using BonusBot.Common.Defaults;
using BonusBot.Database;
using BonusBot.GamePlaningModule.Language;
using BonusBot.GamePlaningModule.Models;
using BonusBot.Services.DiscordNet;
using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    public partial class GamePlaning
    {
        private static bool _initialized;

        private readonly SocketClientHandler _socketClientHandler;
        private readonly BonusDbContextFactory _dbContextFactory;

        public GamePlaning(SocketClientHandler socketClientHandler, BonusDbContextFactory dbContextFactory)
        {
            _socketClientHandler = socketClientHandler;
            _dbContextFactory = dbContextFactory;
            ModuleTexts.Culture = Constants.Culture;

            AddEvents();
        }

        private async void AddEvents()
        {
            if (_initialized) return;

            _initialized = true;
            var client = await _socketClientHandler.ClientSource.Task;
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

            var participationEmote = await GetSettingEmote(_dbContextFactory, guildChannel.Guild, Settings.ParticipationEmoteId);
            var lateParticipationEmote = await GetSettingEmote(_dbContextFactory, guildChannel.Guild, Settings.LateParticipationEmoteId);
            var maybeEmote = await GetSettingEmote(_dbContextFactory, guildChannel.Guild, Settings.MaybeEmoteId);
            var cancellationEmote = await GetSettingEmote(_dbContextFactory, guildChannel.Guild, Settings.CancellationEmoteId);
            var mentionEveryone = await GetSetting<bool>(_dbContextFactory, guildChannel.Guild, Settings.MentionEveryone);

            var participantNames = await Helpers.GetReactedUsers(message, participationEmote, guildChannel.Guild, _socketClientHandler);
            var lateParticipantNames = await Helpers.GetReactedUsers(message, lateParticipationEmote, guildChannel.Guild, _socketClientHandler);
            var maybeNames = await Helpers.GetReactedUsers(message, maybeEmote, guildChannel.Guild, _socketClientHandler);
            var cancellationtNames = await Helpers.GetReactedUsers(message, cancellationEmote, guildChannel.Guild, _socketClientHandler);

            var author = embed.Author!.Value;
            var embedData = new AnnouncementEmbedData(embed.Fields[0].Value, embed.Fields[1].Value, participantNames, participationEmote, lateParticipantNames, lateParticipationEmote,
                maybeNames, maybeEmote, cancellationtNames, cancellationEmote);
            var newEmbedBuilder = Helpers.CreateAnnouncementEmbedBuilder(embedData)
                .WithAuthor(author.Name, author.IconUrl, author.Url);
            await message.ModifyAsync(prop =>
            {
                prop.Embed = newEmbedBuilder.Build();
                if (mentionEveryone)
                    prop.Content = guildChannel.Guild.EveryoneRole.Mention;
            });

            var msgText = Helpers.GetWithoutParticipantsMessage(message.Content);

            if (participantNames.Count == 0)
                await message.AddReactionAsync(participationEmote);
            if (lateParticipantNames.Count == 0)
                await message.AddReactionAsync(lateParticipationEmote);
            if (maybeNames.Count == 0)
                await message.AddReactionAsync(maybeEmote);
            if (cancellationtNames.Count == 0)
                await message.AddReactionAsync(cancellationEmote);
        }
    }
}
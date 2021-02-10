using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using BonusBot.GamePlaningModule.Language;
using BonusBot.Services.DiscordNet;
using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BonusBot.GamePlaningModule
{
    public partial class GamePlaning
    {
        private static bool _initialized;

        private readonly SocketClientHandler _socketClientHandler;
        private readonly FunDbContextFactory _dbContextFactory;

        public GamePlaning(SocketClientHandler socketClientHandler, FunDbContextFactory dbContextFactory)
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
            var emote = await GetSettingEmote(_dbContextFactory, guildChannel.Guild, Settings.AnnouncementEmoteId);
            var reactedUsers = await message.GetReactionUsersAsync(emote, 100).FlattenAsync();

            var names = await Helpers.GetUserNames(_socketClientHandler, reactedUsers, guildChannel).ToListAsync();
            var namesStr = string.Join(", ", names);

            var author = embed.Author!.Value;
            var newEmbedBuilder = Helpers.CreateAnnouncementEmbedBuilder(embed.Fields[0].Value, embed.Fields[1].Value, namesStr, names.Count, emote)
                .WithAuthor(author.Name, author.IconUrl, author.Url);
            await message.ModifyAsync(prop => prop.Embed = newEmbedBuilder.Build());

            var msgText = Helpers.GetWithoutParticipantsMessage(message.Content);
        }
    }
}
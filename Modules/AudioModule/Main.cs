using BonusBot.AudioModule.Commands.PlayerStatusChange;
using BonusBot.AudioModule.Commands.Track;
using BonusBot.AudioModule.Commands.Volume;
using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.AudioModule.Preconditions;
using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using BonusBot.Services.DiscordNet;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BonusBot.AudioModule
{
    [RequireContext(ContextType.Guild)]
    [RequireBotPermission(GuildPermission.Speak & GuildPermission.Connect)]
    public class Main : CommandBase
    {
        internal static LavaRestClient LavaRestClient = new();
        public BonusDbContextFactory DbContextFactory { get; }

        private static bool _initialized;

        internal LavaPlayer? Player { get; private set; }

        [Command("play")]
        [Alias("yt", "youtube", "ytplay", "youtubeplay", "playyt", "playyoutube")]
        [RequirePlayer]
        public Task PlayYouTube([Remainder] string query)
            => new PlayYouTube(this).Do(new(query));

        [Command("queue")]
        [Alias("ytqueue", "youtubequeue", "queueyt", "queueyoutube")]
        [RequirePlayer]
        public Task QueueYouTube([Remainder] string query)
            => new QueueYouTube(this).Do(new(query));

        [Command("resume")]
        [Alias("unpause")]
        public Task Resume()
            => new Resume(this).Do(new());

        [Command("pause")]
        [Alias("unresume")]
        public Task Pause()
            => new Pause(this).Do(new());

        [Command("stop")]
        public Task Stop()
            => new Stop(this).Do(new());

        [Command("volume")]
        [Alias("SetVolume", "SetVol", "Vol", "Volum", "lautstärke")]
        public Task SetVolume([RequireNumberRange(0, 200)] int volume)
            => new SetVolume(this).Do(new(volume));

        [Command("volume")]
        [Alias("GetVolume", "GetVol", "Vol", "Volum", "lautstärke")]
        public Task GetVolume()
            => new GetVolume(this).Do(new());

        public Main(SocketClientHandler socketClientHandler, BonusDbContextFactory bonusDbContextFactory)
        {
            DbContextFactory = bonusDbContextFactory;
            Initialize(socketClientHandler);
        }

        private async void Initialize(SocketClientHandler socketClientHandler)
        {
            if (_initialized) return;

            _initialized = true;
            var client = await socketClientHandler.ClientSource.Task;
            await LavaSocketClient.Instance.Start(client);
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            Player = LavaSocketClient.Instance.GetPlayer(Context.Guild.Id);

            base.BeforeExecute(command);
        }

        protected override void AfterExecute(CommandInfo command)
        {
            Context.MessageData.NeedsDelete = false;

            base.AfterExecute(command);
        }
    }
}
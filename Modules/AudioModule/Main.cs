using BonusBot.AudioModule.Commands.Channel;
using BonusBot.AudioModule.Commands.PlayerStatusChange;
using BonusBot.AudioModule.Commands.Queue;
using BonusBot.AudioModule.Commands.Search;
using BonusBot.AudioModule.Commands.Track;
using BonusBot.AudioModule.Commands.Volume;
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.AudioModule.Models;
using BonusBot.AudioModule.Preconditions;
using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using Discord;
using Discord.Commands;
using Discord.Commands.Builders;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule
{
    [RequireContext(ContextType.Guild)]
    [RequireBotPermission(GuildPermission.Speak & GuildPermission.Connect)]
    [RequireAudioBotRole]
    public class Main : AudioCommandBase
    {
        protected static Main? Instance { get; private set; }

        internal static LavaRestClient LavaRestClient = new();
        public BonusDbContextFactory DbContextFactory { get; }

        private readonly IDiscordClientHandler _discordClientHandler;

        [Command("play")]
        [Alias("yt", "youtube", "ytplay", "youtubeplay", "playyt", "playyoutube")]
        [RequirePlayer]
        public Task PlayYouTube([Remainder] string query)
            => new PlayYouTube(this).Do(new(query));

        [Command("queue")]
        [Alias("ytqueue", "youtubequeue", "queueyt", "queueyoutube", "ytplaylist", "ytwarteschlange")]
        [RequirePlayer]
        public Task QueueYouTube([Remainder] string query)
            => new QueueYouTube(this).Do(new(query));

        [Command("resume")]
        [Alias("unpause")]
        [RequirePlayer(false)]
        public Task Resume(TimeSpan? delay = null)
            => new Resume(this).Do(new(delay));

        [Command("pause")]
        [Alias("unresume")]
        [RequirePlayer(false)]
        public Task Pause(TimeSpan? delay = null)
            => new Pause(this).Do(new(delay));

        [Command("stop")]
        [RequirePlayer(false)]
        public Task Stop(TimeSpan? delay = null)
            => new Stop(this).Do(new(delay));

        [Command("volume")]
        [Alias("SetVolume", "SetVol", "Vol", "Volum", "lautstärke")]
        public Task SetVolume([RequireNumberRange(0, 200)] int volume)
            => new SetVolume(this).Do(new(volume));

        [Command("volume")]
        [Alias("GetVolume", "GetVol", "Vol", "Volum", "lautstärke")]
        public Task GetVolume()
            => new GetVolume(this).Do(new());

        [Command("replay")]
        [Alias("wiederholen", "replaycurrent", "replaynow")]
        [RequirePlayer(false)]
        public Task ReplayCurrent()
            => new ReplayCurrent(this).Do(new());

        [Command("replayprevious")]
        [Alias("wiederholevorherigen", "wiederholevorherig", "previousreplay")]
        [RequirePlayer(false)]
        public Task ReplayPrevious()
            => new ReplayPrevious(this).Do(new());

        [Command("position")]
        [Alias("SetPosition", "PositionSet", "Vorspulen", "Spulen", "Zurückspulen")]
        [RequireCurrentTrack]
        [Priority(1)]
        public Task InvalidPosition(int _)
            => ReplyAsync(ModuleTexts.SetPositionWrongFormatError);

        [Command("position")]
        [Alias("SetPosition", "PositionSet", "Vorspulen", "Spulen", "Zurückspulen")]
        [RequireCurrentTrack]
        public Task InvalidPosition(string input)
            => new SetPosition(this).Do(new(input));

        [Command("skip")]
        [Alias("Überspringen", "skippen", "next", "weiter")]
        [RequireCurrentTrack]
        public Task Skip()
            => new Skip(this).Do(new());

        [Command("shuffle")]
        [Alias("mix")]
        [RequirePlayer]
        public Task Shuffle()
            => new Shuffle(this).Do(new());

        [Command("NowPlaying")]
        [Alias("playing")]
        [RequireCurrentTrack]
        public Task OutputCurrentlyPlaying()
            => new NowPlaying(this).Do(new());

        [Command("Lyrics")]
        [RequireCurrentTrack]
        public Task OutputLyrics()
            => new Lyrics(this).Do(new());

        [Command("Queue")]
        [Alias("Playlist", "Warteschlange", "Warteschleife")]
        [RequirePlayer(false)]
        public Task OutputQueue()
            => new OutputQueue(this).Do(new());

        [Command("DeleteQueue")]
        [Alias("DelQueue", "QueueDelete", "QueueDel", "DeletePlaylist", "PlaylistDelete", "PlaylistDel", "DelPlaylist", "LöscheWarteschlange", "WarteschlangeLöschen")]
        [RequirePlayer(false)]
        public Task DeleteQueue(int playlistNumber)
            => new DeleteQueue(this).Do(new(playlistNumber));

        [Command("disconnect")]
        [Alias("leave")]
        [RequirePlayer(false)]
        public Task Leave()
            => new Leave(this).Do(new());

        [Command("join")]
        [Alias("come")]
        [RequirePlayer]
        public Task Join()
            => new Join(this).Do(new());

        [Group("search")]
        [Alias("YouTubeSearch", "ytSearch", "SearchYt", "SearchYouTube")]
        [RequirePlayer]
        [RequireAudioBotRole]
        public class SearchGroup : AudioCommandBase
        {
            [Command("YouTube")]
            [Alias("yt", "y")]
            [Priority(2)]
            [RequirePlayer(true)]
            public Task SearchYouTube([Remainder] string query)
                => new SearchYouTube(this).Do(new(query, 10));

            [Command("YouTube")]
            [Alias("yt", "y")]
            [Priority(1)]
            [RequirePlayer(true)]
            public Task SearchYouTube([RequireNumberRange(1, 20)] int limit, [Remainder] string query)
                => new SearchYouTube(this).Do(new(query, limit));

            [Command("SoundCloud")]
            [Alias("sc", "s")]
            [Priority(4)]
            [RequirePlayer(true)]
            public Task SearchSoundcloudAsync([Remainder] string query)
                => new SearchSoundcloud(this).Do(new(query, 10));

            [Command("SoundCloud")]
            [Alias("sc", "s")]
            [Priority(3)]
            [RequirePlayer(true)]
            public Task SearchSoundcloudAsync([RequireNumberRange(1, 20)] int limit, [Remainder] string query)
                => new SearchSoundcloud(this).Do(new(query, limit));

            [Command("Play")]
            [Alias("play", "start")]
            [Priority(5)]
            [RequireSearchResult]
            public Task PlaySearchResult([RequireNumberRange(1, 20)] int number)
                => new PlaySearchResult(this).Do(new(number));

            [Command("Queue")]
            [Alias("ytqueue", "scqueue", "playlist", "warteschlange")]
            [Priority(5)]
            [RequireSearchResult]
            public Task QueueSearchResult([RequireNumberRange(1, 20)] int number)
                => new QueueSearchResult(this).Do(new(number));
        }

        public Main(IDiscordClientHandler discordClientHandler, BonusDbContextFactory bonusDbContextFactory)
        {
            Instance = this;
            _discordClientHandler = discordClientHandler;
            DbContextFactory = bonusDbContextFactory;
        }

        protected override async void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            var client = await _discordClientHandler.ClientSource.Task;
            await LavaSocketClient.Instance.Start(client);
        }
    }
}
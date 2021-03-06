﻿using BonusBot.AudioModule.Commands.Track;
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Preconditions;
using Discord.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Command("play")]
        [Alias("yt", "youtube", "ytplay", "youtubeplay", "playyt", "playyoutube")]
        [RequirePlayer]
        public Task PlayYouTube([Remainder] string query)
            => new PlayYouTube(this).Do(new(query));

        [Command("queue")]
        [Alias("ytqueue", "youtubequeue", "queueyt", "queueyoutube", "ytwarteschlange")]
        [RequirePlayer]
        public Task QueueYouTube([Remainder] string query)
            => new QueueYouTube(this).Do(new(query));

        [Command("scplay")]
        [Alias("sc", "soundcloud", "soundcloudplay", "playsc", "playsoundcloud")]
        [RequirePlayer]
        public Task PlaySoundcloud([Remainder] string query)
           => new PlaySoundcloud(this).Do(new(query));

        [Command("scqueue")]
        [Alias("scqueue", "soundcloudqueue", "queuesc", "queuesoundcloud", "scwarteschlange")]
        [RequirePlayer]
        public Task QueueSoundcloud([Remainder] string query)
            => new QueueSoundcloud(this).Do(new(query));

        [Command("position")]
        [Alias("SetPosition", "PositionSet", "Vorspulen", "Spulen", "Zurückspulen")]
        [RequireCurrentTrack]
        [Priority(1)]
        public Task InvalidPosition(int _)
           => ReplyErrorAsync(ModuleTexts.SetPositionWrongFormatError);

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

        [Command("NowPlaying")]
        [Alias("playing")]
        [RequireCurrentTrack]
        public Task OutputCurrentlyPlaying()
            => new NowPlaying(this).Do(new());

        [Command("Lyrics")]
        [RequireCurrentTrack]
        public Task OutputLyrics()
            => new Lyrics(this).Do(new());

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

    }
}

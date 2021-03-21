using BonusBot.AudioModule.Commands.Playlist;
using BonusBot.AudioModule.Preconditions;
using BonusBot.Common.Commands.Conditions;
using Discord.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Command("playlist")]
        [Alias("pl", "ytpl", "plyt", "ytplaylist", "playlistyoutube")]
        [Priority(2)]
        [RequirePlayer]
        public Task PlaylistYouTube([RequireNumberRange(1, 100)] int limit, [Remainder] string query)
            => new PlaylistYouTube(this).Do(new(query, limit));

        [Command("playlist")]
        [Alias("pl", "ytpl", "plyt", "ytplaylist", "playlistyoutube")]
        [Priority(1)]
        [RequirePlayer]
        public Task PlaylistYouTube([Remainder] string query)
            => new PlaylistYouTube(this).Do(new(query, 100));

        [Command("QueuePlaylist")]
        [Alias("qpl", "ytqpl", "qytpl", "qplyt", "plqyt", "ytqueueplaylist", "queueplaylistyoutube")]
        [Priority(2)]
        [RequirePlayer]
        public Task QueuePlaylistYouTube([RequireNumberRange(1, 100)] int limit, [Remainder] string query)
            => new QueuePlaylistYouTube(this).Do(new(query, limit));

        [Command("QueuePlaylist")]
        [Alias("qpl", "ytqpl", "qytpl", "qplyt", "plqyt", "ytqueueplaylist", "queueplaylistyoutube")]
        [Priority(1)]
        [RequirePlayer]
        public Task QueuePlaylistYouTube([Remainder] string query)
            => new QueuePlaylistYouTube(this).Do(new(query, 100));
    }
}

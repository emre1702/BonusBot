using BonusBot.AudioModule.Commands.Search;
using BonusBot.AudioModule.Models;
using BonusBot.AudioModule.Preconditions;
using BonusBot.Common.Commands.Conditions;
using Discord.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Group("search")]
        [Alias("YouTubeSearch", "ytSearch", "SearchYt", "SearchYouTube", "suche", "sucheyt", "ytsuche")]
        [RequirePlayer]
        [RequireAudioBotRole]
        public class SearchGroup : AudioCommandBase
        {
            [Command("YouTube")]
            [Alias("yt", "y")]
            [RequirePlayer(true)]
            public Task SearchYouTube([Remainder] string query)
                => new SearchYouTube(this).Do(new(query));

            [Command("SoundCloud")]
            [Alias("sc", "s")]
            [RequirePlayer(true)]
            public Task SearchSoundcloudAsync([Remainder] string query)
                => new SearchSoundcloud(this).Do(new(query));

            [Command("Play")]
            [Alias("play", "start")]
            [RequireSearchResult]
            public Task PlaySearchResult([RequireNumberRange(1, 20)] int number)
                => new PlaySearchResult(this).Do(new(number));

            [Command("Queue")]
            [Alias("ytqueue", "scqueue", "warteschlange")]
            [RequireSearchResult]
            public Task QueueSearchResult([RequireNumberRange(1, 20)] int number)
                => new QueueSearchResult(this).Do(new(number));
        }
    }
}

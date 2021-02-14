using BonusBot.AudioModule.LavaLink.Models.Lyrics.SearchExact;
using BonusBot.AudioModule.LavaLink.Models.Lyrics.Suggest;
using BonusBot.Common.Extensions;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BonusBot.AudioModule.LavaLink.Helpers
{
    internal class LyricsHelper
    {
        public static LyricsHelper Instance => _lazy.Value;

        private static readonly Lazy<LyricsHelper> _lazy = new(() => new(), true);

        private LyricsHelper()
        {
        }

        private Regex Compiled(string pattern)
            => new(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));

        public async Task<string> Search(string searchText)
        {
            var (author, title) = await Suggest(searchText).ConfigureAwait(false);
            return await SearchExact(author, title).ConfigureAwait(false);
        }

        public Task<string> Search(string trackAuthor, string trackTitle)
        {
            var (author, title) = GetSongInfo(trackAuthor, trackTitle);
            return SearchExact(author, title);
        }

        private Task<string> MakeRequest(string url)
        {
            return HttpHelper.Instance.GetString(new Uri($"https://api.lyrics.ovh/{url}"));
        }

        private async Task<(string Author, string Title)> Suggest(string searchText)
        {
            var (author, title) = GetSongInfo(string.Empty, searchText);
            var responseJson =
                await MakeRequest($"suggest/{HttpUtility.UrlEncode($"{author} {title}")}")
                    .ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(responseJson))
                return default;

            var response = JsonSerializer.Deserialize<LyricsSuggestResponse>(responseJson);
            if (response?.Total.HasValue != true || response.Total!.Value == 0)
                return default;

            var data = response.Data.First();
            return (data.Artist.Name, data.Title);
        }

        private async Task<string> SearchExact(string trackAuthor, string trackTitle)
        {
            var responseJson =
                await MakeRequest($"v1/{HttpUtility.UrlEncode(trackAuthor)}/{HttpUtility.UrlEncode(trackTitle)}")
                    .ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(responseJson))
                return string.Empty;

            LyricsSearchExactResponse? response;
            try
            {
                response = JsonSerializer.Deserialize<LyricsSearchExactResponse>(responseJson);
                if (response is null || response.Lyrics.Length == 0)
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }

            var cleanLyrics = Compiled(@"[\r\n]{2,}").Replace($"{response.Lyrics}", "\n").Replace("\n", Environment.NewLine);
            return cleanLyrics;
        }

        private (string Author, string Title) GetSongInfo(string trackAuthor, string trackTitle)
        {
            var split = trackTitle.Split('-');

            if (split.Length is 1)
                return (trackAuthor, trackTitle);

            var author = split[0];
            var title = split[1];
            var regex = Compiled(@"(ft).\s+\w+|\(.*?\)|(lyrics)");

            while (regex.IsMatch(title))
                title = regex.Replace(title, string.Empty);

            var (returnAuthor, returnTitle) = author switch
            {
                "" or null => (trackAuthor, title),
                var _ when string.Equals(author, trackAuthor, StringComparison.OrdinalIgnoreCase) => (trackAuthor, title),
                _ => (author, title),
            };

            return (returnAuthor!.ReplaceCountrySpecialChars(), returnTitle!.ReplaceCountrySpecialChars());
        }
    }
}
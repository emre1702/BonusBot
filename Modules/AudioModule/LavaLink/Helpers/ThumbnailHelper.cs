using BonusBot.AudioModule.LavaLink.Helpers;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.LavaLink.Models.Thumbnails;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Extensions
{
    internal class ThumbnailHelper
    {
        public static ThumbnailHelper Instance => _lazy.Value;

        private static readonly Lazy<ThumbnailHelper> _lazy = new(() => new(), true);

        private ThumbnailHelper()
        {
        }

        public async Task<string?> FetchThumbnail(LavaLinkTrack track)
        {
            try
            {
                switch ($"{track.Info.Uri}".ToLower())
                {
                    case var yt when yt.Contains("youtube"):
                        return $"https://img.youtube.com/vi/{track.Info.Id}/maxresdefault.jpg";

                    // Doesn't work anymore - check https://dev.twitch.tv/docs/api to implement new logic
                    /*case var twich when twich.Contains("twitch"):
                        url = $"https://api.twitch.tv/v4/oembed?url={track.Info.Uri}";
                        break;*/

                    case var sc when sc.Contains("soundcloud"):
                        return await GetSoundcloudThumbnailUrl(track);

                    case var vim when vim.Contains("vimeo"):
                        return await GetVimeoThumbnailUrl(track);
                }
            }
            catch
            {
            }
            return string.Empty;
        }

        private async Task<string?> GetSoundcloudThumbnailUrl(LavaLinkTrack track)
        {
            var url = $"https://soundcloud.com/oembed?url={track.Info.Uri}&format=json";
            var req = await HttpHelper.Instance.GetString(new Uri(url));
            return JsonSerializer.Deserialize<SoundcloudOEmbedResponse>(req)?.ThumbnailUrl;
        }

        private async Task<string?> GetVimeoThumbnailUrl(LavaLinkTrack track)
        {
            var url = $"https://vimeo.com/api/oembed.json?url={track.Info.Uri}";
            var req = await HttpHelper.Instance.GetString(new Uri(url));
            return JsonSerializer.Deserialize<VimeoOEmbedResponse>(req)?.ThumbnailUrl;
        }
    }
}
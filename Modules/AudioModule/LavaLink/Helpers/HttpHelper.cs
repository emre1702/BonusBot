using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.LavaLink.Helpers
{
    public class HttpHelper
    {
        public static HttpHelper Instance => _lazy.Value;

        private HttpClient Client
        {
            get
            {
                if (_clientCache is { })
                    return _clientCache;
                _clientCache = CreateHttpClient();
                return _clientCache;
            }
            set => _clientCache = value;
        }

        private HttpClient? _clientCache;
        private static readonly Lazy<HttpHelper> _lazy = new(() => new(), true);

        private HttpHelper()
        {
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new HttpClientHandler
            {
                UseCookies = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "BonusBot");
            return client;
        }

        internal async Task<string> GetString(Uri url)
        {
            var get = await Client.GetAsync(url).ConfigureAwait(false);
            if (!get.IsSuccessStatusCode)
                return string.Empty;

            using var content = get.Content;
            var read = await content.ReadAsStringAsync().ConfigureAwait(false);
            return read;
        }

        internal HttpHelper WithCustomHeader(string key, string value)
        {
            if (Client.DefaultRequestHeaders.Contains(key))
                return this;

            Client.DefaultRequestHeaders.Add(key, value);
            return this;
        }
    }
}
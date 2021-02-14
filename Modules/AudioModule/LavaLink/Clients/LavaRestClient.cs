﻿using BonusBot.AudioModule.LavaLink.Helpers;
using BonusBot.AudioModule.LavaLink.Models;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.LavaLink.Clients
{
    internal class LavaRestClient
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _password;

        internal LavaRestClient(string host, int port, string password)
            => (_host, _port, _password) = (host, port, password);

        internal LavaRestClient(Configuration? configuration = null)
        {
            configuration ??= new Configuration();
            _host = configuration.Host;
            _port = configuration.Port;
            _password = configuration.Password;
        }

        public Task<SearchResult> SearchSoundcloud(string query, int limit = 20)
        {
            if (Uri.TryCreate(query, UriKind.Absolute, out Uri? uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                return SearchTracks(query, limit);
            return SearchTracks($"scsearch:{query}", limit);
        }

        public Task<SearchResult> SearchYouTube(string query, int limit = 20)
        {
            if (Uri.TryCreate(query, UriKind.Absolute, out Uri? uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                return SearchTracks(query, limit);
            return SearchTracks($"ytsearch:{query}", limit);
        }

        public async Task<SearchResult> SearchTracks(string query, int limit = 20)
        {
            var url = new Uri($"http://{_host}:{_port}/loadtracks?identifier={WebUtility.UrlEncode(query)}&max-results={limit}");
            var request = await HttpHelper.Instance
                .WithCustomHeader("Authorization", _password)
                .GetString(url)
                .ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<SearchResult>(request)!;
            result.Tracks = result.Tracks.Distinct();

            return result;
        }
    }
}
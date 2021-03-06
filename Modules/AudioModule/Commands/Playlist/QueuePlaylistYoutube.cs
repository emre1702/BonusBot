﻿using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Playlist
{
    internal class QueuePlaylistYouTube : QueuePlaylistBase
    {
        public QueuePlaylistYouTube(Main main) : base(main)
        {
        }

        public override async Task Do(PlaylistArgs args)
        {
            var searchResult = await Main.LavaRestClient.SearchSoundcloud(args.Query);
            await HandleSearchResult(searchResult, args.Limit);
        }
    }
}
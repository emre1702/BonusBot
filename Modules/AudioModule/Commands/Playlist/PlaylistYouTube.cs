﻿using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Playlist
{
    internal class PlaylistYouTube : PlaylistBase
    {
        public PlaylistYouTube(Main main) : base(main)
        {
        }

        public override async Task Do(PlaylistArgs args)
        {
            var searchResult = await Main.LavaRestClient.SearchYouTube(args.Query);
            await HandleSearchResult(searchResult, args.Limit);
        }
    }
}
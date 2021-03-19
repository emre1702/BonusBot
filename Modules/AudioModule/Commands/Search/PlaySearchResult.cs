using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System;
using System.Threading.Tasks;
using static BonusBot.AudioModule.Main;

namespace BonusBot.AudioModule.Commands.Search
{
    internal class PlaySearchResult : CommandHandlerBase<SearchGroup, PlaySearchCommandArgs>
    {
        public PlaySearchResult(SearchGroup searchGroup) : base(searchGroup)
        {
        }

        public override async Task Do(PlaySearchCommandArgs args)
        {
            var index = args.Number - 1;
            if (index >= Class.Player!.LastSearchResult!.Count)
            {
                await Class.ReplyAsync(ModuleTexts.SongAtThisSearchNumberNotExistsError);
                return;
            }

            var track = GetTrackBySearchIndex(index);
            await Play(track);
        }

        private AudioTrack GetTrackBySearchIndex(int index)
            => new(Class.Player!.LastSearchResult![index], Class.Context.GuildUser!);

        private async Task Play(AudioTrack track)
        {
            await Class.Player!.Play(track);
            var msg = string.Format(ModuleTexts.NowPlayingInfo, track);
            if (Class.Player.Status == PlayerStatus.Paused)
                msg += Environment.NewLine + ModuleTexts.PlayerIsPausedInfo;
            await Class.ReplyAsync(msg);
        }
    }
}
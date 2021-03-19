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
    internal class QueueSearchResult : CommandHandlerBase<SearchGroup, PlaySearchCommandArgs>
    {
        public QueueSearchResult(SearchGroup searchGroup) : base(searchGroup)
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
            if (Class.Player.CurrentTrack is null)
                await Play(track);
            else
                await Queue(track);
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

        private Task Queue(AudioTrack track)
        {
            Class.Player!.Queue.Enqueue(track);
            var msg = string.Format(ModuleTexts.TrackHasBeenEnqueuedInfo, track);
            if (Class.Player.Status == PlayerStatus.Paused)
                msg += Environment.NewLine + ModuleTexts.PlayerIsPausedInfo;
            return Class.ReplyAsync(msg);
        }
    }
}
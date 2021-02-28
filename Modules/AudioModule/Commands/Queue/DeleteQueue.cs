using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Queue
{
    internal class DeleteQueue : CommandHandlerBase<Main, NumberArgs>
    {
        public DeleteQueue(Main main) : base(main)
        {
        }

        public override async Task Do(NumberArgs args)
        {
            var queue = Class.Player!.Queue;
            var removedTrack = queue.RemoveAt(args.Number - 1);
            if (removedTrack is { })
            {
                var requestedByName = !string.IsNullOrEmpty(removedTrack.RequestedBy.Nickname) ? removedTrack.RequestedBy.Nickname : removedTrack.RequestedBy.Username;
                await Class.ReplyAsync(string.Format(ModuleTexts.TrackHasBeenRemovedFromQueueInfo, removedTrack, requestedByName));
            }
        }
    }
}
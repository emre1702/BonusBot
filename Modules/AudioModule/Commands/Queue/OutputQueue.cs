using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Queue
{
    internal class OutputQueue : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public OutputQueue(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            var queue = Class.Player!.Queue;
            await Class.ReplyAsync(queue.Count == 0 ? ModuleTexts.QueueEmptyInfo : queue.ToString());
        }
    }
}
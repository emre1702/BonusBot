using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Queue
{
    internal class Shuffle : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Shuffle(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            Class.Player!.Queue.IsShuffle = true;
            await Class.ReplyAsync(ModuleTexts.ShuffleInfo);
        }
    }
}
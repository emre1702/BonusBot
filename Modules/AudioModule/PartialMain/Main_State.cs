using BonusBot.AudioModule.Commands.State;
using Discord.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Command("GetState")]
        public Task GetState()
            => new GetState(this).Do(new());
    }
}

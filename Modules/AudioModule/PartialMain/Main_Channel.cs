using BonusBot.AudioModule.Commands.Channel;
using BonusBot.AudioModule.Preconditions;
using Discord.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Command("disconnect")]
        [Alias("leave")]
        [RequirePlayer(false)]
        public Task Leave()
           => new Leave(this).Do(new());

        [Command("join")]
        [Alias("come")]
        [RequirePlayer]
        public Task Join()
            => new Join(this).Do(new());
    }
}

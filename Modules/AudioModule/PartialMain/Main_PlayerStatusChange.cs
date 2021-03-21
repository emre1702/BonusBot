using BonusBot.AudioModule.Commands.PlayerStatusChange;
using BonusBot.AudioModule.Preconditions;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Command("resume")]
        [Alias("unpause")]
        [RequirePlayer(false)]
        public Task Resume(TimeSpan? delay = null)
               => new Resume(this).Do(new(delay));

        [Command("pause")]
        [Alias("unresume")]
        [RequirePlayer(false)]
        public Task Pause(TimeSpan? delay = null)
            => new Pause(this).Do(new(delay));

        [Command("stop")]
        [RequirePlayer(false)]
        public Task Stop(TimeSpan? delay = null)
            => new Stop(this).Do(new(delay));
    }
}

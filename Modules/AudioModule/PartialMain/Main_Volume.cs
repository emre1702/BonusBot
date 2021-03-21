using BonusBot.AudioModule.Commands.Channel;
using BonusBot.AudioModule.Commands.Volume;
using BonusBot.AudioModule.Preconditions;
using BonusBot.Common.Commands.Conditions;
using Discord.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Command("volume")]
        [Alias("SetVolume", "SetVol", "Vol", "Volum", "lautstärke")]
        public Task SetVolume([RequireNumberRange(0, 200)] int volume)
          => new SetVolume(this).Do(new(volume));

        [Command("volume")]
        [Alias("GetVolume", "GetVol", "Vol", "Volum", "lautstärke")]
        public Task GetVolume(bool plainOutput = false)
            => new GetVolume(this).Do(new(plainOutput));
    }
}




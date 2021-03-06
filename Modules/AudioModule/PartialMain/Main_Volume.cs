﻿using BonusBot.AudioModule.Commands.Volume;
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
        public Task GetVolume()
            => new GetVolume(this).Do(new());
    }
}




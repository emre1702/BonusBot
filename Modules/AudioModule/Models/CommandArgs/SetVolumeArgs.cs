﻿using BonusBot.Common.Commands;

namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record SetVolumeArgs(int Volume) : ICommandHandlerArgsBase;
}
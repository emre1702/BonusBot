using BonusBot.Common.Commands;
using Discord;
using System;

namespace BonusBot.AdminModule.Models.CommandArgs
{
    internal record BanArgs(IUser User, TimeSpan Time, string Reason, int PruneDays) : ICommandHandlerArgs;
}
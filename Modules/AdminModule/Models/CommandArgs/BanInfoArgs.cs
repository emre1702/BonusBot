using BonusBot.Common.Commands;
using Discord;

namespace BonusBot.AdminModule.Models.CommandArgs
{
    internal record BanInfoArgs(IUser User) : ICommandHandlerArgs;
}
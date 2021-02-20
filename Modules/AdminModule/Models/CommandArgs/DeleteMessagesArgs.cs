using BonusBot.Common.Commands;
using Discord;

namespace BonusBot.AdminModule.Models.CommandArgs
{
    internal record DeleteMessagesArgs(int Limit, IUser? User) : ICommandHandlerArgs;
}
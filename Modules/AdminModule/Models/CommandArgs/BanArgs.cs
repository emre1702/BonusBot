using BonusBot.Common.Commands;
using Discord.WebSocket;
using System;

namespace BonusBot.AdminModule.Models.CommandArgs
{
    internal record BanArgs(SocketGuildUser GuildUser, TimeSpan Time, string Reason) : ICommandHandlerArgs;
}
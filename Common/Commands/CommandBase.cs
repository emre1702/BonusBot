﻿using BonusBot.Common.Commands;
using BonusBot.Common.Commands.Exceptions;
using BonusBot.Common.Defaults;
using Discord;
using Discord.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Common.Extensions
{
    public class CommandBase : ModuleBase<CustomContext>
    {
        public Task<IUserMessage> ReplyAsync(EmbedBuilder embed)
        {
            if (!embed.Color.HasValue)
                embed.WithColor(171, 31, 242);
            return base.ReplyAsync(embed: embed.Build());
        }

        public Task<IUserMessage> ReplyAsync(string message)
        {
            return base.ReplyAsync(message);
        }

        public async Task<IUserMessage> ReplyToUserAsync(string msg)
        {
            var user = Context.SocketUser;
            var channel = await user.GetOrCreateDMChannelAsync();
            return await channel.SendMessageAsync(msg);
        }

        public async Task<IUserMessage> ReplyToUserAsync(EmbedBuilder embed)
        {
            if (!embed.Color.HasValue)
                embed.WithColor(171, 31, 242);
            var user = Context.SocketUser;
            var channel = await user.GetOrCreateDMChannelAsync();
            return await channel.SendMessageAsync(embed: embed.Build());
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            if (Context.BonusGuild?.Modules.Contains(GetType().Assembly) == false)
                throw new ModuleIsDisabledException();

            base.BeforeExecute(command);

            Thread.CurrentThread.CurrentUICulture = Context.BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
        }
    }
}
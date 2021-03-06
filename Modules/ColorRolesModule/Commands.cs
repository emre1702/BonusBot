﻿using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ColorRolesModule
{
    [RequireContext(ContextType.Guild)]
    [RequireNotDisabledInGuild(typeof(Commands))]
    public class Commands : CommandBase
    {
        [Command("color")]
        [Alias("farbe", "farben", "colour")]
        public async Task SetUserColor(Color color)
        {
            var role = Context.GuildUser!.Roles.FirstOrDefault(r => r.Name == GetRoleName());
            if (role is null)
                await AddUserRole(color);
            else
                await ModifyUserRole(role, color);
        }

        private async Task AddUserRole(Color color)
        {
            IRole? role = Context.Guild.Roles.FirstOrDefault(r => r.Name == GetRoleName());
            if (role is null)
                role = await Context.Guild.CreateRoleAsync(GetRoleName(), GuildPermissions.None, color, isMentionable: false);
            else
                await role.ModifyAsync(prop => prop.Color = color);
            await Context.GuildUser!.AddRoleAsync(role);
        }

        private Task ModifyUserRole(SocketRole role, Color color)
            => role.ModifyAsync(prop => prop.Color = color);

        private string GetRoleName()
            => $"Role {Context.User.Username}#{Context.User.Discriminator}";
    }
}
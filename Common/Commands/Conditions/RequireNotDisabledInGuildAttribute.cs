﻿using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Languages;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireNotDisabledInGuildAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (context.Guild is null) return Task.FromResult(PreconditionResult.FromSuccess());

            var guildsHandler = services.GetRequiredService<IGuildsHandler>();
            var bonusGuild = guildsHandler.GetGuild(context.Guild)!;

            return bonusGuild.Modules.Contains(command.Module)
                ? Task.FromResult(PreconditionResult.FromSuccess())
                : Task.FromResult(PreconditionResult.FromError(string.Format(Texts.ModuleIsDisabledError, command.Module.Name.ToModuleName())));
        }
    }
}

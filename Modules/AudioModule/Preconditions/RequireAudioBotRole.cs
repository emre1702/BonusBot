using BonusBot.AudioModule.Language;
using BonusBot.Common.Commands;
using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Languages;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Preconditions
{
    internal class RequireAudioBotRole : PreconditionAttribute
    {
        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var guildsHandler = services.GetRequiredService<IGuildsHandler>();
            var bonusGuild = guildsHandler.GetGuild(context.Guild);

            if (bonusGuild is null)
                return PreconditionResult.FromError(ModuleTexts.OnlyAllowedInGuildChat);

            var role = await bonusGuild.Settings.Get<IRole>(GetType().Assembly, Settings.AudioBotUserRoleId);
            if (role is null)
                return PreconditionResult.FromSuccess();

            var ctx = (CustomContext)context;
            Thread.CurrentThread.CurrentUICulture = ctx.BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            return ctx.User?.Roles.Any(r => r.Id == role.Id) == true
                ? PreconditionResult.FromSuccess()
                : PreconditionResult.FromError(string.Format(Texts.RoleRequiredError, role.Name));
        }
    }
}
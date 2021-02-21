using Discord.Commands;
using BonusBot.Common.Extensions;
using BonusBot.Common.Languages;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BonusBot.Common.Interfaces.Guilds;
using Discord;
using System.Threading;
using BonusBot.Common.Defaults;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireEmoteSettingAttribute : RequireSettingAttribute
    {
        public RequireEmoteSettingAttribute(string settingKey) : base(settingKey)
        {
        }

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var result = await base.CheckPermissionsAsync(context, command, services);
            if (result.Error == CommandError.UnmetPrecondition)
                return result;

            var guildsHandler = services.GetRequiredService<IGuildsHandler>();
            var bonusGuild = guildsHandler.GetGuild(context.Guild);

            Thread.CurrentThread.CurrentUICulture = ((CustomContext)context).BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            var emote = await bonusGuild!.Settings.Get<Emote>(command.Module.Name.ToModuleName(), SettingKey);
            if (emote is null)
                return PreconditionResult.FromError(string.Format(Texts.SettingEmoteDoesNotExist, SettingKey, command.Module.Name.ToModuleName()));

            return PreconditionResult.FromSuccess();
        }
    }
}
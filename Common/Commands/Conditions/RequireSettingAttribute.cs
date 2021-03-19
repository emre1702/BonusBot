using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Languages;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireSettingAttribute : PreconditionAttribute
    {
        public string SettingKey { get; }

        public RequireSettingAttribute(string settingKey) => (SettingKey) = (settingKey);

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            Thread.CurrentThread.CurrentUICulture = ((ICustomCommandContext)context).BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            var guildsHandler = services.GetRequiredService<IGuildsHandler>();
            var guild = guildsHandler.GetGuild(context.Guild);
            if (guild is null)
                return PreconditionResult.FromError(Texts.GuildNotInitializedYet);

            var setting = await guild.Settings.Get<object>(command.Module.Name.ToModuleName(), SettingKey);
            if (setting is null)
                return PreconditionResult.FromError(string.Format(Texts.SettingMissingError, SettingKey, command.Module.Name.ToModuleName()));

            return PreconditionResult.FromSuccess();
        }
    }
}
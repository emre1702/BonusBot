using Discord.Commands;
using BonusBot.Common.Extensions;
using BonusBot.Common.Languages;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using BonusBot.Common.Interfaces.Guilds;
using System.Threading;
using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Commands;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireTextChannelSettingAttribute : RequireSettingAttribute
    {
        public RequireTextChannelSettingAttribute(string settingKey) : base(settingKey)
        {
        }

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext cmdContext, CommandInfo command, IServiceProvider services)
        {
            var result = await base.CheckPermissionsAsync(cmdContext, command, services);
            if (result.Error == CommandError.UnmetPrecondition)
                return result;

            var context = (ICustomCommandContext)cmdContext;
            var guildsHandler = services.GetRequiredService<IGuildsHandler>();
            var bonusGuild = guildsHandler.GetGuild(context.Guild);

            var channel = await bonusGuild!.Settings.Get<SocketTextChannel>(command.Module.Name.ToModuleName(), SettingKey);
            Thread.CurrentThread.CurrentUICulture = context.BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            if (channel is null)
                return PreconditionResult.FromError(string.Format(Texts.SettingChannelDoesNotExist, SettingKey, command.Module.Name.ToModuleName()));

            return PreconditionResult.FromSuccess();
        }
    }
}
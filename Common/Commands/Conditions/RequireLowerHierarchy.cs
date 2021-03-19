using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Languages;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireLowerHierarchy : ParameterPreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, ParameterInfo parameter, object value, IServiceProvider services)
        {
            var ctx = (ICustomCommandContext)context;
            var target = (SocketGuildUser)value;

            Thread.CurrentThread.CurrentUICulture = ctx.BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            return ctx.GuildUser!.Hierarchy > target.Hierarchy
                ? Task.FromResult(PreconditionResult.FromSuccess())
                : Task.FromResult(PreconditionResult.FromError(string.Format(Texts.TargetIsHigherInHierarchyError, target.Nickname)));
        }
    }
}
using BonusBot.Common.Languages;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireLowerHierarchy : ParameterPreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, ParameterInfo parameter, object value, IServiceProvider services)
        {
            var ctx = (CustomContext)context;
            var target = (SocketGuildUser)value;

            return ctx.User!.Hierarchy > target.Hierarchy
                ? Task.FromResult(PreconditionResult.FromSuccess())
                : Task.FromResult(PreconditionResult.FromError(string.Format(Texts.TargetIsHigherInHierarchyError, target.Nickname)));
        }
    }
}
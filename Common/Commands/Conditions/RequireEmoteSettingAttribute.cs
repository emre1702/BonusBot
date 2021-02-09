using Discord.Commands;
using BonusBot.Common.Extensions;
using BonusBot.Common.Languages;
using System;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireEmoteSettingAttribute : RequireSettingAttribute
    {
        public RequireEmoteSettingAttribute(string settingKey) : base(settingKey, typeof(ulong))
        {
        }

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext cmdContext, CommandInfo command, IServiceProvider services)
        {
            var result = await base.CheckPermissionsAsync(cmdContext, command, services);
            if (result.Error == CommandError.UnmetPrecondition)
                return result;

            var context = (CustomContext)cmdContext;
            var channelId = context.GetRequiredSettingValue<ulong>(SettingKey);
            var emote = await context.Guild.GetEmoteAsync(channelId);

            if (emote is null)
                return PreconditionResult.FromError(string.Format(Texts.SettingChannelDoesNotExist, SettingKey, command.Module.Name.ToModuleName(), channelId));

            context.RequiredSettingValues[SettingKey] = emote;
            return PreconditionResult.FromSuccess();
        }
    }
}
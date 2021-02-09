using Discord.Commands;
using BonusBot.Common.Extensions;
using BonusBot.Common.Languages;
using BonusBot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireSettingAttribute : PreconditionAttribute
    {
        public string SettingKey { get; }
        private readonly Type _type;

        public RequireSettingAttribute(string settingKey, Type type) => (SettingKey, _type) = (settingKey, type);

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var dbContextFactory = services.GetRequiredService<FunDbContextFactory>();
            using var dbContext = dbContextFactory.CreateDbContext();

            var moduleName = command.Module.Name.ToModuleName();
            var setting = await dbContext.GuildsSettings.FirstOrDefaultAsync(s =>
                s.GuildId == context.Guild.Id &&
                EF.Functions.Like(s.Module, moduleName) &&
                EF.Functions.Like(s.Key, SettingKey));

            if (setting is null)
                return PreconditionResult.FromError(string.Format(Texts.SettingMissingError, SettingKey, command.Module.Name.ToModuleName()));

            try
            {
                var value = Convert.ChangeType(setting.Value, _type);

                if (context is CustomContext c)
                    c.RequiredSettingValues[setting.Key] = value;
                return PreconditionResult.FromSuccess();
            }
            catch
            {
                return PreconditionResult.FromError(string.Format(Texts.SettingInvalidError, SettingKey, command.Module.Name.ToModuleName()));
            }
        }
    }
}
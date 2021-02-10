using BonusBot.Common.Commands;
using BonusBot.Database;
using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BonusBot.Common.Extensions
{
    public class CommandBase : ModuleBase<CustomContext>
    {
        protected Task<IUserMessage> ReplyAsync(EmbedBuilder embed)
        {
            if (!embed.Color.HasValue)
                embed.WithColor(171, 31, 242);
            return base.ReplyAsync(embed: embed.Build());
        }

        protected async Task<IUserMessage> ReplyToUserAsync(string msg)
        {
            var user = Context.SocketUser;
            var channel = await user.GetOrCreateDMChannelAsync();
            return await channel.SendMessageAsync(msg);
        }

        protected async Task<IUserMessage> ReplyToUserAsync(EmbedBuilder embed)
        {
            if (!embed.Color.HasValue)
                embed.WithColor(171, 31, 242);
            var user = Context.SocketUser;
            var channel = await user.GetOrCreateDMChannelAsync();
            return await channel.SendMessageAsync(embed: embed.Build());
        }

        protected async Task<Emote?> GetSettingEmote(FunDbContextFactory dbContextFactory, IGuild guild, string settingKey)
        {
            var emoteId = await GetSetting<ulong>(dbContextFactory, guild, settingKey);
            if (emoteId == 0)
                return null;

            return await guild.GetEmoteAsync(emoteId);
        }

        protected async Task<T?> GetSetting<T>(FunDbContextFactory dbContextFactory, IGuild guild, string settingKey)
        {
            var moduleName = GetType().Assembly.GetName()!.Name!.ToModuleName();

            using var dbContext = dbContextFactory.CreateDbContext();
            var guildSetting = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(dbContext.GuildsSettings, s =>
                s.GuildId == guild.Id &&
                EF.Functions.Like(s.Module, moduleName) &&
                EF.Functions.Like(s.Key, settingKey));
            if (guildSetting is null) return default;

            var ret = Convert.ChangeType(guildSetting.Value, typeof(T));
            return ret is T ? (T)ret : default;
        }
    }
}
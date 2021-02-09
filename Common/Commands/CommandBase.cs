using Discord;
using Discord.Commands;
using Discord.Commands.Builders;
using BonusBot.Common.Commands;
using BonusBot.Common.Defaults;
using System.Globalization;
using System.Threading;
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
    }
}
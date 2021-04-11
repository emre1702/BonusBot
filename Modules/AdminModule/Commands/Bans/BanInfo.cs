using BonusBot.AdminModule.Languages;
using BonusBot.AdminModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using BonusBot.Database.Entities.Cases;
using Discord;
using System.Threading.Tasks;

namespace BonusBot.AdminModule.Commands.Bans
{
    internal class BanInfo : CommandHandlerBase<Main, BanInfoArgs>
    {
        public BanInfo(Main main) : base(main)
        {
        }

        public override async Task Do(BanInfoArgs args)
        {
            var ban = await Class.Context.Guild.GetBanAsync(args.User);
            if (ban is null)
            {
                await Class.ReplyErrorAsync(ModuleTexts.UserIsNotBannedError);
                return;
            }

            var unbanActionEntry = await GetUnbanActionEntry(args.User.Id);
            var embed = ToEmbedBuilder(ban, unbanActionEntry);
            await Class.ReplyAsync(embed);
        }

        private async Task<TimedActions?> GetUnbanActionEntry(ulong targetId)
        {
            await using var dbContext = Class.DbContextFactory.CreateDbContext();
            var unbanActionEntry = await dbContext.TimedActions.Get(Class.Context.Guild.Id, ActionType.Unban, targetId);
            return unbanActionEntry;
        }

        public EmbedBuilder ToEmbedBuilder(IBan discordBan, TimedActions? unbanAction)
        {
            var embedBuilder = new EmbedBuilder()
                .WithAuthor(discordBan.User)
                .WithColor(Color.Red)
                .WithDescription(discordBan.Reason)
                .WithFooter("Ban-Info");

            if (unbanAction is { })
            {
                embedBuilder.AddField(ModuleTexts.Added + ":", unbanAction.AddedDateTime, true);
                embedBuilder.AddField(ModuleTexts.Until + ":", unbanAction.AtDateTime, true);
            }
            return embedBuilder;
        }
    }
}
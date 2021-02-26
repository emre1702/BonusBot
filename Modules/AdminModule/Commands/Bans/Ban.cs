using BonusBot.AdminModule.Languages;
using BonusBot.AdminModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using BonusBot.Database.Entities.Cases;
using System;
using System.Threading.Tasks;

namespace BonusBot.AdminModule.Commands.Bans
{
    internal class Ban : CommandHandlerBase<Main, BanArgs>
    {
        public Ban(Main main) : base(main)
        {
        }

        public override async Task Do(BanArgs args)
        {
            using var dbContext = Class.DbContextFactory.CreateDbContext();
            var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                await RemovePreviousDbEntry(dbContext, args.User.Id);
                AddNewDbEntry(dbContext, args);
                await RemovePreviousDiscordBan(args);
                await AddNewDiscordBan(args);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await Class.ReplyToUserAsync(ModuleTexts.ErrorHappenedChangesReverted);
                await transaction.RollbackAsync();
                throw;
            }
            await InformAdminAboutBan(args);
            await new BanInfo(Class).Do(new(args.User));
        }

        private async Task RemovePreviousDbEntry(BonusDbContext dbContext, ulong targetId)
        {
            var unbanTask = await dbContext.TimedActions.Get(Class.Context.Guild.Id, ActionType.Unban, targetId);
            if (unbanTask is { })
                dbContext.Remove(unbanTask);
        }

        private void AddNewDbEntry(BonusDbContext dbContext, BanArgs args)
        {
            if (!NeedsUnbanLater(args)) return;

            var entry = new TimedActions
            {
                GuildId = Class.Context.Guild.Id,
                Module = GetType().Assembly.ToModuleName(),
                ActionType = ActionType.Unban,
                AtDateTime = DateTime.UtcNow + args.Time,
                SourceId = Class.Context.SocketUser.Id,
                TargetId = args.User.Id,
                MaxDelay = TimeSpan.MaxValue
            };
            dbContext.TimedActions.Add(entry);
        }

        private async Task RemovePreviousDiscordBan(BanArgs args)
        {
            try
            {
                await Class.Context.Guild.RemoveBanAsync(args.User);
            }
            catch
            {
                // Throws an exception if the user is not banned.
            }
        }

        private async Task AddNewDiscordBan(BanArgs args)
        {
            await InformTargetAboutHisBan(args);
            await Class.Context.Guild.AddBanAsync(args.User, args.PruneDays, args.Reason);
        }

        private async Task InformTargetAboutHisBan(BanArgs args)
        {
            try
            {
                var dmChannel = await args.User.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync(string.Format(ModuleTexts.YouHaveBeenBannedInfo,
                    args.Time,
                    DateTime.UtcNow + args.Time,
                    Class.Context.SocketUser.Mention,
                    Class.Context.SocketUser.Username + "#" + Class.Context.SocketUser.Discriminator,
                    args.Reason));
            }
            catch
            {
                // We don't care if he got the message or not.
            }
        }

        private async Task InformAdminAboutBan(BanArgs args)
        {
            await Class.ReplyAsync(string.Format(ModuleTexts.YouHaveBannedInfo,
                    args.Time,
                    DateTime.UtcNow + args.Time,
                    Class.Context.SocketUser.Mention,
                    Class.Context.SocketUser.Username + "#" + Class.Context.SocketUser.Discriminator));
        }

        private bool NeedsUnbanLater(BanArgs args)
            => args.Time != TimeSpan.MaxValue;
    }
}
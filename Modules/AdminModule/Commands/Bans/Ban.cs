﻿using BonusBot.AdminModule.Languages;
using BonusBot.AdminModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
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
            try
            {
                await Class.TimedActionsHandler.DoWithTransaction(async () =>
                {
                    await RemovePreviousDbEntry(args.User.Id);
                    await AddNewDbEntry(args);
                    await RemovePreviousDiscordBan(args);
                    await AddNewDiscordBan(args);

                    await Class.TimedActionsHandler.Save();
                },
                async () =>
                    await new BanInfo(Class).Do(new(args.User))
                );
            }
            catch
            {
                await Class.ReplyErrorToUserAsync(ModuleTexts.ErrorHappenedChangesReverted);
            }

            await InformAdminAboutBan(args);
        }

        private async ValueTask RemovePreviousDbEntry(ulong targetId)
        {
            var unbanTask = Class.TimedActionsHandler.Get(targetId, ActionType.Unban, GetType());
            if (unbanTask is { })
                await Class.TimedActionsHandler.Remove(unbanTask);
        }

        private Task AddNewDbEntry(BanArgs args)
        {
            if (!NeedsUnbanLater(args)) return Task.CompletedTask;

            var entry = new TimedActions
            {
                GuildId = Class.Context.Guild.Id,
                Module = GetType().Assembly.ToModuleName(),
                ActionType = ActionType.Unban,
                AtDateTime = DateTime.UtcNow + args.Time,
                SourceId = Class.Context.User.Id,
                TargetId = args.User.Id
            };
            return Class.TimedActionsHandler.Add(entry);
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
                var dmChannel = await args.User.CreateDMChannelAsync();
                await dmChannel.SendMessageAsync(string.Format(ModuleTexts.YouHaveBeenBannedInfo,
                    args.Time,
                    DateTime.UtcNow + args.Time,
                    Class.Context.User.Mention,
                    Class.Context.User.Username + "#" + Class.Context.User.Discriminator,
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
                    Class.Context.User.Mention,
                    Class.Context.User.Username + "#" + Class.Context.User.Discriminator));
        }

        private bool NeedsUnbanLater(BanArgs args)
            => args.Time != TimeSpan.MaxValue;
    }
}
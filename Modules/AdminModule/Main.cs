using BonusBot.AdminModule.Commands.Bans;
using BonusBot.AdminModule.Languages;
using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.Database;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BonusBot.AdminModule
{
    public class Main : CommandBase
    {
        internal BonusDbContextFactory DbContextFactory { get; private init; }

        [Command("ban")]
        [Alias("TBan", "TimeBan", "BanT", "BanTime", "PermaBan", "PermanentBan", "BanPerma", "BanPermanent", "PBan", "BanP",
            "RemoveBan", "BanRemove", "DeleteBan", "BanDelete", "UBan", "UnBan", "BanU")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public Task Ban(IUser target, TimeSpan time, string reason, int pruneDays = 0)
            => new Ban(this).Do(new(target, time, reason, pruneDays));

        [Command("baninfo")]
        [Alias("infoban", "banshow", "showban")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public Task BanInfo(IUser target)
           => new BanInfo(this).Do(new(target));

        public Main(BonusDbContextFactory bonusDbContextFactory)
            => (DbContextFactory) = (bonusDbContextFactory);

        protected override void BeforeExecute(CommandInfo command)
        {
            ModuleTexts.Culture = Context.BonusGuild.Settings.CultureInfo;

            base.BeforeExecute(command);
        }
    }
}
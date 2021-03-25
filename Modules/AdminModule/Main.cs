using BonusBot.AdminModule.Commands.Bans;
using BonusBot.AdminModule.Commands.Messages;
using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BonusBot.AdminModule
{
    [RequireContext(ContextType.Guild)]
    [RequireNotDisabledInGuild(typeof(Main))]
    public class Main : CommandBase
    {
        internal BonusDbContextFactory DbContextFactory { get; private init; }
        internal ITimedActionsHandler TimedActionsHandler { get; private init; }

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

        [Command("DeleteMessages")]
        [Alias("DeleteMessage", "DelMessages", "DelMessage", "MessagesDelete", "MessageDelete", "MessagesDel", "MessageDel", "DelMsg", "MsgDel", "DeleteMsg", "MsgDelete",
            "NachrichtLöschen", "NachrichtLösche", "LöscheNachricht", "LöschenNachricht", "DeleteNachricht", "DelNachricht")]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public Task DeleteMessages([RequireNumberRange(1, 1000)] int limit = 1, IUser? user = null)
            => new DeleteMessages(this).Do(new(limit, user));

        public Main(BonusDbContextFactory bonusDbContextFactory, ITimedActionsHandler timedActionsHandler)
            => (DbContextFactory, TimedActionsHandler) = (bonusDbContextFactory, timedActionsHandler);
    }
}
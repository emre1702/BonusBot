using BonusBot.AdminModule.Commands;
using BonusBot.Common.Extensions;
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BonusBot.AdminModule
{
    public class Main : CommandBase
    {
        [Command("ban")]
        [Alias("TBan", "TimeBan", "BanT", "BanTime", "PermaBan", "PermanentBan", "BanPerma", "BanPermanent", "PBan", "BanP",
            "RemoveBan", "BanRemove", "DeleteBan", "BanDelete", "UBan", "UnBan", "BanU")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public Task Ban(IUser user, TimeSpan time, [Remainder] string reason)
            => new Ban(this).Do(new(user, time, reason));
    }
}
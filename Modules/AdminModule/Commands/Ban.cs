using BonusBot.AdminModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AdminModule.Commands
{
    internal class Ban : CommandHandlerBase<Main, BanArgs>
    {
        public Ban(Main main) : base(main)
        {
        }

        public override Task Do(BanArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
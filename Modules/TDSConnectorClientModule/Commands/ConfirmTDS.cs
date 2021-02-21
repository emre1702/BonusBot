using BonusBot.Common.Commands;
using BonusBot.TDSConnectorClientModule.Languages;
using BonusBot.TDSConnectorClientModule.Models.CommandArgs;
using System.Threading.Tasks;

namespace BonusBot.TDSConnectorClientModule.Commands
{
    internal class ConfirmTDS : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public ConfirmTDS(Main c) : base(c)
        {
        }

        public override async Task Do(EmptyCommandArgs args)
        {
            var request = GetRequest(Class.Context.SocketUser.Id);
            var reply = (await Main.BBCommandClient.UsedCommandAsync(request)).Message;

            await Class.ReplyToUserAsync(reply ?? ModuleTexts.CommandSentInfo);
        }

        private UsedCommandRequest GetRequest(ulong userId)
            => new() { UserId = userId, Command = UsedCommandType.ConfirmTDS };
    }
}
using BonusBot.Common.Extensions;
using BonusBot.TDSConnectorClientModule.Commands;
using Discord.Commands;
using Discord.Commands.Builders;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using static BonusBot.TDSConnectorClientModule.BBCommand;

namespace BonusBot.TDSConnectorClientModule
{
    public class Main : CommandBase
    {
#nullable disable
        internal static BBCommandClient BBCommandClient { get; private set; }

#nullable restore

        [Command("ConfirmTDS")]
        [Alias("ConfirmIdentity", "ConfirmUserId")]
        public Task ConfirmUserId()
            => new ConfirmTDS(this).Do(new());

        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var grpcChannel = GrpcChannel.ForAddress("http://ragemp-server:5001", channelOptions: new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure
            });
            BBCommandClient = new(grpcChannel);
        }
    }
}
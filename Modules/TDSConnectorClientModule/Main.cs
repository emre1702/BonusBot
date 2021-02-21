using BonusBot.Common.Extensions;
using BonusBot.TDSConnectorClientModule.Commands;
using Discord.Commands;
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
        private static bool _initialized;

        [Command("ConfirmTDS")]
        [Alias("ConfirmIdentity", "ConfirmUserId")]
        public Task ConfirmUserId()
            => new ConfirmTDS(this).Do(new());

        public Main()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var grpcChannel = GrpcChannel.ForAddress("http://ragemp-server:5001", channelOptions: new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure
            });
            BBCommandClient = new(grpcChannel);
        }
    }
}
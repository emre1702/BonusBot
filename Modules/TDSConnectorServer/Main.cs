using BonusBot.TDSConnectorServerModule.Services;
using Discord.Commands;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BonusBot.TDSConnectorServerModule
{
    public class Main : ModuleBase<ICommandContext>
    {
        private bool _initialized;

        public Main(IServiceProvider serviceProvider)
        {
            Initialize(serviceProvider);
        }

        private void Initialize(IServiceProvider serviceProvider)
        {
            if (_initialized)
                return;

            _initialized = true;
            StartServer(serviceProvider);
        }

        private void StartServer(IServiceProvider serviceProvider)
        {
            var server = new Server
            {
                Services =
                {
                    MessageToChannel.BindService(ActivatorUtilities.CreateInstance<MessageToChannelService>(serviceProvider)),
                    MessageToUser.BindService(ActivatorUtilities.CreateInstance<MessageToUserService>(serviceProvider)),
                    RAGEServerStats.BindService(ActivatorUtilities.CreateInstance<RAGEServerStatsService>(serviceProvider)),
                    SupportRequest.BindService(ActivatorUtilities.CreateInstance<SupportRequestService>(serviceProvider)),
                },
                Ports = { new ServerPort("bonusbot", 5000, ServerCredentials.Insecure) }
            };
            server.Start();
        }
    }
}
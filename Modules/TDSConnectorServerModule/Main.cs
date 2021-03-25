using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Defaults;
using BonusBot.TDSConnectorServerModule.Services;
using Discord.Commands;
using Discord.Commands.Builders;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BonusBot.TDSConnectorServerModule
{
    [RequireNotDisabledInGuild(typeof(Main))]
    public class Main : ModuleBase<ICommandContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public Main(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);
            StartServer();
        }

        private void StartServer()
        {
            var host = Constants.IsDocker ? "bonusbot" : "localhost";
            var server = new Server
            {
                Services =
                {
                    MessageToChannel.BindService(ActivatorUtilities.CreateInstance<MessageToChannelService>(_serviceProvider)),
                    MessageToUser.BindService(ActivatorUtilities.CreateInstance<MessageToUserService>(_serviceProvider)),
                    RAGEServerStats.BindService(ActivatorUtilities.CreateInstance<RAGEServerStatsService>(_serviceProvider)),
                    SupportRequest.BindService(ActivatorUtilities.CreateInstance<SupportRequestService>(_serviceProvider)),
                },
                Ports = { new ServerPort(host, 5000, ServerCredentials.Insecure) }
            };
            server.Start();
        }
    }
}
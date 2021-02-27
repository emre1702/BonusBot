using BonusBot.Common;
using BonusBot.Common.Commands;
using BonusBot.Common.Commands.TypeReaders;
using BonusBot.Common.Defaults;
using BonusBot.Common.Events.Arguments;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Languages;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Services.DiscordNet
{
    internal class CommandsHandler
    {
        public CommandService CommandService { get; init; }

        private readonly IDiscordClientHandler _discordClientHandler;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGuildsHandler _guildsHandler;

        public CommandsHandler(IEventsHandler eventsHandler, IDiscordClientHandler discordClientHandler, IServiceProvider serviceProvider,
            IGuildsHandler guildsHandler)
        {
            _discordClientHandler = discordClientHandler;
            _serviceProvider = serviceProvider;
            _guildsHandler = guildsHandler;

            CommandService = CreateCommandService();
            AddTypeReaders(CommandService);

            eventsHandler.MessageCheck += CheckMessageIsCommand;
            eventsHandler.Message += ProcessCommandMessage;
            CommandService.CommandExecuted += CommandExecuted;
        }

        private CommandService CreateCommandService()
        {
            var commandServiceConfig = new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Warning
            };
            var commandService = new CommandService(commandServiceConfig);
            return commandService;
        }

        private void AddTypeReaders(CommandService commandService)
        {
            commandService.AddTypeReader<bool>(new BooleanTypeReader(), true);
            commandService.AddTypeReader<TimeSpan>(new TimeSpanTypeReader(), true);
            commandService.AddTypeReader<IUser>(new IUserTypeReader(), true);
            commandService.AddTypeReader<Color>(new ColorTypeReader(), true);
        }

        private async Task CheckMessageIsCommand(MessageData messageData)
        {
            if (messageData.Message.Author.IsBot || messageData.Message.Author.IsWebhook)
                return;
            if (messageData.Message is not SocketUserMessage message)
                return;

            var botClient = await _discordClientHandler.ClientSource.Task;
            if (!IsMessagePrivateChatCommand(message, botClient, out int prefixLength))
            {
                bool isGuildCommand;
                (isGuildCommand, prefixLength) = await IsMessageGuildChatCommand(message, botClient);
                if (!isGuildCommand)
                    return;
            }

            messageData.CommandPrefixLength = prefixLength;
            messageData.IsCommand = true;
            messageData.NeedsDelete = true;
        }

        private bool IsMessagePrivateChatCommand(SocketUserMessage message, DiscordSocketClient botClient, out int prefixLength)
        {
            if (message.Channel is not IPrivateChannel)
            {
                prefixLength = 0;
                return false;
            }

            if (!GetIsCommand(message, Constants.DefaultCommandPrefix, Constants.DefaultCommandMentionAllowed, botClient, out prefixLength))
                return false;

            return true;
        }

        private async Task<(bool IsCommand, int PrefixLength)> IsMessageGuildChatCommand(SocketUserMessage message, DiscordSocketClient botClient)
        {
            if (message.Channel is not SocketGuildChannel channel)
                return (false, 0);
            var guild = _guildsHandler.GetGuild(channel.Guild);
            if (guild is null)
                return (false, 0);

            var moduleName = GetType().Assembly.ToModuleName();
            var commandPrefix = await guild.Settings.Get<string>(moduleName, CommonSettings.CommandPrefix);
            bool? commandMentionAllowed = await guild.Settings.Get<bool>(moduleName, CommonSettings.CommandPrefixMentionAllowed);

            if (!GetIsCommand(message, commandPrefix ?? Constants.DefaultCommandPrefix, commandMentionAllowed ?? Constants.DefaultCommandMentionAllowed, botClient, out int prefixLength))
                return (false, 0);

            return (true, prefixLength);
        }

        private bool GetIsCommand(SocketUserMessage message, string prefix, bool mentionAllowed, DiscordSocketClient botClient, out int prefixLength)
        {
            prefixLength = 0;
            return message.HasStringPrefix(prefix, ref prefixLength) || mentionAllowed && message.HasMentionPrefix(botClient.CurrentUser, ref prefixLength);
        }

        private async Task ProcessCommandMessage(MessageData messageData)
        {
            if (!messageData.IsCommand)
                return;

            var botClient = await _discordClientHandler.ClientSource.Task;
            var context = new CustomContext(botClient, messageData, _guildsHandler);
            await CommandService.ExecuteAsync(context, messageData.CommandPrefixLength, _serviceProvider, MultiMatchHandling.Best);
        }

        private async Task CommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                Thread.CurrentThread.CurrentUICulture = ((CustomContext)context).BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
                await context.User.SendMessageAsync(
                    string.Format(Texts.CommandExecutedError, result.Error, result.ErrorReason, context.Message.Content));
            }

            if (context is CustomContext ctx && ctx.MessageData.NeedsDelete)
                await context.Message.DeleteAsync();
        }
    }
}
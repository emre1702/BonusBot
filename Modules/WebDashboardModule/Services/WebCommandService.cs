﻿using BonusBot.Common.Interfaces.Core;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardModule.Discord;
using Discord;
using Discord.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IResult = Discord.Commands.IResult;

namespace BonusBot.WebDashboardModule.Services
{
    public class WebCommandService
    {
        private readonly ContextProvideService _contextProvideService;
        private readonly ICommandsHandler _commandsHandler;
        private readonly ICustomServiceProvider _mainServiceProvider;
        private Guid? _id;

        private readonly TaskCompletionSource<IResult> _resultWaitSource = new();

        public WebCommandService(ContextProvideService contextProvideService, ICommandsHandler commandsHandler, ICustomServiceProvider mainServiceProvider)
            => (_contextProvideService, _commandsHandler, _mainServiceProvider) = (contextProvideService, commandsHandler, mainServiceProvider);

        private Task CommandExecuted(Optional<CommandInfo> cmdInfo, ICommandContext context, IResult result)
        {
            if (context is WebCommandContext webContext && webContext.Id == _id)
            {
                _commandsHandler.CommandService.CommandExecuted -= CommandExecuted;
                _resultWaitSource.SetResult(result);
            }   
            return Task.CompletedTask;
        }

        public async Task<(List<string> Messages, List<string> Errors)> Execute(ISession session, string? guildId, string command)
        {
            try
            {
                _id = Guid.NewGuid();
                var context = await _contextProvideService.Get(session, guildId, _id.Value, command);

                _commandsHandler.CommandService.CommandExecuted += CommandExecuted;

                await _commandsHandler.CommandService.ExecuteAsync(context, 0, _mainServiceProvider, MultiMatchHandling.Best);
                var result = await WaitForResult();

                var messages = ((WebUser)context.User).Messages;
                var errors = ((WebUser)context.User).Errors;
                if (result is null)
                    errors.Add("Timeout");

                return (messages, errors);
            }
            catch (Exception ex)
            {
                return (new() { }, new() { ex.Message });
            }
        }

        private async Task<IResult?> WaitForResult()
        {
            using var timeoutToken = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(_resultWaitSource.Task, Task.Delay(TimeSpan.FromMinutes(5), timeoutToken.Token));
            if (completedTask == _resultWaitSource.Task)
            {
                timeoutToken.Cancel();
                return await _resultWaitSource.Task;
            }
            return null;
        }
    }
}

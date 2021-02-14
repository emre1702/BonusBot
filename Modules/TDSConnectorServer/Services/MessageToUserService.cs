using BonusBot.Services.DiscordNet;
using Discord;
using Discord.WebSocket;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BonusBot.TDSConnectorServerModule.Services
{
    public class MessageToUserService : MessageToUser.MessageToUserBase
    {
        private readonly SocketClientHandler _socketClientHandler;

        public MessageToUserService(SocketClientHandler socketClientHandler)
            => _socketClientHandler = socketClientHandler;

        public override async Task<MessageToUserRequestReply> Send(MessageToUserRequest request, ServerCallContext context)
        {
            try
            {
                var client = await _socketClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new MessageToUserRequestReply
                    {
                        ErrorMessage = $"The guild with Id {request.GuildId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        ErrorType = string.Empty
                    };

                SocketGuildUser? target = guild.GetUser(request.UserId);

                if (target is null)
                    return new MessageToUserRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var privateChat = await target.GetOrCreateDMChannelAsync();
                await privateChat.SendMessageAsync(request.Text);

                return new MessageToUserRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new MessageToUserRequestReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }

        public override async Task<MessageToUserRequestReply> SendEmbed(EmbedToUserRequest request, ServerCallContext context)
        {
            try
            {
                var client = await _socketClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new MessageToUserRequestReply
                    {
                        ErrorMessage = $"The guild with Id {request.GuildId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        ErrorType = string.Empty
                    };

                SocketGuildUser? target = guild.GetUser(request.UserId);
                if (target is null)
                    return new MessageToUserRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var embedBuilder = new EmbedBuilder();

                if (!string.IsNullOrEmpty(request.Title))
                    embedBuilder.WithTitle(request.Title);

                if (!string.IsNullOrEmpty(request.Author))
                    embedBuilder.WithAuthor(request.Author);

                embedBuilder.WithTimestamp(DateTimeOffset.UtcNow);

                foreach (var field in request.Fields)
                    embedBuilder.AddField(field.Name, field.Value);

                if (request.ColorR != -1 || request.ColorG != -1 || request.ColorB != -1)
                    embedBuilder.WithColor(request.ColorR != -1 ? request.ColorR : 255, request.ColorG != -1 ? request.ColorG : 255, request.ColorB != -1 ? request.ColorB : 255);

                var privateChat = await target.GetOrCreateDMChannelAsync();
                await privateChat.SendMessageAsync(embed: embedBuilder.Build());

                return new MessageToUserRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new MessageToUserRequestReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }
    }
}
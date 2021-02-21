using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using Discord;
using Discord.WebSocket;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace BonusBot.TDSConnectorServerModule.Services
{
    public class MessageToChannelService : MessageToChannel.MessageToChannelBase
    {
        private readonly IDiscordClientHandler _discordClientHandler;

        public MessageToChannelService(IDiscordClientHandler discordClientHandler)
            => _discordClientHandler = discordClientHandler;

        public override async Task<MessageToChannelRequestReply> Send(MessageToChannelRequest request, ServerCallContext context)
        {
            try
            {
                var client = await _discordClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new MessageToChannelRequestReply
                    {
                        ErrorMessage = $"The guild with Id {request.GuildId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        ErrorType = string.Empty
                    };

                if (guild.GetChannel(request.ChannelId) is not SocketTextChannel channel)
                    return new MessageToChannelRequestReply
                    {
                        ErrorMessage = $"The channel with Id {request.ChannelId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        ErrorType = string.Empty
                    };

                int maxSize = DiscordConfig.MaxMessageSize - 50;    // 50 just to be sure
                var texts = request.Text.SplitByLength(maxSize);

                foreach (var text in texts)
                {
                    await channel.SendMessageAsync(text);
                }

                return new MessageToChannelRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new MessageToChannelRequestReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }

        public override async Task<MessageToChannelRequestReply> SendEmbed(EmbedToChannelRequest request, ServerCallContext context)
        {
            try
            {
                var client = await _discordClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new MessageToChannelRequestReply
                    {
                        ErrorMessage = $"The guild with Id {request.GuildId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        ErrorType = string.Empty
                    };

                if (guild.GetChannel(request.ChannelId) is not SocketTextChannel channel)
                    return new MessageToChannelRequestReply
                    {
                        ErrorMessage = $"The channel with Id {request.ChannelId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
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

                await channel.SendMessageAsync(embed: embedBuilder.Build());

                return new MessageToChannelRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new MessageToChannelRequestReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }
    }
}
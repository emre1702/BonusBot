using BonusBot.Common.Interfaces.Services;
using Discord;
using Discord.Net;
using Discord.Rest;
using Discord.WebSocket;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace BonusBot.TDSConnectorServerModule.Services
{
    public class RAGEServerStatsService : RAGEServerStats.RAGEServerStatsBase
    {
        private static Timer? _checkServerOfflineTimer;
        private static readonly RequestOptions _requestOptions = new RequestOptions { RetryMode = RetryMode.AlwaysFail, Timeout = 5000 };

        private readonly IDiscordClientHandler _discordClientHandler;

        public RAGEServerStatsService(IDiscordClientHandler discordClientHandler)
            => _discordClientHandler = discordClientHandler;

        public override async Task<RAGEServerStatsRequestReply> Send(RAGEServerStatsRequest request, ServerCallContext context)
        {
            try
            {
                if (_checkServerOfflineTimer?.Enabled == true)
                {
                    _checkServerOfflineTimer.Stop();
                    _checkServerOfflineTimer = null;
                }

                var client = await _discordClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new RAGEServerStatsRequestReply
                    {
                        ErrorMessage = $"The guild with Id {request.GuildId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        ErrorType = string.Empty
                    };

                if (guild.GetChannel(request.ChannelId) is not SocketTextChannel channel)
                    return new RAGEServerStatsRequestReply
                    {
                        ErrorMessage = $"The channel with Id {request.ChannelId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        ErrorType = string.Empty
                    };

                await channel.ModifyAsync((properties) => properties.Name = "Server: Online", _requestOptions);

                var datetimeNow = DateTimeOffset.UtcNow;
                var embedBuilder = new EmbedBuilder()
                        .WithAuthor(request.ServerName)
                        .WithColor(Color.Green)
                        .WithTimestamp(datetimeNow)
                        .WithFooter(request.Version)
                        .WithFields(new List<EmbedFieldBuilder>
                        {
                            new EmbedFieldBuilder { IsInline = true, Name = "IP", Value = request.ServerAddress },
                            new EmbedFieldBuilder { IsInline = true, Name = "Port", Value = request.ServerPort },
                            new EmbedFieldBuilder { IsInline = true, Name = "Players online:", Value = request.PlayerAmountOnline },
                            new EmbedFieldBuilder { IsInline = true, Name = "Peak today:", Value = request.PlayerPeakToday },
                            new EmbedFieldBuilder { IsInline = true, Name = "In arena:", Value = request.PlayerAmountInArena },
                            new EmbedFieldBuilder { IsInline = true, Name = "In gang lobby", Value = request.PlayerAmountInGangLobby },
                            new EmbedFieldBuilder { IsInline = true, Name = "In custom lobby:", Value = request.PlayerAmountInCustomLobby },
                            new EmbedFieldBuilder { IsInline = true, Name = "In main menu:", Value = request.PlayerAmountInMainMenu },
                            new EmbedFieldBuilder { IsInline = false, Name = "URL:", Value = $"rage://v/connect?ip={request.ServerAddress}:{request.ServerPort}"},
                            new EmbedFieldBuilder { IsInline = true, Name = "Online since", Value = request.OnlineSince },
                        }); ;

                var msgList = await channel.GetMessagesAsync(1, _requestOptions).FlattenAsync();
                var msg = msgList.FirstOrDefault();
                if (msg is not RestUserMessage message)
                {
                    await channel.SendMessageAsync(embed: embedBuilder.Build(), options: _requestOptions);
                }
                else
                {
                    await message.ModifyAsync(properties =>
                    {
                        properties.Embed = embedBuilder.Build();
                        properties.Content = null;
                    }, _requestOptions);
                }

                _checkServerOfflineTimer = new Timer(request.RefreshFrequencySec * 1000 * 1.5);
                _checkServerOfflineTimer.Elapsed += async (_, __) =>
                {
                    await channel.ModifyAsync((properties) => properties.Name = "Server: Offline", _requestOptions);

                    var msgList = await channel.GetMessagesAsync(1, _requestOptions).FlattenAsync();
                    var msg = msgList.FirstOrDefault();

                    if (msg is RestUserMessage message)
                    {
                        await message.ModifyAsync(properties =>
                        {
                            properties.Embed = null;
                            properties.Content = "Last online: " + GetUniversalDateTimeString(datetimeNow);
                        }, _requestOptions);
                    }
                };
                _checkServerOfflineTimer.Start();

                return new RAGEServerStatsRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (TimeoutException)
            {
                return new RAGEServerStatsRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex) when (ex is HttpException or HttpRequestException)
            {
                return new RAGEServerStatsRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new RAGEServerStatsRequestReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }

        private string GetUniversalDateTimeString(DateTimeOffset dateTime)
        {
            var enUsCulture = CultureInfo.CreateSpecificCulture("en-US");
            return dateTime.ToString("f", enUsCulture) + " +00:00";
        }
    }
}
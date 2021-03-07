using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
using BonusBot.Common.Enums;
using BonusBot.Common.Helper;
using Discord;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.LavaLink
{
    internal static class LavaSocketEventsHandler
    {
        public static void AddEvents(this LavaSocketClient lavaSocketClient)
        {
            lavaSocketClient.Log += Log;
            lavaSocketClient.TrackStuck += TrackStuck;
            lavaSocketClient.TrackFinished += TrackFinished;
            lavaSocketClient.PlayerUpdated += PlayerUpdated;
            lavaSocketClient.SocketClosed += SocketClosed;
            lavaSocketClient.TrackException += TrackException;
        }

        private static Task Log(LogMessage log)
        {
            ConsoleHelper.Log(log.Severity, log.Source, log.Message, log.Exception);
            return Task.CompletedTask;
        }

        private static async Task TrackStuck((LavaPlayer Player, LavaLinkTrack Track, long TimeoutMs) data)
        {
            if (!data.Player.Queue.TryDequeue(out var nextTrack))
            {
                await (data.Player.TextChannel?.SendMessageAsync(string.Format(ModuleTexts.TrackStuckNoMoreItemsInQueueInfo, data.Track)) ?? Task.CompletedTask);
                return;
            }

            if (data.Player is { })
            {
                await data.Player.Play(nextTrack);
                if (data.Player.TextChannel is { })
                {
                    await data.Player.TextChannel.SendMessageAsync(string.Format(ModuleTexts.TrackStuckNextSongInfo, data.Track, data.TimeoutMs, nextTrack));
                }
            }
        }

        private static async Task TrackFinished((LavaPlayer Player, LavaLinkTrack? Track, TrackEndReason Reason) data)
        {
            if (!data.Player.Queue.TryDequeue(out var nextTrack))
            {
                await data.Player.SetCurrentTrack(null);
                await data.Player.SetStatus(PlayerStatus.Ended);
                await (data.Player.TextChannel?.SendMessageAsync(string.Format(ModuleTexts.FinishedPlayingNoMoreItemsInQueueInfo, data.Track?.ToString() ?? "-")) ?? Task.CompletedTask);
                return;
            }

            if (data.Player is { })
            {
                await data.Player.Play(nextTrack);
                /*if (data.Player.TextChannel is { })
                {
                    await data.Player.TextChannel.SendMessageAsync(string.Format(ModuleTexts.FinishedPlayingNowPlayingInfo, data.Track?.ToString() ?? "-", nextTrack));
                    //await player.TextChannel.SendMessageAsync(player.ObjectToString());
                }*/
            }
        }

        private static Task PlayerUpdated((LavaPlayer Player, AudioTrack? Track, TimeSpan Position) data)
        {
            ConsoleHelper.Log(LogSeverity.Debug, LogSource.AudioModule, $"Player updated for {data.Player.VoiceChannel.GuildId}: {data.Position}");
            return Task.CompletedTask;
        }

        private static Task SocketClosed((int Code, string Reason, bool ByRemote) data)
        {
            ConsoleHelper.Log(LogSeverity.Info, LogSource.AudioModule, $"LavaSocket closed. Code: {data.Code} | Reason: {data.Reason} | By remote: {data.ByRemote}");
            return Task.CompletedTask;
        }

        private static Task TrackException((LavaPlayer Player, LavaLinkTrack? FinishedTrack, string Error) data)
        {
            ConsoleHelper.Log(LogSeverity.Info, LogSource.AudioModule, $"Player {data.Player.VoiceChannel.GuildId} {data.Error} for {data.FinishedTrack?.Info.Title}");
            return Task.CompletedTask;
        }
    }
}
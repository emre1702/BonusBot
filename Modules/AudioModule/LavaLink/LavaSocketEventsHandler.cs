using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.AudioModule.LavaLink.Enums;
using BonusBot.AudioModule.LavaLink.Models;
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
            if (!data.Reason.ShouldPlayNext())
                return;

            if (!data.Player.Queue.TryDequeue(out var nextTrack))
            {
                await (data.Player.TextChannel?.SendMessageAsync(string.Format(ModuleTexts.FinishedPlayingNoMoreItemsInQueueInfo, data.Track?.ToString() ?? "-")) ?? Task.CompletedTask);
                return;
            }

            if (data.Player is { })
            {
                await data.Player.Play(nextTrack);
                if (data.Player.TextChannel is { })
                {
                    await data.Player.TextChannel.SendMessageAsync(string.Format(ModuleTexts.FinishedPlayingNowPlayingInfo, data.Track?.ToString() ?? "-", nextTrack));
                    //await player.TextChannel.SendMessageAsync(player.ObjectToString());
                }
            }
        }
    }
}
using BonusBot.AudioModule.Extensions;
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
            lavaSocketClient.TrackFinished += TrackFinished;
        }

        private static Task Log(LogMessage log)
        {
            ConsoleHelper.Log(log.Severity, log.Source, log.Message, log.Exception);
            return Task.CompletedTask;
        }

        private static async Task TrackFinished((LavaPlayer Player, LavaLinkTrack? Track, TrackEndReason Reason) data)
        {
            if (!data.Reason.ShouldPlayNext())
                return;

            if (!data.Player.Queue.TryDequeue(out var item) || item is not AudioTrack nextTrack)
            {
                await (data.Player.TextChannel?.SendMessageAsync($"There are no more items left in queue.") ?? Task.CompletedTask);
                return;
            }

            if (data.Player is { })
            {
                await data.Player.Play(nextTrack);
                if (data.Player.TextChannel is { })
                {
                    await data.Player.TextChannel.SendMessageAsync($"Finished playing: {data.Track}\nNow playing: {nextTrack}");
                    //await player.TextChannel.SendMessageAsync(player.ObjectToString());
                }
            }
        }
    }
}
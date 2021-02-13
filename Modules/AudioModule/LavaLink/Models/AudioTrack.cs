using Discord.WebSocket;
using System;

namespace BonusBot.AudioModule.LavaLink.Models
{
    internal class AudioTrack
    {
        public LavaLinkTrack Audio { get; set; }
        public SocketGuildUser RequestedBy { get; set; }
        public DateTime AddedTime { get; set; } = DateTime.Now;

        public AudioTrack(LavaLinkTrack audio, SocketGuildUser requestedBy, DateTime? addedTime = null)
            => (Audio, RequestedBy, AddedTime) = (audio, requestedBy, addedTime ?? AddedTime);

        public override string ToString() => Audio.ToString();
    }
}
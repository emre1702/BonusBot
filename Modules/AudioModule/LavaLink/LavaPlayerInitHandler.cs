﻿using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.Common.Extensions;
using BonusBot.Common.Helper;
using Discord;
using System.Threading.Tasks;
using BonusBot.Common.Interfaces.Guilds;

namespace BonusBot.AudioModule.LavaLink
{
    internal class LavaPlayerInitHandler
    {
        private readonly AudioInfoHandler _audioInfoHandler;

        public LavaPlayerInitHandler(IGuildsHandler guildsHandler)
        {
            _audioInfoHandler = new(guildsHandler);
        }

        public async Task<LavaPlayer> Create(IVoiceChannel voiceChannel, ITextChannel? textChannel = null, int defaultVolume = 100)
        {
            var player = await LavaSocketClient.Instance.Connect(voiceChannel, textChannel);
            if (player.CurrentVolume != defaultVolume)
                await player.SetVolume(defaultVolume);

            AddEvents(player);
            return player;
        }

        private void AddEvents(LavaPlayer player)
        {
            player.TrackChanged += track => _audioInfoHandler.TrackChanged(player, track);
            player.VolumeChanged += volume => _audioInfoHandler.VolumeChanged(player, volume);
            player.QueueChanged += () => _audioInfoHandler.QueueChanged(player, player.Queue).SafeFireAndForget(true, onException: (ex) => ConsoleHelper.Log(LogSeverity.Error, "QueueChanged", ex.Message, ex));
            player.StatusChanged += data => _audioInfoHandler.StatusChanged(player, data.New);
        }
    }
}
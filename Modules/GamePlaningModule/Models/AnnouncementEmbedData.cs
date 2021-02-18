using Discord;
using System;
using System.Collections.Generic;

namespace BonusBot.GamePlaningModule.Models
{
    public class AnnouncementEmbedData
    {
        public string Game { get; set; }
        public string DateTimeStr { get; set; }

        public string ParticipantsString { get; set; }
        public int AmountParticipants { get; set; }
        public string ParticipationEmoteString { get; set; }

        public string LateParticipantsString { get; set; }
        public int AmountLateParticipants { get; set; }
        public string LateParticipationEmoteString { get; set; }

        public string MaybeParticipantsString { get; set; }
        public int AmountMaybeParticipants { get; set; }
        public string MaybeParticipationEmoteString { get; set; }

        public string CancellationsString { get; set; }
        public int AmountCancellations { get; set; }
        public string CancellationEmoteString { get; set; }

        public AnnouncementEmbedData(string game, string dateTimeStr,
            EmoteData participationData,
            EmoteData lateParticipationData,
            EmoteData maybeData,
            EmoteData cancellationData)
        {
            Game = game;
            DateTimeStr = dateTimeStr;

            ParticipantsString = string.Join(", ", participationData.Reactors);
            ParticipantsString = !string.IsNullOrWhiteSpace(ParticipantsString) ? ParticipantsString : "-";
            AmountParticipants = participationData.Reactors.Count;
            ParticipationEmoteString = participationData.Emote?.ToString() ?? string.Empty;

            LateParticipantsString = string.Join(", ", lateParticipationData.Reactors);
            LateParticipantsString = !string.IsNullOrWhiteSpace(LateParticipantsString) ? LateParticipantsString : "-";
            AmountLateParticipants = lateParticipationData.Reactors.Count;
            LateParticipationEmoteString = lateParticipationData.Emote?.ToString() ?? string.Empty;

            MaybeParticipantsString = string.Join(", ", maybeData.Reactors);
            MaybeParticipantsString = !string.IsNullOrWhiteSpace(MaybeParticipantsString) ? MaybeParticipantsString : "-";
            AmountMaybeParticipants = maybeData.Reactors.Count;
            MaybeParticipationEmoteString = maybeData.Emote?.ToString() ?? string.Empty;

            CancellationsString = string.Join(", ", cancellationData.Reactors);
            CancellationsString = !string.IsNullOrWhiteSpace(CancellationsString) ? CancellationsString : "-";
            AmountCancellations = cancellationData.Reactors.Count;
            CancellationEmoteString = cancellationData.Emote?.ToString() ?? string.Empty;
        }
    }
}
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

        public string CancellationsString { get; set; }
        public int AmountCancellations { get; set; }
        public string CancellationEmoteString { get; set; }

        public AnnouncementEmbedData(string game, string dateTimeStr,
            List<string> participantNames, IEmote? participationEmote,
            List<string> lateParticipantNames, IEmote? lateParticipationEmote,
            List<string> cancellationNames, IEmote? cancellationEmote)
        {
            Game = game;
            DateTimeStr = dateTimeStr;

            ParticipantsString = string.Join(", ", participantNames);
            ParticipantsString = !string.IsNullOrWhiteSpace(ParticipantsString) ? ParticipantsString : "-";
            AmountParticipants = participantNames.Count;
            ParticipationEmoteString = participationEmote?.ToString() ?? string.Empty;

            LateParticipantsString = string.Join(", ", lateParticipantNames);
            LateParticipantsString = !string.IsNullOrWhiteSpace(LateParticipantsString) ? LateParticipantsString : "-";
            AmountLateParticipants = lateParticipantNames.Count;
            LateParticipationEmoteString = lateParticipationEmote?.ToString() ?? string.Empty;

            CancellationsString = string.Join(", ", cancellationNames);
            CancellationsString = !string.IsNullOrWhiteSpace(CancellationsString) ? CancellationsString : "-";
            AmountCancellations = cancellationNames.Count;
            CancellationEmoteString = cancellationEmote?.ToString() ?? string.Empty;
        }
    }
}
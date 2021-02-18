using Discord;
using System.Collections.Generic;

namespace BonusBot.GamePlaningModule.Models
{
    public class EmoteData
    {
        public Emote? Emote { get; init; }
        public List<string> Reactors { get; init; }

        public EmoteData(Emote? emote, List<string> reactors)
            => (Emote, Reactors) = (emote, reactors);
    }
}
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Models.Statistics
{
    internal class Frames
    {
        [JsonPropertyName("sent")]
        public int Sent { get; private set; }

        /// <summary>
        /// Average frames nulled per minute.
        /// </summary>
        [JsonPropertyName("nulled")]
        public int Nulled { get; private set; }

        /// <summary>
        /// Average frames deficit per minute.
        /// </summary>
        [JsonPropertyName("deficit")]
        public int Deficit { get; private set; }
    }
}
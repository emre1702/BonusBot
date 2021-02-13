using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class EqualizerPayload : LavaPayload
    {
        [JsonPropertyName("bands")]
        public List<EqualizerBand> Bands { get; set; }

        public EqualizerPayload(ulong guildId, params EqualizerBand[] bands) : base(guildId, "equalizer")
        {
            if (bands.Any(x => x.Gain > 1.0 || x.Gain < -0.25 || x.Band > 14))
                throw new ArgumentOutOfRangeException(nameof(bands), ModuleTexts.EqualizerParamsError);

            Bands = new List<EqualizerBand>(bands);
        }

        public EqualizerPayload(ulong guildId, List<EqualizerBand> bands) : base(guildId, "equalizer")
        {
            if (bands.Any(x => x.Gain > 1.0 || x.Gain < -0.25 || x.Band > 14))
                throw new ArgumentOutOfRangeException(nameof(bands), ModuleTexts.EqualizerParamsError);

            Bands = bands;
        }
    }
}
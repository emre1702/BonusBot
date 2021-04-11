using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Track
{
    internal class SetPosition : CommandHandlerBase<Main, PositionArgs>
    {
        public SetPosition(Main main) : base(main)
        {
        }

        public override async Task Do(PositionArgs args)
        {
            var trackLength = Class.Player!.CurrentTrack!.Audio.Info.Length;
            var pos = GetPosByInput(args.Position);
            if (pos is null)
            {
                await Class.ReplyErrorAsync(ModuleTexts.SetPositionWrongFormatError);
                return;
            }

            var chosenPercentage = Math.Round(pos.Value.Divide(trackLength) * 100 * 100) / 100;
            if (pos > trackLength)
            {
                await Class.ReplyErrorAsync(string.Format(ModuleTexts.PositionLargeThanLengthError, pos, chosenPercentage));
                return;
            }

            await Class.Player.Seek(pos.Value);
            await Class.ReplyAsync(string.Format(ModuleTexts.SetPositionInfo, pos, chosenPercentage));
        }

        private TimeSpan? GetPosByInput(string input)
        {
            if (TryGetPercentagePosByInput(input, out TimeSpan? timeSpan)
                || input.TryGetSeconds(out timeSpan)
                || input.TryGetMinutes(out timeSpan)
                || input.TryGetHours(out timeSpan)
                || TryGetTimePosByInput(input, out timeSpan))
                return timeSpan;
            return null;
        }

        private bool TryGetPercentagePosByInput(string input, out TimeSpan? timeSpan)
        {
            timeSpan = null;
            if (!input.EndsWith('%'))
                return false;
            input = input.Remove(input.Length - 1);
            if (!double.TryParse(input, out double number))
                return false;

            timeSpan = Class.Player!.CurrentTrack!.Audio.Info.Length.Multiply(number / 100);
            return true;
        }

        private bool TryGetTimePosByInput(string input, out TimeSpan? timeSpan)
        {
            timeSpan = null;
            if (!input.Contains(':'))
                return false;

            var splitted = input.Split(':');
            if (!int.TryParse(splitted[^1], out int seconds))
                return false;
            if (!int.TryParse(splitted[^2], out int minutes))
                return false;

            int hours = 0;
            if (splitted.Length > 2 && !int.TryParse(splitted[^3], out hours))
                return false;

            timeSpan = new TimeSpan(hours, minutes, seconds);
            return true;
        }
    }
}
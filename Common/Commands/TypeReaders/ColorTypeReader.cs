using BonusBot.Common.Languages;
using Discord.Commands;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class ColorTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            try
            {
                if (input.TrimEnd('0') == "#") return Task.FromResult(TypeReaderResult.FromSuccess(new Discord.Color(0, 0, 0)));

                if (TryGetFromHtml(input, out var color)
                    || TryGetFromRGB(input, out color)
                    || TryGetFromName(input, out color))
                    return Task.FromResult(TypeReaderResult.FromSuccess(color));
            }
            catch { }

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidColorError));
        }

        private bool TryGetFromHtml(string input, out Discord.Color? color)
        {
            color = null;
            if (!input.StartsWith("#"))
                return false;

            try
            {
                var wrongTypeColor = ColorTranslator.FromHtml(input);
                if (wrongTypeColor.IsEmpty)
                    return false;
                color = new(wrongTypeColor.R, wrongTypeColor.G, wrongTypeColor.B);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryGetFromRGB(string input, out Discord.Color? color)
        {
            color = null;
            try
            {
                input = input.Replace(", ", ",");
                var rgbArr = input.Split(',', ';', '|', ' ');
                if (rgbArr.Length != 3
                    || !byte.TryParse(rgbArr[0], out var r)
                    || !byte.TryParse(rgbArr[1], out var g)
                    || !byte.TryParse(rgbArr[2], out var b))
                    return false;
                color = new Discord.Color(r, g, b);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryGetFromName(string input, out Discord.Color? color)
        {
            color = null;
            try
            {
                var wrongTypeColor = Color.FromName(input);
                if (wrongTypeColor.IsEmpty) return false;

                color = new Discord.Color(wrongTypeColor.R, wrongTypeColor.G, wrongTypeColor.B);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
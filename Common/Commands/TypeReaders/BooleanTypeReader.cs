using Discord.Commands;
using BonusBot.Common.Languages;
using System;
using System.Threading.Tasks;
using System.Threading;
using BonusBot.Common.Defaults;

namespace BonusBot.Common.Commands.TypeReaders
{
    public class BooleanTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            if (IsYes(input))
                return Task.FromResult(TypeReaderResult.FromSuccess(true));
            if (IsNo(input))
                return Task.FromResult(TypeReaderResult.FromSuccess(false));

            Thread.CurrentThread.CurrentUICulture = ((CustomContext)context).BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, Texts.CommandInvalidBooleanError));
        }

        private bool IsYes(string input)
            => input.ToLower() switch
            {
                "true" or "yes" or "ja" or "si" or "evet" or "yeah" or "ye" or "1" => true,
                _ => false,
            };

        private bool IsNo(string input)
            => input.ToLower() switch
            {
                "false" or "no" or "nein" or "hayir" or "nope" or "na" or "0" => true,
                _ => false,
            };
    }
}
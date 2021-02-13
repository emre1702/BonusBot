﻿using BonusBot.Common.Languages;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BonusBot.Common.Commands.Conditions
{
    public class RequireNumberRangeAttribute : ParameterPreconditionAttribute
    {
        public int Min { get; }
        public int Max { get; }

        public RequireNumberRangeAttribute(int min = int.MinValue, int max = int.MaxValue)
        {
            Min = min;
            Max = max;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, ParameterInfo parameter, object value, IServiceProvider services)
        {
            var val = Convert.ToInt32(value);
            return Min >= 0 && val <= Max
                ? Task.FromResult(PreconditionResult.FromSuccess())
                : Task.FromResult(PreconditionResult.FromError(string.Format(Texts.NumberRangeError, parameter.Name, Min, Max)));
        }
    }
}
﻿using System;

namespace BonusBot.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string GetModuleName(this Type type)
            => type.Assembly.ToModuleName() ?? string.Empty;
    }
}
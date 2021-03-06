﻿using Discord;
using System;
using System.Globalization;

namespace BonusBot.Common.Helper
{
    public static class IdSelectHelper
    {
        public static string GetIdentifier(this object value)
            => value switch
            {
                IChannel channel => channel.Id.ToString(),
                IRole role => role.Id.ToString(),
                IUser user => user.Id.ToString(),
                IGuild guild => guild.Id.ToString(),
                Emote emote => emote.Id.ToString(),
                IMessage message => message.Id.ToString(),

                CultureInfo cultureInfo => cultureInfo.Name,

                string str => str,
                int signedInt => signedInt.ToString(),
                uint unsignedInt => unsignedInt.ToString(),
                long signedLong => signedLong.ToString(),
                ulong unsignedLong => unsignedLong.ToString(),
                bool boolean => boolean ? bool.TrueString : bool.FalseString,

                _ => throw new NotSupportedException($"GetIdentifier is not supported for type {value.GetType().Name}.")
            };
    }
}
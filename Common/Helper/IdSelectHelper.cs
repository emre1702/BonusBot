using Discord;
using System;

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

                string str => str,
                ulong unsignedLong => unsignedLong.ToString(),
                _ => throw new NotSupportedException($"GetIdentifier is not support for type {value.GetType().Name}.")
            };
    }
}
using Discord;

namespace BonusBot.Common.Helper
{
    public static class EmbedHelper
    {
        public static EmbedBuilder DefaultEmbed
            => new EmbedBuilder()
                .WithCurrentTimestamp();
    }
}
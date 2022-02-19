using BonusBot.Common.Interfaces.Discord;
using Discord;
using System.Threading.Tasks;

namespace BonusBot.Common.Extensions
{
    public static class IUserExtensions
    {
        public static async Task<IUserMessage> SendErrorMessage(this IUser user, string? message = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null, AllowedMentions? allowedMentions = null,
            MessageReference? messageReference = null, MessageComponent? components = null, ISticker[]? stickers = null, Embed[]? embeds = null)
        {
            var dmChannel = await user.CreateDMChannelAsync();
            if (dmChannel is IErrorChannel errorChannel)
                return await errorChannel.SendErrorMessageAsync(message ?? "?");
            else
                return await dmChannel.SendMessageAsync(message, isTTS, embed, options, allowedMentions, messageReference, components, stickers, embeds);
        }       
    }
}

﻿using BonusBot.Common.Commands.Conditions;
using BonusBot.Common.Defaults;
using BonusBot.Common.Interfaces.Commands;
using Discord;
using Discord.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Common.Extensions
{ 
    public class CommandBase : ModuleBase<ICustomCommandContext>
    {
        public Task<IUserMessage> ReplyAsync(EmbedBuilder embed)
        {
            if (!embed.Color.HasValue)
                embed.WithColor(171, 31, 242);
            return ReplyAsync(embed: embed.Build());
        }

        public new Task<IUserMessage> ReplyAsync(string? message = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null, AllowedMentions? allowedMentions = null, 
            MessageReference? messageReference = null, MessageComponent? components = null, ISticker[]? stickers = null, Embed[]? embeds = null)
            => Context.Channel.SendMessageAsync(message, isTTS, embed, options, allowedMentions, messageReference, components, stickers, embeds);

        public Task<IUserMessage> ReplyErrorAsync(string? message = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null, AllowedMentions? allowedMentions = null,
            MessageReference? messageReference = null, MessageComponent? components = null, ISticker[]? stickers = null, Embed[]? embeds = null)
            => Context.Channel.SendMessageAsync(message, isTTS, embed, options, allowedMentions, messageReference, components, stickers, embeds);

        public async Task<IUserMessage> ReplyToUserAsync(string? message = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null, AllowedMentions? allowedMentions = null,
            MessageReference? messageReference = null, MessageComponent? components = null, ISticker[]? stickers = null, Embed[]? embeds = null)
        {
            var channel = await Context.User.CreateDMChannelAsync();
            return await channel.SendMessageAsync(message, isTTS, embed, options, allowedMentions, messageReference, components, stickers, embeds);
        }

        public Task<IUserMessage> ReplyErrorToUserAsync(string? message = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null, AllowedMentions? allowedMentions = null,
            MessageReference? messageReference = null, MessageComponent? components = null, ISticker[]? stickers = null, Embed[]? embeds = null)
            => Context.User.SendErrorMessage(message, isTTS, embed, options, allowedMentions, messageReference, components, stickers, embeds);

        public async Task<IUserMessage> ReplyToUserAsync(EmbedBuilder embed)
        {
            if (!embed.Color.HasValue)
                embed.WithColor(171, 31, 242);
            var channel = await Context.User.CreateDMChannelAsync();
            return await channel.SendMessageAsync(embed: embed.Build());
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            base.BeforeExecute(command);

            Thread.CurrentThread.CurrentUICulture = Context.BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
        }
    }
}
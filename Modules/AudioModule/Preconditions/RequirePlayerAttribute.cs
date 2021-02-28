using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.Common.Commands;
using BonusBot.Common.Defaults;
using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Guilds;
using BonusBot.Common.Languages;
using BonusBot.Database;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Preconditions
{
    internal class RequirePlayerAttribute : PreconditionAttribute
    {
        private static LavaPlayerInitHandler? _lavaPlayerInitHandler;
        private readonly bool _createPlayerIfNotExists;

        public RequirePlayerAttribute(bool createPlayerIfNotExists = true)
            => _createPlayerIfNotExists = createPlayerIfNotExists;

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var ctx = (CustomContext)context;
            var lavaClient = LavaSocketClient.Instance;
            var player = lavaClient.GetPlayer(context.Guild.Id);

            Thread.CurrentThread.CurrentUICulture = ctx.BonusGuild?.Settings.CultureInfo ?? Constants.DefaultCultureInfo;
            if (player is null)
            {
                if (ctx.User is null)
                    return PreconditionResult.FromError(Texts.CommandOnlyAllowedInGuild);
                if (ctx.User.VoiceChannel is null)
                    return PreconditionResult.FromError(ModuleTexts.NotConnectToVoiceChannel);
                if (!_createPlayerIfNotExists)
                    return PreconditionResult.FromError(ModuleTexts.NoPlayerForGuildError);

                var guildsHandler = services.GetRequiredService<IGuildsHandler>();
                _lavaPlayerInitHandler = new(guildsHandler);

                var defaultVolume = await GetDefaultVolume(ctx.Guild.Id, guildsHandler);
                await _lavaPlayerInitHandler.Create(ctx.User.VoiceChannel, ctx.Channel as ITextChannel, defaultVolume);
            }
            else
            {
                if (ctx.Channel is ITextChannel textChannel)
                    player.MoveChannels(textChannel);
            }

            return PreconditionResult.FromSuccess();
        }

        private async Task<int> GetDefaultVolume(ulong guildId, IGuildsHandler guildsHandler)
        {
            var bonusGuild = guildsHandler.GetGuild(guildId)!;
            int? volume = await bonusGuild.Settings.Get<int>(GetType().Assembly, Settings.Volume);
            return volume ?? 100;
        }
    }
}
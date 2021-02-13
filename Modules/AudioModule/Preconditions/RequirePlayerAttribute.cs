using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.LavaLink;
using BonusBot.AudioModule.LavaLink.Clients;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using BonusBot.Common.Languages;
using BonusBot.Database;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Preconditions
{
    internal class RequirePlayerAttribute : PreconditionAttribute
    {
        private static LavaPlayerInitHandler? _lavaPlayerInitHandler;

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var ctx = (CustomContext)context;
            var lavaClient = LavaSocketClient.Instance;
            var player = lavaClient.GetPlayer(context.Guild.Id);

            if (player is null)
            {
                if (ctx.User is null)
                    return PreconditionResult.FromError(Texts.CommandOnlyAllowedInGuild);
                if (ctx.User.VoiceChannel is null)
                    return PreconditionResult.FromError(ModuleTexts.NotConnectToVoiceChannel);

                var bonusDbContextFactory = services.GetRequiredService<BonusDbContextFactory>();
                _lavaPlayerInitHandler = new(bonusDbContextFactory);

                var defaultVolume = await GetDefaultVolume(ctx.Guild.Id, bonusDbContextFactory);
                await _lavaPlayerInitHandler.Create(ctx.User.VoiceChannel, ctx.Channel as ITextChannel, defaultVolume);
            }

            return PreconditionResult.FromSuccess();
        }

        private async Task<int> GetDefaultVolume(ulong guildId, BonusDbContextFactory dbContextFactory)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var volume = await dbContext.GuildsSettings.GetInt32(guildId, Settings.Volume, GetType().Assembly);
            return volume ?? 100;
        }
    }
}
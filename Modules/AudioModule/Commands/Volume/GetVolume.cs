using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Volume
{
    internal class GetVolume : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public GetVolume(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            if (Class.Player is { })
            {
                await Class.ReplyAsync(string.Format(ModuleTexts.GetVolumeInfo, Class.Player.CurrentVolume));
                return;
            }

            using var dbContext = Class.DbContextFactory.CreateDbContext();
            var volume = await dbContext.GuildsSettings.GetInt32(Class.Context.Guild.Id, Settings.Volume, GetType().Assembly);
            if (volume is { })
            {
                await Class.ReplyAsync(string.Format(ModuleTexts.GetVolumeInfo, volume.Value));
                return;
            }

            await Class.ReplyAsync(ModuleTexts.NoVolumeSavedError);
        }
    }
}
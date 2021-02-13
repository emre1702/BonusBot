using BonusBot.AudioModule.Language;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Volume
{
    internal class GetVolume : CommandHandlerBase<Main, CommandHandlerArgsBase>
    {
        public GetVolume(Main main) : base(main)
        {
        }

        public override async Task Do(CommandHandlerArgsBase _)
        {
            if (Main.Player is { })
            {
                await Main.ReplyAsync(string.Format(ModuleTexts.GetVolumeInfo, Main.Player.CurrentVolume));
                return;
            }

            using var dbContext = Main.DbContextFactory.CreateDbContext();
            var volume = await dbContext.GuildsSettings.GetInt32(Main.Context.Guild.Id, Settings.Volume, GetType().Assembly);
            if (volume is { })
            {
                await Main.ReplyAsync(string.Format(ModuleTexts.GetVolumeInfo, volume.Value));
                return;
            }

            await Main.ReplyAsync(ModuleTexts.NoVolumeSavedError);
        }
    }
}
using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.Common.Commands;
using BonusBot.Common.Extensions;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Volume
{
    internal class SetVolume : CommandHandlerBase<Main, SetVolumeArgs>
    {
        public SetVolume(Main main) : base(main)
        {
        }

        public override async Task Do(SetVolumeArgs args)
        {
            await (Class.Player?.SetVolume(args.Volume) ?? Task.CompletedTask);

            using var dbContext = Class.DbContextFactory.CreateDbContext();
            await dbContext.GuildsSettings.AddOrUpdate(Class.Context.Guild.Id, Settings.Volume, GetType().Assembly, args.Volume.ToString());
            await dbContext.SaveChangesAsync();

            await Class.ReplyAsync(string.Format(ModuleTexts.SetVolumeInfo, args.Volume));
        }
    }
}
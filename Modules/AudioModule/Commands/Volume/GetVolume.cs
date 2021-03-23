using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
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

            int? volume = await Class.Context.BonusGuild!.Settings.Get<int>(GetType().Assembly, Settings.Volume);
            if (volume.HasValue)
            {
                await Class.ReplyAsync(string.Format(ModuleTexts.GetVolumeInfo, volume.Value));
                return;
            }

            await Class.ReplyAsync(ModuleTexts.NoVolumeSavedError);
        }
    }
}
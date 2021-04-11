using BonusBot.AudioModule.Helpers;
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
            var volume = await VolumeHelper.GetVolume(Class.Context.BonusGuild!);
            if (volume.HasValue)
            {
                await Class.ReplyAsync(string.Format(ModuleTexts.GetVolumeInfo, volume.Value));
                return;
            }

            await Class.ReplyErrorAsync(ModuleTexts.NoVolumeSavedError);
        }
    }
}
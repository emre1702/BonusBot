using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Volume
{
    internal class GetVolume : CommandHandlerBase<Main, GetVolumeCommandArgs>
    {
        public GetVolume(Main main) : base(main)
        {
        }

        public override async Task Do(GetVolumeCommandArgs args)
        {
            if (Class.Player is { })
            {
                await Class.ReplyAsync(args.PlainOutput ? Class.Player.CurrentVolume.ToString() : string.Format(ModuleTexts.GetVolumeInfo, Class.Player.CurrentVolume));
                return;
            }

            int? volume = await Class.Context.BonusGuild!.Settings.Get<int>(GetType().Assembly, Settings.Volume);
            if (volume.HasValue)
            {
                await Class.ReplyAsync(args.PlainOutput ? volume.ToString() : string.Format(ModuleTexts.GetVolumeInfo, volume.Value));
                return;
            }

            await Class.ReplyAsync(ModuleTexts.NoVolumeSavedError);
            if (args.PlainOutput)
                await Class.ReplyAsync("100");
        }
    }
}
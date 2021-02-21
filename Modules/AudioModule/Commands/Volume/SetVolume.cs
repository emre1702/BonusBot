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

            await Class.Context.BonusGuild!.Settings.Set(GetType().Assembly, Settings.Volume, args.Volume);

            await Class.ReplyAsync(string.Format(ModuleTexts.SetVolumeInfo, args.Volume));
        }
    }
}
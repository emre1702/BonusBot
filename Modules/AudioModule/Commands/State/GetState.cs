using BonusBot.AudioModule.Models;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.State
{
    internal class GetState : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public GetState(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            var volume = await GetVolume();
            var stateInfo = new StateInfo(volume);
            await Class.ReplyAsync(JsonSerializer.Serialize(stateInfo, new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        private async ValueTask<int> GetVolume()
        {
            if (Class.Player is { })
                return Class.Player.CurrentVolume;

            int? volume = await Class.Context.BonusGuild!.Settings.Get<int>(GetType().Assembly, Settings.Volume);
            if (volume.HasValue)
                return volume.Value;

            return 100;
        }
    }
}

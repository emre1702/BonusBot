using BonusBot.AudioModule.Language;
using BonusBot.AudioModule.Models.CommandArgs;
using BonusBot.AudioModule.PartialMain;
using BonusBot.Common.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Commands.Channel
{
    internal class Join : CommandHandlerBase<Main, EmptyCommandArgs>
    {
        public Join(Main main) : base(main)
        {
        }

        public override async Task Do(EmptyCommandArgs _)
        {
            var voiceChannel = Class.Context.GuildUser?.VoiceChannel;
            if (voiceChannel is null)
            {
                await Class.ReplyErrorAsync(ModuleTexts.NotConnectToVoiceChannel);
                return;
            }

            await Class.Player!.MoveChannels(voiceChannel, Class.Player.CachedState.IsSelfDeafened);
        }
    }
}
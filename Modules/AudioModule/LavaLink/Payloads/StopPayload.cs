namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class StopPayload : LavaPayload
    {
        public StopPayload(ulong guildId) : base(guildId, "stop")
        {
        }
    }
}
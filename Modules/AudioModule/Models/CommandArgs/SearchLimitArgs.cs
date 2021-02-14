namespace BonusBot.AudioModule.Models.CommandArgs
{
    internal record SearchLimitArgs(string Query, int Limit) : IQueryArgs;
}
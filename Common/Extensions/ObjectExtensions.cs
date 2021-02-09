namespace BonusBot.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T? CastTo<T>(this object obj)
            => obj is T val ? val : default;
    }
}
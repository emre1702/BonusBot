namespace BonusBot.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToModuleName(this string str)
            => str
                .Replace("BonusBot.", "")
                .Replace(".dll, ", "")
                .Replace("Module", "");
    }
}
using System.Reflection;

namespace BonusBot.Common.Extensions
{
    public static class AssemblyExtensions
    {
        public static string ToModuleName(this Assembly assembly)
            => assembly.GetName()!.Name!.ToModuleName();
    }
}
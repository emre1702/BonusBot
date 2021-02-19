using System;

namespace BonusBot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GuildSettingsContainerAttribute : Attribute
    {
    }
}
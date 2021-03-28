using System;

namespace BonusBot.Common.Attributes.Settings
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GuildSettingsContainerAttribute : Attribute
    {
    }
}
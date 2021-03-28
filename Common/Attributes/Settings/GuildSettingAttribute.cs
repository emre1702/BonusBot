using BonusBot.Common.Enums;
using System;
using System.Text.Json.Serialization;

namespace BonusBot.Common.Attributes.Settings
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GuildSettingAttribute : Attribute
    {
        public GuildSettingType Type { get; }
        public int? Min { get; } = 0;
        public int? Max { get; } = 255;
        public object? DefaultValue { get; set; }
        [JsonIgnore]
        public override object TypeId => base.TypeId;

        public GuildSettingAttribute(GuildSettingType type) 
            => Type = type;

        public GuildSettingAttribute(GuildSettingType type, int min, int max) 
            => (Type, Min, Max) = (type, min, max);
    }
}

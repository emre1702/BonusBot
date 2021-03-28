using BonusBot.Common.Attributes.Settings;

namespace BonusBot.Common.Models
{
    public record GuildSettingData(string ModuleName, string SettingName, GuildSettingAttribute Info)
    {
        public string Value { get; set; } = string.Empty;
    }
}

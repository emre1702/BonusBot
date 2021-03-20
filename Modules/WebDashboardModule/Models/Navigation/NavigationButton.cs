using System.Text.Json.Serialization;

namespace BonusBot.WebDashboardModule.Models.Navigation
{
    public class NavigationButton
    {
        [JsonPropertyName("icon")]
        public string Icon { get; init; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; init; } = string.Empty;
    }
}

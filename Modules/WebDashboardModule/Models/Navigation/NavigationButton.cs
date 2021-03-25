using System.Text.Json.Serialization;

namespace BonusBot.WebDashboardModule.Models.Navigation
{
    public class NavigationButton
    {
        public string Icon { get; init; } = string.Empty;

        public string Url { get; init; } = string.Empty;
    }
}

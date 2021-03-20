using System.Text.Json.Serialization;

namespace BonusBot.WebDashboardModule.Models.Discord
{
    public class WebGuildData
    {
        // Using string instead of ulong because of Angular not being able to handle such high values.
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("owner")]
        public bool IsOwner { get; init; }
    }
}

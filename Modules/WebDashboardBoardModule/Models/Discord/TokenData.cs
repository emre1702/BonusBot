using System.Text.Json.Serialization;

namespace BonusBot.WebDashboardBoardModule.Models.Discord
{
    public class TokenData
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; init; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; init; } = "Bearer";
        
        [JsonPropertyName("expires_in")]
        public int ExpiresInSeconds { get; init; }

        [JsonPropertyName("scope")]
        public string Scope { get; init; } = string.Empty;
    }
}

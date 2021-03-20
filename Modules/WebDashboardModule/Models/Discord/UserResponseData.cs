using BonusBot.WebDashboardModule.Converter;
using System.Text.Json.Serialization;

namespace BonusBot.WebDashboardModule.Models.Discord
{
    public class UserResponseData
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(UInt64JsonConverter))]
        public ulong Id { get; init; }

        [JsonPropertyName("username")]
        public string Username { get; init; } = string.Empty;

        [JsonPropertyName("discriminator")]
        public string Discriminator { get; init; } = string.Empty;

        [JsonPropertyName("avatar")]
        public string? Avatar { get; init; }

        [JsonPropertyName("bot")]
        public bool? IsBot { get; init; } = false;

        [JsonPropertyName("system")]
        public bool? IsSystem { get; init; } = false;

        [JsonPropertyName("mfa_enabled")]
        public bool? HasTwoFactorAuth { get; init; }

        [JsonPropertyName("locale")]
        public string Locale { get; init; } = "en-US";

        [JsonPropertyName("verified")]
        public bool? IsVerified { get; init; } // Will be null because email scope is not requested

        [JsonPropertyName("email")]
        public string? Email { get; init; }   // Will be null because email scope is not requested

        [JsonPropertyName("flags")]
        public int? Flags { get; init; }

        [JsonPropertyName("premium_type")]
        public int? PremiumType { get; init; }

        [JsonPropertyName("public_flags")]
        public int? PublicFlags { get; init; }
    }
}

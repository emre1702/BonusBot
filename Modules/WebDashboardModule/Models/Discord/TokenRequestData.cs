using BonusBot.WebDashboardModule.Defaults;
using System;
using System.Text.Json.Serialization;

namespace BonusBot.WebDashboardModule.Models.Discord
{
    public class TokenRequestData
    {
        [JsonPropertyName("client_id")]
        public string ClientId => Environment.GetEnvironmentVariable(WebEnvironmentKeys.BotClientId)!;

        [JsonPropertyName("client_secret")]
        public string ClientSecret => Environment.GetEnvironmentVariable(WebEnvironmentKeys.BotClientSecret)!;

        [JsonPropertyName("grant_type")]
        public string GrantType => "authorization_code";
        
        [JsonPropertyName("redirect_uri")]
        public string RedirectUri => WebConstants.OAuthTokenUrlRedirectUri;

        [JsonPropertyName("code")]
        public string Code { get; init; }

        /*[JsonPropertyName("scope")] 
        public string Scope => "identify,guilds";*/

        public TokenRequestData(string code) => Code = code;
    }
}

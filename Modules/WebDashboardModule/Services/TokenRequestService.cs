using BonusBot.WebDashboardModule.Defaults;
using BonusBot.WebDashboardModule.Models.Discord;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardModule.Services
{
    public class TokenRequestService
    {
        public async Task<TokenData> GetTokenData(string code)
        {
            Console.WriteLine("GetTokenData");
            using var httpClient = new HttpClient();
            var stringContent = new FormUrlEncodedContent(GetRequestData(code)!);
            var data = await httpClient.PostAsync(WebConstants.OAuthTokenUrl, stringContent);
            var dataContent = await data.Content.ReadAsStringAsync();
            if (data.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(dataContent);

            return JsonSerializer.Deserialize<TokenData>(dataContent)!;
        }

        private static Dictionary<string, string> GetRequestData(string code)
            => new()
            {
                ["client_id"] = Environment.GetEnvironmentVariable(WebEnvironmentKeys.BotClientId)!,
                ["client_secret"] = Environment.GetEnvironmentVariable(WebEnvironmentKeys.BotClientSecret)!,
                ["grant_type"] = "authorization_code",
                ["redirect_uri"] = WebConstants.OAuthTokenUrlRedirectUri,
                ["code"] = code,
                ["scope"] = "identify guilds"
            };
    }
}

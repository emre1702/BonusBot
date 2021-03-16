using BonusBot.WebDashboardBoardModule.Defaults;
using BonusBot.WebDashboardBoardModule.Models.Discord;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Services
{
    public class UserRequestService
    {
        public async Task<UserResponseData> GetUser(TokenData tokenData)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new(tokenData.TokenType, tokenData.AccessToken);

            var data = await httpClient.GetAsync(WebConstants.UserDataUrl);
            var dataContent = await data.Content.ReadAsStringAsync();
            if (data.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(dataContent);

            return JsonSerializer.Deserialize<UserResponseData>(dataContent)!;
        }
    }
}

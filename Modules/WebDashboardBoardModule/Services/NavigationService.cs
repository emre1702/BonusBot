using BonusBot.Common.Interfaces.Services;
using BonusBot.WebDashboardBoardModule.Defaults;
using BonusBot.WebDashboardBoardModule.Models.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BonusBot.WebDashboardBoardModule.Services
{
    public class NavigationService
    {
        private readonly IDiscordClientHandler _discordClientHandler;

        public NavigationService(IDiscordClientHandler discordClientHandler)
            => _discordClientHandler = discordClientHandler;

        public async Task<IEnumerable<WebGuildData>> GetGuilds(TokenData tokenData)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new(tokenData.TokenType, tokenData.AccessToken);

            var data = await httpClient.GetAsync(WebConstants.UserGuildsUrl);
            var dataContent = await data.Content.ReadAsStringAsync();
            if (data.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(dataContent);

            var discordClient = await _discordClientHandler.ClientSource.Task;
            var listOfGuilds = JsonSerializer.Deserialize<List<WebGuildData>>(dataContent)!;
            return listOfGuilds.Where(g => discordClient.Guilds.Any(botGuild => botGuild.Id.ToString() == g.Id));
        }
    }
}

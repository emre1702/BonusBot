namespace BonusBot.WebDashboardBoardModule.Defaults
{
    public static class WebConstants
    {
        public const string OAuthUrl = "https://discord.com/api/oauth2/authorize?client_id={0}&redirect_uri=http%3A%2F%2Fbonusbot.net%2FLoginRedirect&response_type=code&scope=identify%20guilds&state={1}";
        public const string OAuthTokenUrl = "https://discord.com/api/oauth2/token";
        public const string OAuthTokenUrlRedirectUri = "http://bonusbot.net/LoginRedirect";
        public const string UserDataUrl = "https://discord.com/api/users/@me";
        public const string UserGuildsUrl = "https://discord.com/api/users/@me/guilds";
    }
}

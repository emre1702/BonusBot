namespace BonusBot.WebDashboardModule.Defaults
{
    public static class WebConstants
    {
        public const string OAuthTokenUrlRedirectUri = "{0}/LoginRedirect";
        public const string OAuthUrl = "https://discord.com/api/oauth2/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope=identify%20guilds&state={2}";
        public const string OAuthTokenUrl = "https://discord.com/api/oauth2/token";
        public const string UserDataUrl = "https://discord.com/api/users/@me";
        public const string UserGuildsUrl = "https://discord.com/api/users/@me/guilds";
    }
}

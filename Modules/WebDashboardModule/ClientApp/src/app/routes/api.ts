export default {
    get: {
        command: {
            audioSettingsState: `WebCommand/AudioSettingsState`,
        },
        content: {
            accessLevel: `Content/UserAccessLevel`,
        },
        navigation: {
            guilds: `Navigation/Guilds`,
        },
        login: {
            oAuthUrl: `Login/OAuthUrl`,
        },
    },
    post: {
        command: {
            execute: `WebCommand/Execute`,
        },
        logout: {
            execute: `Logout/Execute`,
        },
    },
};

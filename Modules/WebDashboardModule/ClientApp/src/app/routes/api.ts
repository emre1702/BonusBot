export default {
    get: {
        color: {
            userColor: `Color/UserColor`,
        },
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
        settings: {
            allModuleDatas: `Settings/AllModuleDatas`,
            moduleSettings: `Settings/ModuleSettings`,
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

export default {
    get: {
        audio: {
            audioState: `Audio/GetState`,
        },
        color: {
            userColor: `Color/UserColor`,
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
            locale: `Settings/Locale`,
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

import { environment } from 'src/environments/environment';

const root = environment.apiBaseUrl;

export default {
    get: {
        content: {
            accessLevel: `${root}/Content/UserAccessLevel`,
        },
        navigation: {
            guilds: `${root}/Navigation/Guilds`,
        },
        login: {
            oAuthUrl: `${root}/Login/OAuthUrl`,
        },
    },
    post: {
        command: {
            execute: `${root}/WebCommand/Execute`,
        },
    },
};

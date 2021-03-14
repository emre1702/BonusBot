import { environment } from 'src/environments/environment';

const root = environment.apiBaseUrl;

export default {
    get: {
        httpService: {
            init: `${root}/Init/Init`,
        },
        login: {
            oAuthUrl: `${root}/Login/OAuthUrl`,
        },
    },
    post: {
        login: {
            signIn: `${root}/Login/SignIn`,
        },
    },
};

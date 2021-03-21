import { DefinePlugin } from 'webpack';

export const plugins = [
    new DefinePlugin({
        'process.env': {
            WEB_BASE_URL: process.env.WEB_BASE_URL,
        },
    }),
];

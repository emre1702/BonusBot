const webpack = require('webpack');

module.exports = {
    plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                WEB_BASE_URL: JSON.stringify(process.env.WEB_BASE_URL),
            },
        }),
    ],
};

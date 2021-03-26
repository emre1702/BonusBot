module.exports = {
    root: true,
    plugins: ['@typescript-eslint', '@angular-eslint'],
    overrides: [
        {
            files: ['*.ts'],
            extends: [
                'plugin:@typescript-eslint/recommended',
                'plugin:@typescript-eslint/recommended-requiring-type-checking',
                'plugin:@angular-eslint/recommended',
            ],
            parser: '@typescript-eslint/parser',
            parserOptions: {
                ecmaVersion: 2020,
                project: ['src/tsconfig.app.json', 'src/tsconfig.server.json', 'src/tsconfig.spec.json'],
                sourceType: 'module',
            },
            rules: {
                '@typescript-eslint/explicit-module-boundary-types': 'off',
                '@typescript-eslint/no-misused-promises': 'off',
                '@typescript-eslint/no-floating-promises': 'off',
                '@typescript-eslint/no-unsafe-assignment': 'off',
                '@typescript-eslint/no-unsafe-call': 'off',
                '@typescript-eslint/no-unsafe-return': 'off',
                '@typescript-eslint/no-unsafe-member-access': 'off',
            },
        },
    ],
};

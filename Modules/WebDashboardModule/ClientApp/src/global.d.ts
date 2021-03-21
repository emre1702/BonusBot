export {};

declare global {
    interface String {
        format(...replacements: string[]): string;
    }
}

declare var process: Process;

interface Process {
    env: Env;
}

interface Env {
    WEB_BASE_URL: string;
}

interface GlobalEnvironment {
    process: Process;
}

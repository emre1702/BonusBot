if (!String.prototype.format) {
    String.prototype.format = function (...args: string[]): string {
        return this.replace(/{(\d+)}/g, (match: string, number: number) => (typeof args[number] != 'undefined' ? args[number] : match));
    };
}

if (!String.prototype.toTitleCase) {
    String.prototype.toTitleCase = function (): string {
        return this.replace(/\w\S*/g, (txt: string) => txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase());
    };
}

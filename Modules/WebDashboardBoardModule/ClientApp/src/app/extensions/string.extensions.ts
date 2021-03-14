export {};

if (!String.prototype.format) {
  String.prototype.format = function (...args: string[]): string {
    return this.replace(/{(\d+)}/g, (match: string, number: number) => {
      return typeof args[number] != "undefined" ? args[number] : match;
    });
  };
}

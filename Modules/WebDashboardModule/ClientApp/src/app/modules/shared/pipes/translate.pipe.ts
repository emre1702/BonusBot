import { Pipe, PipeTransform } from '@angular/core';
import { SettingsService } from '../services/settings.service';

@Pipe({
    name: 'translate',
    pure: false,
})
export class TranslatePipe implements PipeTransform {
    constructor(private readonly settings: SettingsService) {}

    transform(index: string, ...formatReplace: any[]): any {
        const str = this.settings.language[index] || index;
        if (formatReplace) {
            return this.formatString(str, ...formatReplace);
        }
        return str;
    }

    formatString(str: string, ...val: any[]) {
        for (let index = 0; index < val.length; index++) {
            str = str.replace(`{${index}}`, String(val[index]));
        }
        return str;
    }
}

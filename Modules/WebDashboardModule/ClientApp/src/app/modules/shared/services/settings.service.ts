import { Injectable } from '@angular/core';
import { English } from 'src/app/language/english';
import { German } from 'src/app/language/german';
import { Language } from 'src/app/language/language';
import { LanguageValue } from 'src/app/language/language-value';

@Injectable()
export class SettingsService {
    private static languages = {
        [LanguageValue.English]: new English(),
        [LanguageValue.German]: new German(),
    };

    language: Language = SettingsService.languages[LanguageValue.English];

    constructor() {
        this.language = this.getLanguage();
    }

    setLanguage(value: LanguageValue) {
        this.language = SettingsService.languages[value];
    }

    private getLanguage(): Language {
        const lang = (navigator.language || window.navigator.language).toLowerCase();
        if (lang.startsWith('de_') || lang.startsWith('de-')) {
            return SettingsService.languages[LanguageValue.German];
        }

        return SettingsService.languages[LanguageValue.English];
    }
}

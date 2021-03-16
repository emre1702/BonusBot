import { Injectable } from '@angular/core';
import { English } from 'src/app/language/english';
import { German } from 'src/app/language/german';
import { Language } from 'src/app/language/language';
import { LanguageValue } from 'src/app/language/language-value';

@Injectable({ providedIn: 'root' })
export class Settings {
    private static languages = {
        [LanguageValue.English]: new English(),
        [LanguageValue.German]: new German(),
    };

    language: Language = Settings.languages[LanguageValue.English];

    setLanguage(value: LanguageValue) {
        this.language = Settings.languages[value];
    }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { LanguageValue } from 'src/app/language/language-value';
import { SettingsService } from 'src/app/modules/shared/services/settings.service';
import { GuildSelectionService } from '../../services/guild-selection.service';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-toolbar',
    templateUrl: './toolbar.component.html',
    styleUrls: ['./toolbar.component.scss'],
})
export class NavigationToolbarComponent {
    @Input() navigationOpened: boolean;
    @Output() navigationOpenedChange = new EventEmitter<boolean>();

    @Input() messagesOpened: boolean;
    @Output() messagesOpenedChange = new EventEmitter<boolean>();

    @Input() isInLogin = true;

    languages = [LanguageValue.English, LanguageValue.German];

    constructor(
        readonly guildSelectionService: GuildSelectionService,
        readonly navigationService: NavigationService,
        readonly settingsService: SettingsService,
        private readonly sanitizer: DomSanitizer
    ) {}

    getImageUrl(language: LanguageValue) {
        switch (language) {
            case LanguageValue.English:
                return this.sanitizer.bypassSecurityTrustStyle(`url(assets/images/languages/english.png)`);
            case LanguageValue.German:
                return this.sanitizer.bypassSecurityTrustStyle(`url(assets/images/languages/german.png)`);
        }
    }
}

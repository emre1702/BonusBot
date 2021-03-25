import { Component } from '@angular/core';
import { AudioSettingsService } from '../../services/audio-settings.service';
import { AudioSettingsStateService } from '../../services/audio-settings-state.service';

@Component({
    selector: 'app-audio-settings',
    templateUrl: './audio-settings.component.html',
    styleUrls: ['../../../../styles/setting-styles.scss'],
    providers: [AudioSettingsStateService],
})
export class AudioSettingsComponent {
    constructor(readonly service: AudioSettingsService, readonly stateService: AudioSettingsStateService) {}
}

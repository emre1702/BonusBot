import { Component } from '@angular/core';
import { AudioSettingsService } from '../../services/audio-settings.service';
import { AudioSettingsStateService } from '../../services/audio-settings-state.service';
import { AudioPlayerStatus } from '../../enums/audio-player-status';

@Component({
    selector: 'app-audio-settings',
    templateUrl: './audio-settings.component.html',
    styleUrls: ['../../../../styles/setting-styles.scss'],
    providers: [AudioSettingsStateService],
})
export class AudioSettingsComponent {
    playerStatusEnum = AudioPlayerStatus;

    constructor(readonly service: AudioSettingsService, readonly stateService: AudioSettingsStateService) {}
}

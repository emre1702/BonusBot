import { Component } from '@angular/core';
import { AudioSettingsService } from '../../services/audio-settings.service';
import { AudioSettingsStateFacade } from '../../services/audio-settings.facade';
import { AudioPlayerStatus } from '../../enums/audio-player-status';

@Component({
    selector: 'app-audio-settings',
    templateUrl: './audio-settings.component.html',
    styleUrls: ['../../../../styles/setting-styles.scss'],
    providers: [AudioSettingsStateFacade],
})
export class AudioSettingsComponent {
    playerStatusEnum = AudioPlayerStatus;

    constructor(readonly service: AudioSettingsService, readonly facade: AudioSettingsStateFacade) {}
}

import { Component } from '@angular/core';
import { AudioSettingsService } from '../../services/audio-settings.service';
import { AudioSettingsStateService } from '../../states/audio-settings/audio-settings.state-service';

@Component({
    selector: 'app-audio-settings',
    templateUrl: './audio-settings.component.html',
    styleUrls: ['./audio-settings.component.scss'],
    providers: [AudioSettingsStateService],
})
export class AudioSettingsComponent {
    constructor(readonly service: AudioSettingsService, readonly stateService: AudioSettingsStateService) {}
}

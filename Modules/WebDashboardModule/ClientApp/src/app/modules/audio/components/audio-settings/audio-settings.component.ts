import { Component, OnInit } from '@angular/core';
import { AudioSettingsService } from '../../services/audio-settings.service';

@Component({
    selector: 'app-audio-settings',
    templateUrl: './audio-settings.component.html',
    styleUrls: ['./audio-settings.component.scss'],
    providers: [AudioSettingsService],
})
export class AudioSettingsComponent {
    constructor(readonly service: AudioSettingsService) {}
}

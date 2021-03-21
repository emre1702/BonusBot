import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-audio-provider',
    templateUrl: './audio-provider.component.html',
})
export class AudioProviderComponent {
    @Input() audioProvider = 'yt';
    @Output() audioProviderChange = new EventEmitter<string>();
}

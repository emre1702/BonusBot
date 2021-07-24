import { Component } from '@angular/core';
import { CommandService } from 'src/app/modules/shared/services/command.service';
import { AudioButton } from '../../models/audio-button';
import { AudioSettingsStateFacade } from '../../services/audio-settings.facade';

@Component({
    selector: 'app-play-queue',
    templateUrl: './play-queue.component.html',
})
export class PlayQueueComponent {
    audioProvider = 'yt';
    urlIdOrSearch: string;

    buttons: AudioButton[] = [
        { name: 'Play', func: () => this.play() },
        { name: 'Queue', func: () => this.queue() },
    ];

    constructor(private readonly commandService: CommandService, private readonly settingsFacade: AudioSettingsStateFacade) {}

    private play() {
        if (this.urlIdOrSearch) {
            this.commandService.execute(`${this.audioProvider} ${this.urlIdOrSearch}`).subscribe();
        } else {
            this.settingsFacade.resume();
        }
    }

    private queue() {
        if (this.urlIdOrSearch) {
            this.commandService.execute(`${this.audioProvider}queue ${this.urlIdOrSearch}`).subscribe();
        } else {
            this.settingsFacade.resume();
        }
    }
}

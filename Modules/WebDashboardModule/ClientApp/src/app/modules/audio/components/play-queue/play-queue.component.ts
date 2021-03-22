import { Component } from '@angular/core';
import { CommandService } from 'src/app/modules/shared/services/command.service';
import { AudioButton } from '../../models/audio-button';

@Component({
    selector: 'app-play-queue',
    templateUrl: './play-queue.component.html',
})
export class PlayQueueComponent {
    audioProvider = 'yt';
    urlIdOrSearch: string;

    buttons: AudioButton[] = [
        { name: 'Play', func: () => this.commandService.execute(`${this.audioProvider} ${this.urlIdOrSearch}`).subscribe() },
        { name: 'Queue', func: () => this.commandService.execute(`${this.audioProvider}queue ${this.urlIdOrSearch}`).subscribe() },
    ];

    constructor(private readonly commandService: CommandService) {}
}

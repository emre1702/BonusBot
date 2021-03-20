import { Component } from '@angular/core';
import { CommandService } from 'src/app/modules/shared/services/command.service';

@Component({
    selector: 'app-audio',
    templateUrl: './audio.component.html',
    styleUrls: ['./audio.component.scss'],
})
export class AudioComponent {
    urlOrSearch: string;

    constructor(private readonly commandService: CommandService) {}

    play() {
        this.commandService.execute(`yt play ${this.urlOrSearch}`);
    }
}

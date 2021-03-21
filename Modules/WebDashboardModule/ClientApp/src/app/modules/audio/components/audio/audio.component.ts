import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { CommandService } from 'src/app/modules/shared/services/command.service';

@Component({
    selector: 'app-audio',
    templateUrl: './audio.component.html',
    styleUrls: ['./audio.component.scss'],
})
export class AudioComponent {
    urlOrSearch: string;

    constructor(private readonly commandService: CommandService, private readonly sanitizer: DomSanitizer) {}

    play() {
        this.commandService.execute(`play ${this.urlOrSearch}`);
    }

    queue() {
        this.commandService.execute(`queue ${this.urlOrSearch}`);
    }

    playlist() {
        this.commandService.execute(`playlist ${this.urlOrSearch}`);
    }
}

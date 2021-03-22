import { Component, OnInit } from '@angular/core';
import { CommandService } from 'src/app/modules/shared/services/command.service';
import { AudioButton } from '../../models/audio-button';

@Component({
    selector: 'app-play-queue-playlist',
    templateUrl: './play-queue-playlist.component.html',
})
export class PlayQueuePlaylistComponent {
    audioProvider = 'yt';
    urlIdOrSearch: string;
    limit = 10;

    buttons: AudioButton[] = [
        { name: 'Playlist', func: () => this.commandService.execute(`${this.audioProvider}Playlist ${this.limit} ${this.urlIdOrSearch}`).subscribe() },
        {
            name: 'PlaylistQueue',
            func: () => this.commandService.execute(`${this.audioProvider}QueuePlaylist ${this.limit} ${this.urlIdOrSearch}`).subscribe(),
        },
    ];

    constructor(private readonly commandService: CommandService) {}
}

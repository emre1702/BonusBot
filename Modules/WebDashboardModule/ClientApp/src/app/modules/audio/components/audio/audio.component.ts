import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { QuestionDialogComponent } from 'src/app/modules/shared/popups/question-dialog/question-dialog.component';
import { CommandService } from 'src/app/modules/shared/services/command.service';
import { AudioButton } from '../../models/audio-button';

@Component({
    selector: 'app-audio',
    templateUrl: './audio.component.html',
    styleUrls: ['./audio.component.scss'],
})
export class AudioComponent {
    /*

    constructor(private readonly commandService: CommandService, private readonly dialog: MatDialog) {}

    playlist() {
        this.checkPlaylistLimit().subscribe((result) => {
            if (result) 
        });
    }

    playlistQueue() {
        this.checkPlaylistLimit().subscribe((result) => {
            if (result) ;
        });
    }

    private checkPlaylistLimit(): Observable<boolean> {
        if (this.limit <= 1) {
            return this.dialog.open(QuestionDialogComponent, { data: 'PlaylistLimit1AreYouSure' }).afterClosed();
        }
        return of(true);
    }*/
}

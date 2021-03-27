import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CommandService } from '../../shared/services/command.service';

@Injectable()
export class GamePlaningService {
    createAnnouncement(formGroup: FormGroup) {
        const game = String(formGroup.controls.game.value).replace('"', "'");
        const date = (formGroup.controls.date.value as Date).toLocaleString();
        this.commandService.execute(`meetup "${game}" "${date}"`).subscribe();
    }

    constructor(private readonly commandService: CommandService) {}
}

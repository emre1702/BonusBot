import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { first, mergeMap } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../page/services/guild-selection.service';
import { CommandService } from '../../shared/services/command.service';

@Injectable()
export class GamePlaningService {
    constructor(
        private readonly commandService: CommandService,
        private readonly httpClient: HttpClient,
        private readonly guildSelectionService: GuildSelectionService
    ) {}

    createAnnouncement(formGroup: FormGroup) {
        const game = String(formGroup.controls.game.value).replace('"', "'");
        const date = (formGroup.controls.date.value as Date).toISOString();
        this.commandService.execute(`meetup "${game}" "${date}"`).subscribe();
    }

    getLocale(): Observable<string> {
        return this.guildSelectionService.selectedGuildId$.pipe(
            first(),
            mergeMap((guildId) => this.httpClient.get(api.get.settings.locale, { responseType: 'text', params: { guildId } }))
        );
    }
}

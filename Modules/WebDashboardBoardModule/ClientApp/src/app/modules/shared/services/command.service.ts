import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../navigation/services/guild-selection.service';

@Injectable()
export class CommandService {
    constructor(private readonly httpClient: HttpClient, private readonly guildSelectionService: GuildSelectionService) {}

    execute(command: string) {
        this.guildSelectionService.selectedGuildId$.pipe(first()).subscribe((guildId) => {
            const body = { guildId, command };

            this.httpClient.post(api.post.command.execute, body).subscribe();
        });
    }
}

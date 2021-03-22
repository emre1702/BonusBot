import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { first, switchMap, tap } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../page/services/guild-selection.service';
import { MessagesService } from '../../page/services/messages.service';

@Injectable()
export class CommandService {
    constructor(
        private readonly httpClient: HttpClient,
        private readonly guildSelectionService: GuildSelectionService,
        private readonly messagesService: MessagesService
    ) {}

    execute(command: string): Observable<string[]> {
        return this.guildSelectionService.selectedGuildId$.pipe(
            first(),
            switchMap((guildId) => {
                const body = { guildId, command };

                return this.httpClient.post(api.post.command.execute, body).pipe(tap((messages: string[]) => this.messagesService.addMessages(messages)));
            })
        );
    }
}

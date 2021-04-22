import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import api from 'src/app/routes/api';
import { GuildData } from '../models/guild-data';

@Injectable({ providedIn: 'root' })
export class GuildSelectionService {
    private selectedGuildId = new ReplaySubject<string>(1);
    selectedGuildId$ = this.selectedGuildId.asObservable();

    private guilds = new ReplaySubject<GuildData[]>();
    guilds$ = this.guilds.asObservable();

    constructor(httpClient: HttpClient) {
        httpClient.get<GuildData[]>(api.get.navigation.guilds).subscribe((guilds) => {
            if (guilds) {
                this.guilds.next(guilds);
                this.selectGuildId(guilds[0]?.id);
            }
        });
    }

    selectGuildId(value: string) {
        this.selectedGuildId.next(value);
    }
}

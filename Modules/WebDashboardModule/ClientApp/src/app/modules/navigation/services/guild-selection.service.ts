import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { merge, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildData } from '../models/guild-data';

@Injectable({ providedIn: 'root' })
export class GuildSelectionService {
    private guilds = new ReplaySubject<GuildData[]>();
    guilds$ = this.guilds.asObservable();

    private selectedGuildId = new ReplaySubject<string>(1);
    selectedGuildId$ = merge(this.selectedGuildId, this.guilds.pipe(map((guilds) => guilds[0]?.id)));

    constructor(httpClient: HttpClient) {
        httpClient.get<GuildData[]>(api.get.navigation.guilds).subscribe((guilds) => {
            if (guilds) this.guilds.next(guilds);
        });
    }

    selectGuildId(value: string) {
        this.selectedGuildId.next(value);
    }
}

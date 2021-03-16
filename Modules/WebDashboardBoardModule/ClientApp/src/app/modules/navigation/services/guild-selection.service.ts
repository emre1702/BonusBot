import { Injectable } from '@angular/core';
import { merge, ReplaySubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { HttpService } from 'src/app/services/http.service';
import { GuildData } from '../models/guild-data';

@Injectable({ providedIn: 'root' })
export class GuildSelectionService {
    private guilds = new ReplaySubject<GuildData[]>();
    guilds$ = this.guilds.asObservable();

    private selectedGuildId = new Subject<string>();
    selectedGuildId$ = merge(this.selectedGuildId, this.guilds.pipe(map((guilds) => guilds[0]?.id)));

    constructor(httpService: HttpService) {
        httpService.get(api.get.navigation.guilds).subscribe((guilds) => this.guilds.next(guilds));
    }

    selectGuildId(value: string) {
        this.selectedGuildId.next(value);
    }
}

import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { first, mergeMap } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../page/services/guild-selection.service';
import { CommandService } from '../../shared/services/command.service';
import { AudioSettingsState } from '../models/audio-settings-state';
import * as Selectors from '../states/audio-settings/audio-settings.selectors';

@Injectable()
export class AudioSettingsService implements OnDestroy {
    private destroySubject = new Subject();

    volume$ = this.store.select(Selectors.selectVolume);

    constructor(private readonly httpClient: HttpClient, private readonly commandService: CommandService, private readonly store: Store) {}

    ngOnDestroy() {
        this.destroySubject.next();
    }

    setVolume(volume: number): Observable<string[]> {
        return this.commandService.execute(`setVolume ${volume}`);
    }

    loadAudioSettings(guildId: string): Observable<AudioSettingsState> {
        return this.httpClient.get<AudioSettingsState>(api.get.command.audioSettingsState, { params: { guildId: guildId } });
    }
}

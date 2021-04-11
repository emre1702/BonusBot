import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import api from 'src/app/routes/api';
import { CommandService } from '../../shared/services/command.service';
import { AudioSettingsState } from '../models/audio-settings-state';
import * as Selectors from '../states/audio-settings/audio-settings.selectors';

@Injectable()
export class AudioSettingsService implements OnDestroy {
    private destroySubject = new Subject();

    state$ = this.store.select(Selectors.selectState);

    constructor(private readonly httpClient: HttpClient, private readonly commandService: CommandService, private readonly store: Store) {}

    ngOnDestroy() {
        this.destroySubject.next();
    }

    setVolume(volume: number): Observable<string[]> {
        return this.commandService.execute(`setVolume ${volume}`);
    }

    pause(): Observable<string[]> {
        return this.commandService.execute('pause');
    }

    resume(): Observable<string[]> {
        return this.commandService.execute('resume');
    }

    stop(): Observable<string[]> {
        return this.commandService.execute('stop');
    }

    join(): Observable<string[]> {
        return this.commandService.execute('join');
    }

    leave(): Observable<string[]> {
        return this.commandService.execute('leave');
    }

    loadAudioSettings(guildId: string): Observable<AudioSettingsState> {
        return this.httpClient.get<AudioSettingsState>(api.get.audio.audioState, { params: { guildId: guildId } });
    }
}

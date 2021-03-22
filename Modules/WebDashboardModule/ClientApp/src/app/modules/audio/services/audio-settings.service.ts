import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { interval, merge, ReplaySubject, Subject } from 'rxjs';
import { distinctUntilChanged, map, mergeMap, repeat, takeUntil, withLatestFrom } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../page/services/guild-selection.service';
import { CommandService } from '../../shared/services/command.service';
import { VolumeData } from '../models/volume-data';

@Injectable()
export class AudioSettingsService implements OnDestroy {
    private destroySubject = new Subject();

    private volume = new ReplaySubject<number>();
    volume$ = merge(
        this.volume,
        merge(interval(10 * 1000).pipe(repeat()), this.guildSelection.selectedGuildId$).pipe(
            withLatestFrom(this.guildSelection.selectedGuildId$),
            mergeMap(([, guildId]) =>
                this.httpClient
                    .get<VolumeData>(api.get.command.volume, { params: { guildId: guildId } })
                    .pipe(map((v) => v.volume))
            )
        )
    ).pipe(distinctUntilChanged(), takeUntil(this.destroySubject));

    constructor(
        private readonly httpClient: HttpClient,
        private readonly guildSelection: GuildSelectionService,
        private readonly commandService: CommandService
    ) {}

    ngOnDestroy() {
        this.destroySubject.next();
    }

    setVolume(volume: number) {
        this.commandService.execute(`setVolume ${volume}`).subscribe(() => this.volume.next(volume));
    }
}

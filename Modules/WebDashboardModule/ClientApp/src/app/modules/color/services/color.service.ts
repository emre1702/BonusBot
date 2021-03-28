import { Color } from '@angular-material-components/color-picker';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { combineLatest, forkJoin, merge, of, ReplaySubject, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map, mergeMap, pairwise, skip, switchMap, takeUntil, tap, withLatestFrom } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../page/services/guild-selection.service';
import { CommandService } from '../../shared/services/command.service';

@Injectable()
export class ColorService implements OnDestroy {
    color$ = this.guildSelectionService.selectedGuildId$.pipe(
        mergeMap((guildId) =>
            this.httpClient
                .get<{ r: number; g: number; b: number }>(api.get.color.userColor, { params: { guildId } })
                .pipe(map((discordColor) => new Color(discordColor.r, discordColor.g, discordColor.b)))
        )
    );

    colorChanged = new Subject<Color>();

    private destroySubject = new Subject();

    constructor(
        private readonly httpClient: HttpClient,
        private readonly commandService: CommandService,
        private readonly guildSelectionService: GuildSelectionService
    ) {
        this.colorChanged
            .pipe(
                takeUntil(this.destroySubject)
                /*mergeMap((color) => combineLatest([of(color), ])),
                tap((color) => this.colorRequest.next(color))*/
            )
            /*.subscribe();*/
            .subscribe((color) => this.commandService.execute(`color #${color.hex}`).subscribe());
    }

    ngOnDestroy() {
        this.destroySubject.next();
    }
}
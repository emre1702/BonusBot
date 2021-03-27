import { Color } from '@angular-material-components/color-picker';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, mergeMap, pairwise, takeUntil, tap, withLatestFrom } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../page/services/guild-selection.service';
import { CommandService } from '../../shared/services/command.service';

@Injectable()
export class ColorService implements OnDestroy {
    private colorRequest = new ReplaySubject(1);
    color$ = this.colorRequest.pipe(
        withLatestFrom(this.guildSelectionService.selectedGuildId$),
        mergeMap(([, guildId]) =>
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
        this.colorRequest.next();

        this.colorChanged
            .pipe(takeUntil(this.destroySubject), distinctUntilChanged(), pairwise(), debounceTime(3000))
            .subscribe(([, color]) => this.setColor(color));
    }

    ngOnDestroy() {
        this.destroySubject.next();
    }

    private setColor(color: Color) {
        this.commandService.execute(`color #${color.hex}`).subscribe();
    }
}

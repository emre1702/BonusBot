import { Color } from '@angular-material-components/color-picker';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { filter, map, mergeMap, takeUntil, withLatestFrom } from 'rxjs/operators';
import api from 'src/app/routes/api';
import { GuildSelectionService } from '../../page/services/guild-selection.service';
import { CommandService } from '../../shared/services/command.service';

@Injectable()
export class ColorService implements OnDestroy {
    private destroySubject = new Subject();

    private color = new ReplaySubject<Color>(1);
    color$ = this.color.asObservable();

    colorChanged = new Subject<Color>();

    constructor(
        private readonly httpClient: HttpClient,
        private readonly commandService: CommandService,
        private readonly guildSelectionService: GuildSelectionService
    ) {
        this.addSubscriptions();
    }

    ngOnDestroy() {
        this.destroySubject.next();
    }

    private addSubscriptions() {
        this.colorChanged
            .pipe(
                takeUntil(this.destroySubject),
                withLatestFrom(this.color$),
                filter(([newColor, currentColor]) => newColor.rgba != currentColor.rgba)
            )
            .subscribe(([color]) => this.commandService.execute(`color #${color.hex}`).subscribe());

        this.guildSelectionService.selectedGuildId$
            .pipe(
                takeUntil(this.destroySubject),
                mergeMap((guildId) => this.httpClient.get<{ r: number; g: number; b: number }>(api.get.color.userColor, { params: { guildId } })),
                map((discordColor) => new Color(discordColor.r, discordColor.g, discordColor.b))
            )
            .subscribe((color) => this.color.next(color));
    }
}

import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { merge, Observable, of, ReplaySubject, Subject } from 'rxjs';
import { finalize, switchMap, takeUntil, tap, withLatestFrom } from 'rxjs/operators';
import { Constants } from 'src/app/constants';
import api from 'src/app/routes/api';
import { HttpService } from 'src/app/services/http.service';
import { GuildSelectionService } from '../../navigation/services/guild-selection.service';
import { NavigationService } from '../../navigation/services/navigation.service';
import { UserAccessLevel } from '../models/user-access-level';

@Injectable()
export class ContentService implements OnDestroy {
    loading: boolean;

    private accessLevelRequest = new Subject<{}>();
    accessLevel$: Observable<UserAccessLevel> = this.accessLevelRequest.pipe(
        switchMap((params) => {
            this.loading = true;
            if (!params) return of(UserAccessLevel.hasAccess).pipe(finalize(() => (this.loading = false)));
            return this.httpService
                .get<UserAccessLevel>(api.get.content.accessLevel, { params: params })
                .pipe(finalize(() => (this.loading = false)));
        })
    );

    private destroySubject = new Subject();

    constructor(navigationService: NavigationService, guildSelectionService: GuildSelectionService, private readonly httpService: HttpService) {
        navigationService.navigateTo$
            .pipe(takeUntil(this.destroySubject), withLatestFrom(guildSelectionService.selectedGuildId$))
            .subscribe(([url, guildId]) => this.navigatedToUrl(url, guildId));
        guildSelectionService.selectedGuildId$
            .pipe(takeUntil(this.destroySubject))
            .subscribe((guildId) => this.navigatedToUrl(window.location.pathname, guildId));
    }

    ngOnDestroy() {
        this.destroySubject.next();
    }

    private navigatedToUrl(url: string, selectedGuildId: string) {
        const button = Constants.buttons.find((b) => b.url.toLowerCase() === url.toLowerCase());
        this.accessLevelRequest.next(button?.module && !button.alwaysAccess ? { module: button.module, guildId: '' + selectedGuildId } : undefined);
    }
}

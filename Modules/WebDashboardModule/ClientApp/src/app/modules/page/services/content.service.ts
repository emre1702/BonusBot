import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { merge, Observable, of, Subject } from 'rxjs';
import { distinctUntilChanged, finalize, switchMap, takeUntil } from 'rxjs/operators';
import { Constants } from 'src/app/constants';
import api from 'src/app/routes/api';
import { UserAccessLevel } from '../models/user-access-level';
import { GuildSelectionService } from './guild-selection.service';
import { NavigationService } from './navigation.service';

@Injectable()
export class ContentService implements OnDestroy {
    loading: boolean;

    private accessLevelRequest = new Subject<{}>();
    accessLevel$: Observable<UserAccessLevel> = this.accessLevelRequest.pipe(
        switchMap((params) => {
            this.loading = true;
            if (!params) return of(UserAccessLevel.hasAccess).pipe(finalize(() => (this.loading = false)));
            return this.httpClient
                .get<UserAccessLevel>(api.get.content.accessLevel, { params: params })
                .pipe(finalize(() => (this.loading = false)));
        })
    );

    private destroySubject = new Subject();

    constructor(navigationService: NavigationService, guildSelectionService: GuildSelectionService, private readonly httpClient: HttpClient) {
        merge(navigationService.navigateTo$, guildSelectionService.selectedGuildId$)
            .pipe(distinctUntilChanged(), takeUntil(this.destroySubject))
            .subscribe(([url, guildId]) => this.navigatedToUrl(url ?? window.location.pathname, guildId));
    }

    ngOnDestroy() {
        this.destroySubject.next();
    }

    private navigatedToUrl(url: string, selectedGuildId: string) {
        const button = Constants.navigationButtons.find((b) => b.url.toLowerCase() === url.toLowerCase());
        this.accessLevelRequest.next(button?.module && !button.alwaysAccess ? { module: button.module, guildId: '' + selectedGuildId } : undefined);
    }
}

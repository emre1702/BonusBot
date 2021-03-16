import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnDestroy } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { distinctUntilChanged, filter, map } from 'rxjs/operators';
import { Constants } from 'src/app/constants';
import { GuildSelectionService } from '../../services/guild-selection.service';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.scss'],
})
export class NavigationToolbarComponent implements OnDestroy {
    isInLogin$ = this.router.events.pipe(
        filter((e) => e instanceof NavigationEnd),
        map((e: NavigationEnd) => e.urlAfterRedirects.endsWith('/login'))
    );
    buttons = Constants.buttons;

    mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;

    constructor(
        readonly navigationService: NavigationService,
        readonly router: Router,
        readonly guildSelectionService: GuildSelectionService,
        changeDetectorRef: ChangeDetectorRef,
        media: MediaMatcher
    ) {
        navigationService.navigateTo$.pipe(distinctUntilChanged()).subscribe((url) => router.navigateByUrl(url));

        this.mobileQuery = media.matchMedia('(max-width: 600px)');
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addEventListener('change', this._mobileQueryListener);
    }

    ngOnDestroy(): void {
        this.mobileQuery.removeEventListener('change', this._mobileQueryListener);
    }
}

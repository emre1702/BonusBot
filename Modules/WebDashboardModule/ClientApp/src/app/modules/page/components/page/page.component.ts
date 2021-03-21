import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnDestroy } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';

@Component({
    selector: 'app-page',
    templateUrl: './page.component.html',
    styleUrls: ['./page.component.scss'],
})
export class PageComponent implements OnDestroy {
    mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;

    navigationOpened = true;
    messagesOpened = true;

    isInLogin$ = this.router.events.pipe(
        filter((e) => e instanceof NavigationEnd),
        map((e: NavigationEnd) => e.urlAfterRedirects.endsWith('/login'))
    );

    constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, private readonly router: Router) {
        this.mobileQuery = media.matchMedia('(max-width: 600px)');
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addEventListener('change', this._mobileQueryListener);
    }

    ngOnDestroy(): void {
        this.mobileQuery.removeEventListener('change', this._mobileQueryListener);
    }
}

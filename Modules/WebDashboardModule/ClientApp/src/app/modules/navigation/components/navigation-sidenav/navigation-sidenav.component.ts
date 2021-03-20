import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { Router } from '@angular/router';
import { distinctUntilChanged } from 'rxjs/operators';
import { Constants } from 'src/app/constants';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-navigation-sidenav',
    templateUrl: './navigation-sidenav.component.html',
    styleUrls: ['./navigation-sidenav.component.scss'],
})
export class NavigationSidenavComponent implements OnDestroy {
    @Input() sidenavOpened: boolean;
    @Output() sidenavOpenedChange = new EventEmitter<boolean>();

    mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;

    buttons = Constants.buttons;

    constructor(readonly navigationService: NavigationService, readonly router: Router, changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
        navigationService.navigateTo$.pipe(distinctUntilChanged()).subscribe((url) => router.navigateByUrl(url));

        this.mobileQuery = media.matchMedia('(max-width: 600px)');
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addEventListener('change', this._mobileQueryListener);
    }

    ngOnDestroy(): void {
        this.mobileQuery.removeEventListener('change', this._mobileQueryListener);
    }
}

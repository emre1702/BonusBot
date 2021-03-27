import { MediaMatcher } from '@angular/cdk/layout';
import { Injectable, OnDestroy } from '@angular/core';

@Injectable()
export class MobileService implements OnDestroy {
    private mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;

    isMobileView: boolean;

    constructor(media: MediaMatcher) {
        this.mobileQuery = media.matchMedia('(max-width: 600px)');
        this._mobileQueryListener = () => (this.isMobileView = this.mobileQuery.matches);
        this.mobileQuery.addEventListener('change', this._mobileQueryListener);
        this._mobileQueryListener();
    }

    ngOnDestroy(): void {
        this.mobileQuery.removeEventListener('change', this._mobileQueryListener);
    }
}

import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class NavigationService {
    private navigateTo = new Subject<string>();
    navigateTo$ = this.navigateTo.asObservable();

    navigateToUrl(url: string) {
        this.navigateTo.next(url);
    }

    navigateToLogin() {
        this.navigateToUrl('/login');
    }
}

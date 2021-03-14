import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class NavigationService {
    private navigateTo = new Subject<string>();
    navigateTo$ = this.navigateTo.asObservable();

    navigateToLogin() {
        this.navigateTo.next('/login');
    }
}

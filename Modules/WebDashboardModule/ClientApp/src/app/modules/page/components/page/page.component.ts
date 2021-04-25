import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { merge, Subject } from 'rxjs';
import { filter, map, share } from 'rxjs/operators';

@Component({
    selector: 'app-page',
    templateUrl: './page.component.html',
    styleUrls: ['./page.component.scss'],
})
export class PageComponent {
    isInLogin$ = this.router.events.pipe(
        share(),
        filter((e) => e instanceof NavigationEnd),
        map((e: NavigationEnd) => e.urlAfterRedirects.endsWith('/login'))
    );

    private navigationOpened = new Subject<boolean>();
    navigationOpened$ = merge(
        this.navigationOpened,
        this.isInLogin$.pipe(
            filter((isInLogin) => isInLogin),
            map((isInLogin) => !isInLogin)
        )
    );

    messagesOpened = true;

    constructor(private readonly router: Router) {}

    setNavigationOpened(toggle: boolean) {
        this.navigationOpened.next(toggle);
    }
}

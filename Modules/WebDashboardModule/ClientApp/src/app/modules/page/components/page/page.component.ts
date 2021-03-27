import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';

@Component({
    selector: 'app-page',
    templateUrl: './page.component.html',
    styleUrls: ['./page.component.scss'],
})
export class PageComponent {
    navigationOpened = true;
    messagesOpened = true;

    isInLogin$ = this.router.events.pipe(
        filter((e) => e instanceof NavigationEnd),
        map((e: NavigationEnd) => e.urlAfterRedirects.endsWith('/login'))
    );

    constructor(private readonly router: Router) {}
}

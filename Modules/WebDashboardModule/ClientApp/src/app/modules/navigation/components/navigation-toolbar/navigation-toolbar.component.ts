import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';
import { GuildSelectionService } from '../../services/guild-selection.service';

@Component({
    selector: 'app-navigation-toolbar',
    templateUrl: './navigation-toolbar.component.html',
    styleUrls: ['./navigation-toolbar.component.scss'],
})
export class NavigationToolbarComponent {
    @Input() sidenavOpened: boolean;
    @Output() sidenavOpenedChange = new EventEmitter<boolean>();

    isInLogin$ = this.router.events.pipe(
        filter((e) => e instanceof NavigationEnd),
        map((e: NavigationEnd) => e.urlAfterRedirects.endsWith('/login'))
    );

    constructor(readonly router: Router, readonly guildSelectionService: GuildSelectionService) {}
}

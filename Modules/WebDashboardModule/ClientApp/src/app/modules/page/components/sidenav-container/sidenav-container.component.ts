import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { distinctUntilChanged } from 'rxjs/operators';
import { Constants } from 'src/app/constants';
import { MessagesService } from '../../services/messages.service';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-sidenav-container',
    templateUrl: './sidenav-container.component.html',
})
export class SidenavContainerComponent {
    @Input() isMobileView: boolean;
    @Input() isInLogin: boolean;

    @Input() navigationOpened: boolean;
    @Output() navigationOpenedChange = new EventEmitter<boolean>();

    @Input() messagesOpened: boolean;
    @Output() messagesOpenedChange = new EventEmitter<boolean>();

    buttons = Constants.buttons;

    constructor(readonly messagesService: MessagesService, private readonly navigationService: NavigationService, router: Router) {
        navigationService.navigateTo$.pipe(distinctUntilChanged()).subscribe((url) => router.navigateByUrl(url));
    }

    navigateToUrl(url: string) {
        this.navigationService.navigateToUrl(url);
        this.navigationOpenedChange.emit(false);
    }
}

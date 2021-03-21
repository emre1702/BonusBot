import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { distinctUntilChanged } from 'rxjs/operators';
import { Constants } from 'src/app/constants';
import { LogoutService } from 'src/app/modules/login/services/logout.service';
import { ConfirmationDialogComponent } from 'src/app/modules/shared/popups/confirmation-dialog/confirmation-dialog.component';
import { NavigationButton } from '../../models/navigation-button';
import { MessagesService } from '../../services/messages.service';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-sidenav-container',
    templateUrl: './sidenav-container.component.html',
    providers: [LogoutService],
})
export class SidenavContainerComponent {
    @Input() isMobileView: boolean;
    @Input() isInLogin: boolean;

    @Input() navigationOpened: boolean;
    @Output() navigationOpenedChange = new EventEmitter<boolean>();

    @Input() messagesOpened: boolean;
    @Output() messagesOpenedChange = new EventEmitter<boolean>();

    buttons = Constants.navigationButtons;

    constructor(
        readonly messagesService: MessagesService,
        private readonly navigationService: NavigationService,
        router: Router,
        private readonly dialog: MatDialog,
        readonly logoutService: LogoutService
    ) {
        navigationService.navigateTo$.pipe(distinctUntilChanged()).subscribe((url) => router.navigateByUrl(url));
    }

    navigateTo(button: NavigationButton) {
        if (button.confirmationTextKey) {
            this.dialog
                .open(ConfirmationDialogComponent, { data: button.confirmationTextKey })
                .afterClosed()
                .subscribe((confirmed: boolean) => {
                    if (!confirmed) return;
                    this.navigationService.navigateToUrl(button.url);
                    this.navigationOpenedChange.emit(false);
                });
        } else {
            this.navigationService.navigateToUrl(button.url);
            this.navigationOpenedChange.emit(false);
        }
    }
}

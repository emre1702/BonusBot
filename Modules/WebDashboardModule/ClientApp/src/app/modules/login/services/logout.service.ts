import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import api from 'src/app/routes/api';
import { NavigationService } from '../../page/services/navigation.service';
import { ConfirmationDialogComponent } from '../../shared/popups/confirmation-dialog/confirmation-dialog.component';

@Injectable()
export class LogoutService {
    constructor(private readonly httpClient: HttpClient, private readonly navigationService: NavigationService, private readonly dialog: MatDialog) {}

    signOut() {
        this.dialog
            .open(ConfirmationDialogComponent, { data: 'LogoutConfirmationText' })
            .afterClosed()
            .subscribe((confirmed: boolean) => {
                if (!confirmed) return;
                this.httpClient.post(api.post.logout.execute, null).subscribe(() => this.navigationService.navigateToLogin());
            });
    }
}

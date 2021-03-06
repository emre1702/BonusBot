import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'confirmation-dialog',
    templateUrl: './confirmation-dialog.component.html',
})
export class ConfirmationDialogComponent {
    constructor(readonly dialogRef: MatDialogRef<ConfirmationDialogComponent>, @Inject(MAT_DIALOG_DATA) readonly questionKey: string) {}
}

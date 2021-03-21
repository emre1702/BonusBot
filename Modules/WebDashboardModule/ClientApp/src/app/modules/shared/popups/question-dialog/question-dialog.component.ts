import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'question-dialog',
    templateUrl: './question-dialog.component.html',
})
export class QuestionDialogComponent {
    constructor(readonly dialogRef: MatDialogRef<QuestionDialogComponent>, @Inject(MAT_DIALOG_DATA) readonly questionKey: string) {}
}

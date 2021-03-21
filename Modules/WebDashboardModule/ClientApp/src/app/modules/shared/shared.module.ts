import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslatePipe } from './pipes/translate.pipe';
import { SettingsService } from './services/settings.service';
import { CommandService } from './services/command.service';
import { QuestionDialogComponent } from './popups/question-dialog/question-dialog.component';
import { MaterialModule } from '../material/material.module';
import { NumberInputComponent } from './components/number-input/number-input.component';
import { FormsModule } from '@angular/forms';

const EXPORTS = [TranslatePipe, QuestionDialogComponent, NumberInputComponent];
const PROVIDERS = [SettingsService, CommandService];

@NgModule({
    declarations: [...EXPORTS],
    imports: [CommonModule, MaterialModule, FormsModule],
    exports: [...EXPORTS],
    providers: [...PROVIDERS],
})
export class SharedModule {}

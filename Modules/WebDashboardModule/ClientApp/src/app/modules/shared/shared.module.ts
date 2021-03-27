import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslatePipe } from './pipes/translate.pipe';
import { SettingsService } from './services/settings.service';
import { CommandService } from './services/command.service';
import { ConfirmationDialogComponent } from './popups/confirmation-dialog/confirmation-dialog.component';
import { MaterialModule } from '../material/material.module';
import { NumberInputComponent } from './components/number-input/number-input.component';
import { FormsModule } from '@angular/forms';
import { MobileService } from './services/mobile.service';
import { DateTimePickerComponent } from './components/date-time-picker/date-time-picker.component';

const EXPORTS = [TranslatePipe, ConfirmationDialogComponent, NumberInputComponent, DateTimePickerComponent];
const PROVIDERS = [SettingsService, CommandService, MobileService];

@NgModule({
    declarations: [...EXPORTS, DateTimePickerComponent],
    imports: [CommonModule, MaterialModule, FormsModule],
    exports: [...EXPORTS],
    providers: [...PROVIDERS],
})
export class SharedModule {}

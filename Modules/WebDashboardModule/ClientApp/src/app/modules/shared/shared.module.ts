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
import { MAT_COLOR_FORMATS, NgxMatColorPickerModule, NGX_MAT_COLOR_FORMATS } from '@angular-material-components/color-picker';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ColorPickerComponent } from './components/color-picker/color-picker.component';

const EXPORTS = [TranslatePipe, ConfirmationDialogComponent, NumberInputComponent, DateTimePickerComponent, ColorPickerComponent];

@NgModule({
    declarations: [...EXPORTS],
    imports: [
        CommonModule,
        MaterialModule,
        FormsModule,
        MatMomentDateModule,
        NgxMatDatetimePickerModule,
        NgxMatNativeDateModule,
        NgxMatTimepickerModule,
        NgxMatColorPickerModule,
        MatDatepickerModule,
    ],
    exports: [...EXPORTS],
    providers: [SettingsService, CommandService, MobileService, { provide: MAT_COLOR_FORMATS, useValue: NGX_MAT_COLOR_FORMATS }],
})
export class SharedModule {}

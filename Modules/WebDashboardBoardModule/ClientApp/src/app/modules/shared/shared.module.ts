import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslatePipe } from './pipes/translate.pipe';
import { SettingsService } from './services/settings.service';
import { CommandService } from './services/command.service';

const EXPORTS = [TranslatePipe];
const PROVIDERS = [SettingsService, CommandService];

@NgModule({
    declarations: [...EXPORTS],
    imports: [CommonModule],
    exports: [...EXPORTS],
    providers: [...PROVIDERS],
})
export class SharedModule {}

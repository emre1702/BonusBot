import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslatePipe } from './pipes/translate.pipe';

const EXPORTS = [TranslatePipe];

@NgModule({
    declarations: [...EXPORTS],
    imports: [CommonModule],
    exports: [...EXPORTS],
})
export class SharedModule {}

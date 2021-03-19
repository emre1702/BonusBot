import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AudioComponent } from './components/audio/audio.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations: [AudioComponent],
    imports: [CommonModule, FormsModule, SharedModule],
    exports: [AudioComponent],
})
export class AudioModule {}

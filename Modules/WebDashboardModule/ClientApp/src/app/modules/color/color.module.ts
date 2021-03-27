import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColorComponent } from './components/color/color.component';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../material/material.module';

@NgModule({
    declarations: [ColorComponent],
    imports: [CommonModule, FormsModule, MaterialModule, SharedModule],
})
export class ColorModule {}

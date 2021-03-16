import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationToolbarComponent } from './components/navigation/navigation.component';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../material/material.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [NavigationToolbarComponent],
    imports: [CommonModule, FormsModule, SharedModule, MaterialModule, RouterModule],
    exports: [NavigationToolbarComponent],
})
export class NavigationModule {}

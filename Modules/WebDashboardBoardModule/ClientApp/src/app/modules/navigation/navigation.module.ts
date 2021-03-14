import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationToolbarComponent } from './components/navigation-toolbar/navigation-toolbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';

@NgModule({
    declarations: [NavigationToolbarComponent],
    imports: [CommonModule, MatToolbarModule],
    exports: [NavigationToolbarComponent],
})
export class NavigationModule {}

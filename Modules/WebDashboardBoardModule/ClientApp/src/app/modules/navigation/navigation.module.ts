import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './components/navigation/navigation.component';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../material/material.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NavigationSidenavComponent } from './components/navigation-sidenav/navigation-sidenav.component';
import { NavigationToolbarComponent } from './components/navigation-toolbar/navigation-toolbar.component';

@NgModule({
    declarations: [NavigationComponent, NavigationToolbarComponent, NavigationSidenavComponent],
    imports: [CommonModule, FormsModule, SharedModule, MaterialModule, RouterModule],
    exports: [NavigationComponent],
})
export class NavigationModule {}

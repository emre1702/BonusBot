import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageComponent } from './components/page/page.component';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../material/material.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NavigationToolbarComponent } from './components/toolbar/toolbar.component';
import { ContentComponent } from './components/content/content.component';
import { SidenavContainerComponent } from './components/sidenav-container/sidenav-container.component';

@NgModule({
    declarations: [PageComponent, NavigationToolbarComponent, ContentComponent, SidenavContainerComponent],
    imports: [CommonModule, FormsModule, SharedModule, MaterialModule, RouterModule],
    exports: [PageComponent],
})
export class PageModule {}

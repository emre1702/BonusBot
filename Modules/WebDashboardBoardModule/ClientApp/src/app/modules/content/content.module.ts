import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentComponent } from './components/content/content.component';

@NgModule({
    declarations: [ContentComponent],
    imports: [CommonModule],
    exports: [ContentComponent],
})
export class ContentModule {}

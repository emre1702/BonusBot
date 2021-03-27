import { Component } from '@angular/core';
import { ColorService } from '../../services/color.service';

@Component({
    selector: 'app-color',
    templateUrl: './color.component.html',
    styleUrls: ['../../../../styles/setting-styles.scss'],
    providers: [ColorService],
})
export class ColorComponent {
    constructor(readonly service: ColorService) {}
}

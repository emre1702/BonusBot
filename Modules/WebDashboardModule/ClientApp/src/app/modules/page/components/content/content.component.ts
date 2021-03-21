import { Component } from '@angular/core';
import { UserAccessLevel } from '../../models/user-access-level';
import { ContentService } from '../../services/content.service';

@Component({
    selector: 'app-content',
    templateUrl: './content.component.html',
    providers: [ContentService],
})
export class ContentComponent {
    accessLevelEnum = UserAccessLevel;

    constructor(readonly service: ContentService) {}
}

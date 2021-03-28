import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Store } from '@ngrx/store';
import { ServerSettingsStateService } from '../../../services/server-settings-state.service';

@Component({
    selector: 'app-settings-page-container',
    templateUrl: './settings-page-container.component.html',
})
export class SettingsPageContainerComponent {
    @Input() isActive: boolean;
    @Input() canDisable: boolean;
    @Input() moduleName: string;

    @Output() isActiveChange = new EventEmitter<boolean>();
    constructor(readonly service: ServerSettingsStateService, private readonly store: Store) {}
}

import { Component, OnInit } from '@angular/core';
import { ServerSettingsStateService } from '../../services/server-settings-state.service';

@Component({
    selector: 'app-server-settings',
    templateUrl: './server-settings.component.html',
    styleUrls: ['../../../../styles/setting-styles.scss'],
    providers: [ServerSettingsStateService],
})
export class ServerSettingsComponent implements OnInit {
    constructor(readonly service: ServerSettingsStateService) {}

    ngOnInit() {
        this.service.loadAllModuleDatas();
    }
}

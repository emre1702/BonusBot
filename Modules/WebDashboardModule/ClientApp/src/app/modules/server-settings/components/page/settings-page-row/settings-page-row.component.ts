import { Component, Input } from '@angular/core';
import { ServerSettingType } from '../../../enums/server-setting-type';
import { ServerSettingData } from '../../../models/server-setting-data';
import { ServerSettingsStateService } from '../../../services/server-settings-state.service';

@Component({
    selector: 'app-settings-page-row',
    templateUrl: './settings-page-row.component.html',
    styleUrls: ['./settings-page-row.component.scss'],
})
export class SettingsPageRowComponent {
    @Input() moduleName: string;
    @Input() settingKey: string;
    @Input() settingData: ServerSettingData;

    typeEnum = ServerSettingType;

    constructor(private readonly service: ServerSettingsStateService) {}

    changeSetting(value: unknown) {
        this.service.setSetting(this.moduleName, this.settingKey, value, this.settingData.info.type);
    }
}

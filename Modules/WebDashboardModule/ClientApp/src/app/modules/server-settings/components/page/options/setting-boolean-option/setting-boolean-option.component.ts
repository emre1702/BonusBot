import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ServerSettingInfo } from 'src/app/modules/server-settings/models/server-setting-info';

@Component({
    selector: 'app-setting-boolean-option',
    templateUrl: './setting-boolean-option.component.html',
    styleUrls: ['../options-base-styles.scss'],
})
export class SettingBooleanOptionComponent {
    @Input() name: string;
    @Input() info: ServerSettingInfo;

    valueBoolean: boolean;
    @Input() set value(v: string) {
        if (v?.length) this.valueBoolean = v.toLowerCase() === 'true';
        else this.valueBoolean = this.info.defaultValue as boolean;
    }
    @Output() valueChange = new EventEmitter<string>();

    valueChanged(value: boolean) {
        this.valueChange.emit(String(value));
    }
}

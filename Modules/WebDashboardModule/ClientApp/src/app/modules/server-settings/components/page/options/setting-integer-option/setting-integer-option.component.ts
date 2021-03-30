import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ServerSettingInfo } from 'src/app/modules/server-settings/models/server-setting-info';

@Component({
    selector: 'app-setting-integer-option',
    templateUrl: './setting-integer-option.component.html',
    styleUrls: ['../options-base-styles.scss'],
})
export class SettingIntegerOptionComponent {
    @Input() name: string;
    @Input() info: ServerSettingInfo;

    valueInteger: number;
    @Input() set value(v: string) {
        if (v?.length) this.valueInteger = Number(v);
        else this.valueInteger = this.info.defaultValue as number;
    }
    @Output() valueChange = new EventEmitter<string>();

    valueChanged(value: number) {
        this.valueChange.emit(String(value));
    }
}

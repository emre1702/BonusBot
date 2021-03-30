import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ServerSettingInfo } from 'src/app/modules/server-settings/models/server-setting-info';

@Component({
    selector: 'app-setting-string-option',
    templateUrl: './setting-string-option.component.html',
    styleUrls: ['../options-base-styles.scss'],
})
export class SettingStringOptionComponent {
    @Input() name: string;
    @Input() info: ServerSettingInfo;
    @Input() value: string;

    @Output() valueChange = new EventEmitter<string>();

    get placeHolder(): string {
        if (this.value?.length) return '';
        return (this.info?.defaultValue as string) ?? '';
    }
}

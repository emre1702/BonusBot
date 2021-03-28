import { ServerSettingType } from '../enums/server-setting-type';

export interface ServerSettingInfo {
    type: ServerSettingType;
    min?: number;
    max?: number;
    defaultValue?: unknown;
}

import { ServerSettingInfo } from './server-setting-info';

export interface ServerSettingData {
    moduleName: string;
    settingName: string;
    info: ServerSettingInfo;
    value: string;
}

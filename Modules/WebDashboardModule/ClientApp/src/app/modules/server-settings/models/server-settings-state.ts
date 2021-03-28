import { ModuleData } from './module-data';
import { ServerSettingDataByKey } from './server-setting-data-by-key';

export interface ServerSettingsState {
    moduleDatas: ModuleData[];
    moduleSettings: { [key: string]: ServerSettingDataByKey };
    selectedModule: string;
}

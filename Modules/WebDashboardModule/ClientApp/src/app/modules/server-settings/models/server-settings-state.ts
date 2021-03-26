import { ModuleData } from './module-data';
import { ModuleSetting } from './module-setting';

export interface ServerSettingsState {
    moduleDatas: ModuleData[];
    moduleSettings: { [key: string]: ModuleSetting[] };
    selectedModule: string;
}

import { createAction, props } from '@ngrx/store';
import { ModuleData } from '../../models/module-data';
import { ModuleSetting } from '../../models/module-setting';

export const loadAllModuleDatas = createAction('[Server Settings] Load All Module Datas');
export const loadAllModuleDatasSuccess = createAction('[Server Settings] Load All Module Datas Success', props<{ moduleDatas: ModuleData[] }>());
export const loadAllModuleDatasFailure = createAction('[Server Settings] Load All Module Datas Failure', props<{ err: unknown }>());

export const loadModuleSettings = createAction('[Server Settings] Load Module Settings', props<{ moduleName: string }>());
export const loadModuleSettingsSuccess = createAction(
    '[Server Settings] Load Module Settings Success',
    props<{ moduleName: string; moduleSettings: ModuleSetting[] }>()
);
export const loadModuleSettingsFailure = createAction('[Server Settings] Load Module Settings Failure', props<{ err: unknown }>());

export const setModuleActive = createAction('[Server Settings] Set Module Active', props<{ moduleData: ModuleData }>());
export const setModuleActiveSuccess = createAction('[Server Settings] Set Module Active Success', props<{ moduleData: ModuleData }>());
export const setModuleActiveFailure = createAction('[Server Settings] Set Module Active Failure', props<{ err: unknown }>());

export const setModuleSetting = createAction('[Server Settings] Set Module Setting', props<{ moduleName: string; moduleSetting: ModuleSetting }>());
export const setModuleSettingSuccess = createAction(
    '[Server Settings] Set Module Setting Success',
    props<{ moduleName: string; moduleSetting: ModuleSetting }>()
);
export const setModuleSettingFailure = createAction('[Server Settings] Set Module Setting Failure', props<{ err: unknown }>());

export const selectModule = createAction('[Server Settings] Select Module', props<{ moduleName: string }>());

export const reloadModuleSettingsTriggered = createAction('[Server Settings] Reload Module Settings Triggered');

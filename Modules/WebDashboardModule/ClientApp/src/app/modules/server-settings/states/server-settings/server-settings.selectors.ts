import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ServerSettingsState } from '../../models/server-settings-state';
import { serverSettingsFeatureKey } from './server-settings.state-main';

export const selectState = createFeatureSelector<ServerSettingsState>(serverSettingsFeatureKey);

export const selectSelectedModule = createSelector(selectState, (state: ServerSettingsState) => state.selectedModule);
export const selectModuleDatas = createSelector(selectState, (state: ServerSettingsState) => state.moduleDatas);
export const selectModuleSettings = createSelector(
    selectState,
    selectSelectedModule,
    (state: ServerSettingsState, selectedModuleName: string) => state.moduleSettings[selectedModuleName]
);

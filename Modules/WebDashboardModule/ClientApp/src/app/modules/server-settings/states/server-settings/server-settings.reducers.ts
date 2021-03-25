import { createReducer, on } from '@ngrx/store';
import { ServerSettingsState } from '../../models/server-settings-state';
import * as Actions from './server-settings.actions';

const initialState: ServerSettingsState = {
    moduleDatas: [],
    moduleSettings: {},
};

export const serverSettingsReducers = createReducer(
    initialState,

    on(Actions.loadAllModuleDatasSuccess, (state, action) => ({ ...state, moduleDatas: action.moduleDatas })),
    on(Actions.loadModuleSettingsSuccess, (state, action) => ({
        ...state,
        moduleSettings: { ...state.moduleSettings, [action.moduleName]: action.moduleSettings },
    })),
    on(Actions.setModuleActiveSuccess, (state, action) => {
        const dataIndex = state.moduleDatas.findIndex((m) => m.name === action.moduleData.name);
        return {
            ...state,
            moduleDatas: [
                ...state.moduleDatas.filter((_, index) => index < dataIndex),
                action.moduleData,
                ...state.moduleDatas.filter((_, index) => index > dataIndex),
            ],
        };
    })
);

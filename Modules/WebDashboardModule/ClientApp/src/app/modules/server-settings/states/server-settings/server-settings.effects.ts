import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';
import { catchError, debounceTime, filter, map, mergeMap, withLatestFrom } from 'rxjs/operators';
import { GuildSelectionService } from 'src/app/modules/page/services/guild-selection.service';
import { ServerSettingsService } from '../../services/server-settings.service';
import * as ServerSettingsActions from './server-settings.actions';

@Injectable()
export class ServerSettingsEffects {
    loadAllModuleDatas$ = createEffect(() =>
        this.actions.pipe(
            ofType(ServerSettingsActions.loadAllModuleDatas),
            withLatestFrom(this.guildSelectionService.selectedGuildId$),
            mergeMap(([, guildId]) =>
                this.service.loadAllModuleDatas(guildId).pipe(
                    map((moduleDatas) => ServerSettingsActions.loadAllModuleDatasSuccess({ moduleDatas })),
                    catchError((err) => of(ServerSettingsActions.loadAllModuleDatasFailure({ err })))
                )
            )
        )
    );

    loadModuleSettings$ = createEffect(() =>
        this.actions.pipe(
            ofType(ServerSettingsActions.loadModuleSettings),
            debounceTime(200),
            withLatestFrom(this.guildSelectionService.selectedGuildId$),
            mergeMap(([action, guildId]) =>
                this.service.loadModuleSettings(guildId, action.moduleName).pipe(
                    map((moduleSettings) => ServerSettingsActions.loadModuleSettingsSuccess({ moduleName: action.moduleName, moduleSettings })),
                    catchError((err) => of(ServerSettingsActions.loadModuleSettingsFailure({ err })))
                )
            )
        )
    );

    setModuleActive$ = createEffect(() =>
        this.actions.pipe(
            ofType(ServerSettingsActions.setModuleActive),
            mergeMap((action) =>
                this.service.setModuleActive(action.moduleData).pipe(
                    map(() => ServerSettingsActions.setModuleActiveSuccess({ moduleData: action.moduleData })),
                    catchError((err) => of(ServerSettingsActions.setModuleActiveFailure({ err })))
                )
            )
        )
    );

    setModuleActiveSuccess$ = createEffect(() =>
        this.actions.pipe(
            ofType(ServerSettingsActions.setModuleActiveSuccess),
            filter(({ moduleData }) => moduleData.isActive),
            mergeMap(({ moduleData }) => of(this.store.dispatch(ServerSettingsActions.loadModuleSettings({ moduleName: moduleData.name })))),
            map(() => ServerSettingsActions.reloadModuleSettingsTriggered())
        )
    );

    setModuleSetting$ = createEffect(() =>
        this.actions.pipe(
            ofType(ServerSettingsActions.setModuleSetting),
            mergeMap((action) =>
                this.service.setModuleSetting(action.moduleName, action.settingKey, action.value, action.settingType).pipe(
                    map(() => ServerSettingsActions.setModuleSettingSuccess(action)),
                    catchError((err) => of(ServerSettingsActions.setModuleSettingFailure({ err })))
                )
            )
        )
    );

    setModuleSettingSuccess$ = createEffect(() =>
        this.actions.pipe(
            ofType(ServerSettingsActions.setModuleSettingSuccess),
            mergeMap(({ moduleName }) => of(this.store.dispatch(ServerSettingsActions.loadModuleSettings({ moduleName })))),
            map(() => ServerSettingsActions.reloadModuleSettingsTriggered())
        )
    );

    selectModule$ = createEffect(() =>
        this.actions.pipe(
            ofType(ServerSettingsActions.selectModule),
            mergeMap(({ moduleName }) => of(this.store.dispatch(ServerSettingsActions.loadModuleSettings({ moduleName })))),
            map(() => ServerSettingsActions.reloadModuleSettingsTriggered())
        )
    );

    constructor(
        private readonly actions: Actions,
        private readonly service: ServerSettingsService,
        private readonly guildSelectionService: GuildSelectionService,
        private readonly store: Store
    ) {}
}

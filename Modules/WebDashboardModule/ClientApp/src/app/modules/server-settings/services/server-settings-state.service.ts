import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { share, takeUntil } from 'rxjs/operators';
import { ServerSettingType } from '../enums/server-setting-type';
import * as Actions from '../states/server-settings/server-settings.actions';
import * as Selectors from '../states/server-settings/server-settings.selectors';

@Injectable()
export class ServerSettingsStateService implements OnDestroy {
    moduleDatas$ = this.store.select(Selectors.selectModuleDatas);
    selectedModule$ = this.store.select(Selectors.selectSelectedModule);
    moduleSettings$ = this.store.select(Selectors.selectModuleSettings).pipe(share());

    private destroySubject = new Subject();

    constructor(private readonly store: Store) {
        this.selectedModule$.pipe(takeUntil(this.destroySubject)).subscribe((moduleName) => this.store.dispatch(Actions.loadModuleSettings({ moduleName })));
    }

    ngOnDestroy() {
        this.destroySubject.next();
    }

    loadAllModuleDatas() {
        this.store.dispatch(Actions.loadAllModuleDatas());
    }

    setSetting(moduleName: string, settingKey: string, value: unknown, settingType: ServerSettingType) {
        this.store.dispatch(Actions.setModuleSetting({ moduleName, settingKey, value, settingType }));
    }

    setModuleActive(moduleName: string, isActive: boolean, canDisable: boolean) {
        this.store.dispatch(Actions.setModuleActive({ moduleData: { name: moduleName, isActive, canDisable } }));
    }

    selectModule(moduleName: string) {
        this.store.dispatch(Actions.selectModule({ moduleName }));
    }
}

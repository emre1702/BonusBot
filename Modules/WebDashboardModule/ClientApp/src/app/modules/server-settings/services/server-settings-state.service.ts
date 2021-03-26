import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import * as Actions from '../states/server-settings/server-settings.actions';
import * as Selectors from '../states/server-settings/server-settings.selectors';

@Injectable()
export class ServerSettingsStateService implements OnDestroy {
    moduleDatas$ = this.store.select(Selectors.selectModuleDatas);
    selectedModule$ = this.store.select(Selectors.selectSelectedModule);
    moduleSettings$ = this.store.select(Selectors.selectModuleSettings);

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

    setSetting(moduleName: string, name: string, value: string) {
        this.store.dispatch(Actions.setModuleSetting({ moduleName, moduleSetting: { name, value } }));
    }

    setModuleActive(moduleName: string, isActive: boolean) {
        this.store.dispatch(Actions.setModuleActive({ moduleData: { name: moduleName, isActive } }));
    }

    selectModule(moduleName: string) {
        this.store.dispatch(Actions.selectModule({ moduleName }));
    }
}

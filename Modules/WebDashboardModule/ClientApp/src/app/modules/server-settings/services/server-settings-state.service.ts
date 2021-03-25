import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { Action } from 'rxjs/internal/scheduler/Action';
import { switchMap, takeUntil } from 'rxjs/operators';
import * as Actions from '../states/server-settings/server-settings.actions';
import * as Selectors from '../states/server-settings/server-settings.selectors';

@Injectable()
export class ServerSettingsStateService implements OnDestroy {
    selectedModuleChanged = new Subject<string>();

    moduleDatas$ = this.store.select(Selectors.selectModuleDatas);
    moduleSettings$ = this.selectedModuleChanged.pipe(switchMap((moduleName) => this.store.select(Selectors.selectModuleSettings, { moduleName })));

    private destroySubject = new Subject();

    constructor(private readonly store: Store) {
        this.selectedModuleChanged
            .pipe(takeUntil(this.destroySubject))
            .subscribe((moduleName) => this.store.dispatch(Actions.loadModuleSettings({ moduleName })));
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
}

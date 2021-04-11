import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { interval, Subject } from 'rxjs';
import { repeat, takeUntil } from 'rxjs/operators';
import * as Actions from '../states/audio-settings/audio-settings.actions';

@Injectable()
export class AudioSettingsStateFacade implements OnDestroy {
    private destroySubject = new Subject();

    constructor(private readonly store: Store) {
        this.reloadStateEveryMs(10 * 1000);
        this.store.dispatch(Actions.loadAudioSettings());
    }

    ngOnDestroy() {
        this.destroySubject.next();
    }

    setVolume(volume: number) {
        this.store.dispatch(Actions.setVolume({ volume: volume }));
    }

    pause() {
        this.store.dispatch(Actions.pause());
    }

    resume() {
        this.store.dispatch(Actions.resume());
    }

    stop() {
        this.store.dispatch(Actions.stop());
    }

    join() {
        this.store.dispatch(Actions.join());
    }

    leave() {
        this.store.dispatch(Actions.leave());
    }

    private reloadStateEveryMs(ms: number) {
        interval(ms)
            .pipe(repeat(), takeUntil(this.destroySubject))
            .subscribe(() => this.store.dispatch(Actions.loadAudioSettings()));
    }
}

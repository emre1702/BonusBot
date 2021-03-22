import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { interval, merge, ReplaySubject, Subject } from 'rxjs';
import { distinctUntilChanged, mergeMap, takeUntil } from 'rxjs/operators';
import api from 'src/app/routes/api';

@Injectable()
export class AudioSettingsService implements OnDestroy {
    private destroySubject = new Subject();

    private volume = new ReplaySubject<number>();
    volume$ = merge(this.volume, interval(20 * 1000).pipe(mergeMap(() => this.httpClient.get<number>(api.get.command.volume)))).pipe(
        distinctUntilChanged(),
        takeUntil(this.destroySubject)
    );

    constructor(private readonly httpClient: HttpClient) {}

    ngOnDestroy() {
        this.destroySubject.next();
    }

    setVolume(volume: number) {
        this.httpClient.post(api.post.command.execute, volume);
    }
}

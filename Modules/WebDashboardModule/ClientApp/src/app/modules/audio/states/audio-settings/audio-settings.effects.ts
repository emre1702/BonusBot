import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap, withLatestFrom } from 'rxjs/operators';
import { GuildSelectionService } from 'src/app/modules/page/services/guild-selection.service';
import { AudioSettingsState } from '../../models/audio-settings-state';
import { AudioSettingsService } from '../../services/audio-settings.service';
import * as AudioSettingsActions from './audio-settings.actions';

@Injectable()
export class AudioSettingsEffects {
    setVolume$ = createEffect(() =>
        this.actions.pipe(
            ofType(AudioSettingsActions.setVolume),
            mergeMap((action) =>
                this.service.setVolume(action.volume).pipe(
                    map(() => AudioSettingsActions.setVolumeSuccess({ volume: action.volume })),
                    catchError((err) => of(AudioSettingsActions.setVolumeFailure({ err })))
                )
            )
        )
    );

    pause$ = createEffect(() =>
        this.actions.pipe(
            ofType(AudioSettingsActions.pause),
            mergeMap(() =>
                this.service.pause().pipe(
                    map(() => AudioSettingsActions.pauseSuccess()),
                    catchError((err) => of(AudioSettingsActions.pauseFailure({ err })))
                )
            )
        )
    );

    resume$ = createEffect(() =>
        this.actions.pipe(
            ofType(AudioSettingsActions.resume),
            mergeMap(() =>
                this.service.resume().pipe(
                    map(() => AudioSettingsActions.resumeSuccess()),
                    catchError((err) => of(AudioSettingsActions.resumeFailure({ err })))
                )
            )
        )
    );

    stop$ = createEffect(() =>
        this.actions.pipe(
            ofType(AudioSettingsActions.stop),
            mergeMap(() =>
                this.service.stop().pipe(
                    map(() => AudioSettingsActions.stopSuccess()),
                    catchError((err) => of(AudioSettingsActions.stopFailure({ err })))
                )
            )
        )
    );

    loadAudioSettings$ = createEffect(() =>
        this.actions.pipe(
            ofType(AudioSettingsActions.loadAudioSettings),
            withLatestFrom(this.guildSelectionService.selectedGuildId$),
            mergeMap(([, guildId]) =>
                this.service.loadAudioSettings(guildId).pipe(
                    map((audioSettings: AudioSettingsState) => AudioSettingsActions.loadAudioSettingsSuccess(audioSettings)),
                    catchError((err) => of(AudioSettingsActions.loadAudioSettingsFailure({ err })))
                )
            )
        )
    );

    constructor(
        private readonly actions: Actions,
        private readonly service: AudioSettingsService,
        private readonly guildSelectionService: GuildSelectionService
    ) {}
}

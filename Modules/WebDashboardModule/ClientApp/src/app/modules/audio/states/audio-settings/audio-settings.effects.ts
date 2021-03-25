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

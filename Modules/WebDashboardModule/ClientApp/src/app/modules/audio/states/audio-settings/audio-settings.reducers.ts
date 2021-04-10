import { createReducer, on } from '@ngrx/store';
import { AudioPlayerStatus } from '../../enums/audio-player-status';
import { AudioSettingsState } from '../../models/audio-settings-state';
import * as AudioSettingsActions from './audio-settings.actions';

const intialState: AudioSettingsState = {
    volume: 0,
};

export const audioSettingsReducer = createReducer(
    intialState,

    on(AudioSettingsActions.setVolumeSuccess, (state, action) => ({ ...state, volume: action.volume })),
    on(AudioSettingsActions.pauseSuccess, (state) => ({ ...state, status: AudioPlayerStatus.paused })),
    on(AudioSettingsActions.resumeSuccess, (state) => ({ ...state, status: AudioPlayerStatus.playing })),
    on(AudioSettingsActions.stopSuccess, (state) => ({ ...state, status: AudioPlayerStatus.stopped })),

    on(AudioSettingsActions.loadAudioSettingsSuccess, (_, newState) => newState)
);

import { createReducer, on } from '@ngrx/store';
import { AudioSettingsState } from '../../models/audio-settings-state';
import * as AudioSettingsActions from './audio-settings.actions';

const intialState: AudioSettingsState = {
    volume: 0,
};

export const audioSettingsReducer = createReducer(
    intialState,

    on(AudioSettingsActions.setVolumeSuccess, (state, action) => {
        return { ...state, volume: action.volume };
    }),

    on(AudioSettingsActions.loadAudioSettingsSuccess, (_, newState) => newState)
);

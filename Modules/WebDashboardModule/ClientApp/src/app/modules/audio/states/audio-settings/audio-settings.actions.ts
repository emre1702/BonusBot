import { createAction, props } from '@ngrx/store';
import { AudioSettingsState } from '../../models/audio-settings-state';

export const setVolume = createAction('[Audio Settings] Set Volume', props<{ volume: number }>());
export const setVolumeSuccess = createAction('[Audio Settings] Set Volume Success', props<{ volume: number }>());
export const setVolumeFailure = createAction('[Audio Settings] Set Volume Failure', props<{ err: unknown }>());

export const loadAudioSettings = createAction('[Audio Settings] Load Audio Settings');
export const loadAudioSettingsSuccess = createAction('[Audio Settings] Load Audio Settings Success', props<AudioSettingsState>());
export const loadAudioSettingsFailure = createAction('[Audio Settings] Load Audio Settings Failure', props<{ err: unknown }>());

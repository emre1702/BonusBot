import { createAction, props } from '@ngrx/store';
import { AudioSettingsState } from '../../models/audio-settings-state';

export const setVolume = createAction('[Audio Settings] Set Volume', props<{ volume: number }>());
export const setVolumeSuccess = createAction('[Audio Settings] Set Volume Success', props<{ volume: number }>());
export const setVolumeFailure = createAction('[Audio Settings] Set Volume Failure', props<{ err: unknown }>());

export const pause = createAction('[Audio Settings] Pause');
export const pauseSuccess = createAction('[Audio Settings] Pause Success');
export const pauseFailure = createAction('[Audio Settings] Pause Failure', props<{ err: unknown }>());

export const resume = createAction('[Audio Settings] Resume');
export const resumeSuccess = createAction('[Audio Settings] Resume Success');
export const resumeFailure = createAction('[Audio Settings] Resume Failure', props<{ err: unknown }>());

export const stop = createAction('[Audio Settings] Stop');
export const stopSuccess = createAction('[Audio Settings] Stop Success');
export const stopFailure = createAction('[Audio Settings] Stop Failure', props<{ err: unknown }>());

export const loadAudioSettings = createAction('[Audio Settings] Load Audio Settings');
export const loadAudioSettingsSuccess = createAction('[Audio Settings] Load Audio Settings Success', props<AudioSettingsState>());
export const loadAudioSettingsFailure = createAction('[Audio Settings] Load Audio Settings Failure', props<{ err: unknown }>());

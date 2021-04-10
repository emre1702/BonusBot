import { createFeatureSelector } from '@ngrx/store';
import { AudioSettingsState } from '../../models/audio-settings-state';
import { audioSettingsFeatureKey } from './audio-settings-state.main';

export const selectState = createFeatureSelector<AudioSettingsState>(audioSettingsFeatureKey);

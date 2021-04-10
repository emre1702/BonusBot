import { AudioPlayerStatus } from '../enums/audio-player-status';

export interface AudioSettingsState {
    volume: number;
    status?: AudioPlayerStatus;
}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AudioComponent } from './components/audio/audio.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { PlayQueueComponent } from './components/play-queue/play-queue.component';
import { PlayQueuePlaylistComponent } from './components/play-queue-playlist/play-queue-playlist.component';
import { AudioProviderComponent } from './components/selections/audio-provider/audio-provider.component';
import { AudioSettingsComponent } from './components/audio-settings/audio-settings.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { audioSettingsFeatureKey } from './states/audio-settings/audio-settings.state-main';
import { audioSettingsReducer } from './states/audio-settings/audio-settings.reducer';
import { AudioSettingsEffects } from './states/audio-settings/audio-settings.effects';
import { AudioSettingsService } from './services/audio-settings.service';

@NgModule({
    declarations: [AudioComponent, PlayQueueComponent, PlayQueuePlaylistComponent, AudioProviderComponent, AudioSettingsComponent],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        MaterialModule,
        StoreModule.forFeature(audioSettingsFeatureKey, audioSettingsReducer),
        EffectsModule.forFeature([AudioSettingsEffects]),
    ],
    providers: [AudioSettingsService],
    exports: [AudioComponent],
})
export class AudioModule {}

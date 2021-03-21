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

@NgModule({
    declarations: [AudioComponent, PlayQueueComponent, PlayQueuePlaylistComponent, AudioProviderComponent, AudioSettingsComponent],
    imports: [CommonModule, FormsModule, SharedModule, MaterialModule],
    exports: [AudioComponent],
})
export class AudioModule {}

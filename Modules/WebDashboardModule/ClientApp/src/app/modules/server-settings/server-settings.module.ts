import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServerSettingsComponent } from './components/server-settings/server-settings.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { StoreModule } from '@ngrx/store';
import { serverSettingsFeatureKey } from './states/server-settings/server-settings.state-main';
import { serverSettingsReducers } from './states/server-settings/server-settings.reducers';
import { EffectsModule } from '@ngrx/effects';
import { ServerSettingsEffects } from './states/server-settings/server-settings.effects';
import { ServerSettingsService } from './services/server-settings.service';
import { SharedModule } from '../shared/shared.module';
import { SettingsPageContainerComponent } from './components/page/settings-page-container/settings-page-container.component';
import { SettingStringOptionComponent } from './components/page/options/setting-string-option/setting-string-option.component';
import { SettingsPageRowComponent } from './components/page/settings-page-row/settings-page-row.component';

@NgModule({
    declarations: [ServerSettingsComponent, SettingStringOptionComponent, SettingsPageContainerComponent, SettingsPageRowComponent],
    imports: [
        CommonModule,
        FormsModule,
        MaterialModule,
        SharedModule,
        StoreModule.forFeature(serverSettingsFeatureKey, serverSettingsReducers),
        EffectsModule.forFeature([ServerSettingsEffects]),
    ],
    providers: [ServerSettingsService],
})
export class ServerSettingsModule {}

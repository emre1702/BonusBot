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
import { SettingBooleanOptionComponent } from './components/page/options/setting-boolean-option/setting-boolean-option.component';
import { SettingIntegerOptionComponent } from './components/page/options/setting-integer-option/setting-integer-option.component';
import { SettingChannelOptionComponent } from './components/page/options/setting-channel-option/setting-channel-option.component';
import { SettingRoleOptionComponent } from './components/page/options/setting-role-option/setting-role-option.component';
import { SettingEmoteOptionComponent } from './components/page/options/setting-emote-option/setting-emote-option.component';
import { SettingLocaleOptionComponent } from './components/page/options/setting-locale-option/setting-locale-option.component';
import { SettingTimezoneOptionComponent } from './components/page/options/setting-timezone-option/setting-timezone-option.component';
import { SettingIntegerSliderOptionComponent } from './components/page/options/setting-integerslider-option/setting-integerslider-option.component';

@NgModule({
    declarations: [
        ServerSettingsComponent,
        SettingStringOptionComponent,
        SettingsPageContainerComponent,
        SettingsPageRowComponent,
        SettingBooleanOptionComponent,
        SettingIntegerOptionComponent,
        SettingChannelOptionComponent,
        SettingRoleOptionComponent,
        SettingEmoteOptionComponent,
        SettingLocaleOptionComponent,
        SettingTimezoneOptionComponent,
        SettingIntegerSliderOptionComponent,
    ],
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

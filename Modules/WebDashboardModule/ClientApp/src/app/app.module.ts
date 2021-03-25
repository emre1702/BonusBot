import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PageModule } from './modules/page/page.module';
import { LoginComponent } from './modules/login/components/login/login.component';
import { LoginModule } from './modules/login/login.module';
import { DashboardComponent } from './modules/dashboard/dashboard/dashboard.component';
import { AudioComponent } from './modules/audio/components/audio/audio.component';
import { AudioModule } from './modules/audio/audio.module';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { ServerSettingsModule } from './modules/server-settings/server-settings.module';
import { ServerSettingsComponent } from './modules/server-settings/components/server-settings/server-settings.component';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'login', component: LoginComponent },
            { path: 'loginredirect', component: LoginComponent },
            { path: 'audio', component: AudioComponent },
            { path: 'settings', component: ServerSettingsComponent },

            { path: '**', component: DashboardComponent },
        ]),
        BrowserAnimationsModule,

        PageModule,
        LoginModule,
        AudioModule,
        ServerSettingsModule,

        StoreModule.forRoot({}),
        EffectsModule.forRoot(),
        StoreDevtoolsModule.instrument({
            maxAge: 25,
            logOnly: environment.production,
        }),
    ],
    providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true }],
    bootstrap: [AppComponent],
})
export class AppModule {}

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

            { path: '**', component: DashboardComponent },
        ]),
        BrowserAnimationsModule,

        PageModule,
        LoginModule,
        AudioModule,
        StoreModule.forRoot({}),
        EffectsModule.forRoot(),
    ],
    providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true }],
    bootstrap: [AppComponent],
})
export class AppModule {}

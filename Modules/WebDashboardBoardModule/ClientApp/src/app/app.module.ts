import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationModule } from './modules/navigation/navigation.module';
import { LoginComponent } from './modules/login/components/login/login.component';
import { LoginModule } from './modules/login/login.module';
import { DashboardComponent } from './modules/dashboard/dashboard/dashboard.component';
import { AudioComponent } from './modules/audio/components/audio/audio.component';
import { ContentModule } from './modules/content/content.module';
import { AudioModule } from './modules/audio/audio.module';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { SharedModule } from './modules/shared/shared.module';

@NgModule({
    declarations: [AppComponent, NavMenuComponent, CounterComponent, FetchDataComponent],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'login', component: LoginComponent },
            { path: 'loginredirect', component: LoginComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },

            { path: 'audio', component: AudioComponent },
            { path: '**', component: DashboardComponent },
        ]),
        BrowserAnimationsModule,

        NavigationModule,
        LoginModule,
        ContentModule,
        AudioModule,
    ],
    providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true }],
    bootstrap: [AppComponent],
})
export class AppModule {}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationModule } from './modules/navigation/navigation.module';
import { LoginComponent } from './modules/login/components/login/login.component';
import { LoginModule } from './modules/login/login.module';

@NgModule({
    declarations: [AppComponent, NavMenuComponent, HomeComponent, CounterComponent, FetchDataComponent],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'login', component: LoginComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },

            { path: '**', component: HomeComponent },
        ]),
        BrowserAnimationsModule,

        NavigationModule,
        LoginModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}

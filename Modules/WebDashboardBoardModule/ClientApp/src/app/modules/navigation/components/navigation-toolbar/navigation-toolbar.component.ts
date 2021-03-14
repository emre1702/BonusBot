import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import api from 'src/app/routes/api';
import { HttpService } from 'src/app/services/http.service';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-navigation-toolbar',
    templateUrl: './navigation-toolbar.component.html',
    styleUrls: ['./navigation-toolbar.component.scss'],
})
export class NavigationToolbarComponent {
    constructor(navigationService: NavigationService, router: Router, httpService: HttpService) {
        navigationService.navigateTo$.subscribe((url) => router.navigateByUrl(url));

        httpService.get(api.get.httpService.init).subscribe();
    }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import api from 'src/app/routes/api';
import { NavigationService } from '../../page/services/navigation.service';

@Injectable()
export class LoginService {
    constructor(private readonly httpClient: HttpClient, private readonly navigationService: NavigationService) {}

    signIn() {
        this.httpClient.get(api.get.login.oAuthUrl, { responseType: 'text' }).subscribe((url?: string) => {
            if (url) window.location.href = url;
            else this.navigationService.navigateToUrl('/dashboard');
        });
    }
}

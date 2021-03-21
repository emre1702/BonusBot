import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import api from 'src/app/routes/api';

@Injectable()
export class LoginService {
    constructor(private readonly httpClient: HttpClient) {}

    signIn() {
        this.httpClient.get(api.get.login.oAuthUrl, { responseType: 'text' }).subscribe((url: string) => (window.location.href = url));
    }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import api from 'src/app/routes/api';

@Injectable()
export class LoginService {
    private oAuthUrl = new ReplaySubject<string>();
    oAuthUrl$ = this.oAuthUrl.asObservable();

    constructor(httpClient: HttpClient) {
        httpClient.get(api.get.login.oAuthUrl, { responseType: 'text' }).subscribe((url: string) => this.oAuthUrl.next(url));
    }
}

import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import api from 'src/app/routes/api';
import { HttpService } from 'src/app/services/http.service';

@Injectable()
export class LoginService {
    private oAuthUrl = new ReplaySubject<string>();
    oAuthUrl$ = this.oAuthUrl.asObservable();

    constructor(httpService: HttpService) {
        httpService.getText(api.get.login.oAuthUrl).subscribe((url: string) => this.oAuthUrl.next(url));
    }
}

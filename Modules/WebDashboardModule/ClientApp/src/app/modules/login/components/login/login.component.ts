import { Component } from '@angular/core';
import { LoginService } from '../../services/login.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    providers: [LoginService],
})
export class LoginComponent {
    constructor(readonly service: LoginService) {}
}

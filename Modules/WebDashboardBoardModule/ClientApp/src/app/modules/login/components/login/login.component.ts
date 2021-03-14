import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    providers: [LoginService],
})
export class LoginComponent implements OnInit {
    constructor(readonly loginService: LoginService) {}

    ngOnInit() {}
}

import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NavigationService } from '../modules/page/services/navigation.service';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
    constructor(private readonly navigationService: NavigationService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((err: { status: number }) => {
                if (err.status == 401) {
                    this.navigationService.navigateToLogin();
                    return EMPTY;
                }
                return throwError(err);
            })
        );
    }
}

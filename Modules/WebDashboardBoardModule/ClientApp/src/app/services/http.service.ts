import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { NavigationService } from '../modules/navigation/services/navigation.service';

@Injectable({
    providedIn: 'root',
})
export class HttpService {
    constructor(private readonly httpClient: HttpClient, private readonly navigationService: NavigationService) {}

    get(url: string): Observable<any> {
        return this.httpClient.get(url).pipe(catchError((err) => this.catchError(err)));
    }

    getText(url: string): Observable<string> {
        return this.httpClient.get(url, { responseType: 'text' }).pipe(catchError((err) => this.catchError(err))) as Observable<string>;
    }

    private catchError(error: { status: number }) {
        if (error.status === 401) {
            this.navigationService.navigateToLogin();
            return of([]);
        }
        return throwError(error);
    }
}

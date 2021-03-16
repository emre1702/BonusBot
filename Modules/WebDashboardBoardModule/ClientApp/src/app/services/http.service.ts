import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { NavigationService } from '../modules/navigation/services/navigation.service';

@Injectable({
    providedIn: 'root',
})
export class HttpService {
    constructor(private readonly httpClient: HttpClient, private readonly navigationService: NavigationService) {}

    get<T>(url: string, options?: HttpOptions): Observable<any> {
        return this.httpClient.get<T>(url, options).pipe(catchError((err) => this.catchError(err)));
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

export interface HttpOptions {
    headers?:
        | HttpHeaders
        | {
              [header: string]: string | string[];
          };
    observe?: 'body';
    params?:
        | HttpParams
        | {
              [param: string]: string | string[];
          };
    reportProgress?: boolean;
    responseType?: 'json';
    withCredentials?: boolean;
}

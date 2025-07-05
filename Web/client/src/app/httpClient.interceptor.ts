import { throwError as observableThrowError, Observable, BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { catchError, finalize, map } from 'rxjs/operators';
import { environment } from '../environments/environment';
@Injectable()
export class HTTPStatus {
    private requestInFlight$: BehaviorSubject<boolean> | any;

    constructor() {

        this.requestInFlight$ = new BehaviorSubject(false);
    }

    setHttpStatus(inFlight: boolean) {
        this.requestInFlight$.next(inFlight);
    }

    getHttpStatus(): Observable<boolean> {
        return this.requestInFlight$.asObservable();
    }
}

@Injectable()
export class HttpClientInterceptor implements HttpInterceptor {

    public constructor(private status: HTTPStatus) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let endpointUrl = req.url;

        if (!req.url.includes(environment.BOLink.substring(0, environment.BOLink.length))) {
            endpointUrl = environment.BOLink + '/api/' + req.url;
        }
        
        this.status.setHttpStatus(true);
        const authReq = req.clone({
            headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token')),
            reportProgress: true,
            url: endpointUrl
        });
        return next.handle(authReq)
            .pipe(
                map(event => {
                    return event;
                }),
                catchError(error => {
                    return observableThrowError(error);
                }),
                finalize(() => {
                    this.status.setHttpStatus(false);
                })
            );
    }

}

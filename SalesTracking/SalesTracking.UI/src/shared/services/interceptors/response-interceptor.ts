import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Observable, catchError, map, tap, throwError } from "rxjs";
import { LogoutHandlerService } from "../global/logout-handler.service";
import { AuthTokenHandlerService } from "../global/authenciation-token-handler";
import { Router } from "@angular/router";
import { NotificationService } from "../global/notification-service";
import { Injectable } from "@angular/core";

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {

    constructor(private logoutService: LogoutHandlerService, private authtoken: AuthTokenHandlerService,
        private router: Router, private notificationService: NotificationService) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            tap(evt => {
                if (evt instanceof HttpResponse) { }
            }),
            catchError(err => {
                if (err.status === 0) {
                    this.notificationService.showCustomMsg('Connection Error', 'Connection error please contact adminstrator.', 'error');
                }
                else if (err.status === 401 && this.authtoken.isTokenExpired()) {
                    this.notificationService.showCustomMsg('Session Expired', 'Your session will be refreshed now.', 'error');
                    this.logoutService.logout();
                } else {
                    this.router.navigate(['./dashboard', {}]);
                }
                const error = err.error.message || err.statusText;
                return throwError(() => error);
            }), map(event => {
                if (event instanceof HttpResponse) {
                    if (event.body.isError == true && event.body.statusCode == 403) { this.router.navigate(['./dashboard', {}]); }
                }
                return event;
            }));
    }
}
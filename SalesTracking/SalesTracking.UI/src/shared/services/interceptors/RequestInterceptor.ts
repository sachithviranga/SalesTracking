import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { StorageHandlerService } from '../global/StorageHandlerService';
import { Configuration } from '../../utilities/configuration';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {

    constructor(private storageHandler: StorageHandlerService, private configObj: Configuration) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authHeader = '';
        let token = this.storageHandler.getLocalStorage('id_token');

        if (!request.params.has('isBasiAutherization') && token) {
            authHeader = 'Bearer ' + token;
        }

        request = request.clone({
            setHeaders: {
                'Authorization': authHeader,
                'Content-Type': 'application/json', 
                'If-Modified-Since': 'Mon, 26 Jul 1997 05:00:00 GMT',
                'Cache-Control': 'no-cache',
                'Pragma': 'no-cache',
            },
            url:request.url
        });

        return next.handle(request);
    }
}


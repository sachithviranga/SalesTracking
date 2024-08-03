import { Injectable } from '@angular/core';
import { HttpClient,  HttpParams } from '@angular/common/http';
import { Configuration } from '../utilities/configuration';
import { Observable , throwError   } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable()
export class DataService {
    [x: string]: any;

    private restServerURL: string;

    /**
     * Constructor.
     */
    constructor(private http: HttpClient, public _configuration: Configuration) {

        this.restServerURL = _configuration.baseServerURL;
    }


    handleError(error: Response) {
        return throwError(error.statusText);
    }

    /**
    * get all by get.
    */
    public getAllByGet<t>(actionUrl: any, param?: any, authUrl?: string): Observable<t> {
        var baseUrl: string = authUrl ? authUrl : this.restServerURL;
        const endpointUrl: string = baseUrl + actionUrl;
        return this.http.get<t>(endpointUrl, { params: param })
            .pipe(
                map((response: t) => { return response; }),
                catchError(this.handleError)
            );
    }

    ///**
    // * Get all by reference data by post.
    // */
    public getAllByGetWithJson<T>(actionurl: any, param?: any): Observable<T> {
        //const endpointUrl: string = this._configuration.restServerURL + this._configuration.referenceData;
        return this.http.get<T>(actionurl + JSON.stringify(param))
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );

    }

    ///**
    // * Get all by Post.
    // */
    public getAllByPost<T>(actionUrl: any, params: any, authUrl?: string): Observable<T> {
        var baseUrl: string = authUrl ? authUrl : this.restServerURL;
        const endpointUrl: string = baseUrl + actionUrl;

        return this.http.post<T>(endpointUrl, params)
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );
    }

    /**
   * Add.
   */
    public add<T>(actionUrl: any, object: any): Observable<T> {
        const endpointUrl: string = this._configuration.restServerURL + actionUrl;
        return this.http.post<T>(endpointUrl, object)
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );
    }


    public PostRequestWithBasicAuth<T>(actionUrl: any, params: any, authUrl?: string): Observable<T> {
        var baseUrl: string = authUrl ? authUrl : this.restServerURL;
        const endpointUrl: string = baseUrl + actionUrl;

        //const myObject: any = { isBasiAutherization: true };
        //const httpParams: HttpParamsOptions = { fromObject: myObject } as HttpParamsOptions;


        const httpParams = new HttpParams()
            .set('isBasiAutherization', 'true');


        return this.http.post<T>(endpointUrl, params, { params: httpParams })
            .pipe(
                map((response: T) => { return response; }),
                catchError(this.handleError)
            );
    }

 
}

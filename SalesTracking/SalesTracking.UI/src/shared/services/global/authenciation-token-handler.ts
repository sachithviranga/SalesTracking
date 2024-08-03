import { Injectable } from "@angular/core";
import { StorageHandlerService } from "./StorageHandlerService";
import { NgxPermissionsService } from "ngx-permissions";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthTokenHandlerService {

    constructor(private storageService: StorageHandlerService, public permissionsService: NgxPermissionsService) {

    }

    public setTokens(body: any): void {
        if (typeof body.accessToken !== 'undefined' && body.accessToken !== null) {

            this.storageService.setLocalStorage('id_token', body.accessToken);
            this.storageService.setLocalStorage('refresh_token', body.refreshToken);


            //this.setUserInfo();

            // Calculates token expiration.
            let expiryTime = body.expiresIn as number * 1000;
            this.storageService.setLocalStorage('exp', expiryTime.toString());
        }
    }

    public setUserInfo(): void {

        const token = this.storageService.getLocalStorage('id_token');
        if (token !== null) {
            const helper = new JwtHelperService();
            const decodedToken = helper.decodeToken(token);
            //const decodedIdToken = jwt_decode(token);
            this.permissionsService.flushPermissions();

            if (decodedToken.Permission != undefined && decodedToken.Permission != null) {
                for (var i = 0; i < decodedToken.Permission.length; i++) {
                    this.permissionsService.addPermission(decodedToken.Permission[i]);
                }
            }
        }
    }

    public isTokenExpired() {

        var token = this.storageService.getLocalStorage('id_token');
        if (token != null) {
            const helper = new JwtHelperService();
            return helper.isTokenExpired(token);
        }
        return true;
    }
}


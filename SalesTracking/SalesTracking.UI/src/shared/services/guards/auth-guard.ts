import { Injectable, inject } from "@angular/core";
import { StorageHandlerService } from "../global/StorageHandlerService";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { AuthTokenHandlerService } from "../global/authenciation-token-handler";

@Injectable({ providedIn: 'root' })
class PermissionsService {

    constructor(
        private storageHandlerService: StorageHandlerService,
        public router: Router,
        public authTokenHandlerService: AuthTokenHandlerService) {
        this.authTokenHandlerService.setUserInfo();
    }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.checkValidity();
    }

    checkValidity() {

        //token not found
        if (this.storageHandlerService.getLocalStorage('id_token') == null) {
            this.router.navigate(['/auth/login']);
            return false;
        }

        //token expired
        if (this.authTokenHandlerService.isTokenExpired()) {
            this.router.navigate(['/auth/login']);
            return false;
        }

        return true;

    }
}

export const AuthGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
    return inject(PermissionsService).canActivate(next, state);
}
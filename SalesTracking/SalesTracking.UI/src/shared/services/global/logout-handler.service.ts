import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { StorageHandlerService } from './StorageHandlerService';
import { BlockUI, NgBlockUI } from 'ng-block-ui';


@Injectable()
export class LogoutHandlerService {

    @BlockUI() blockUI: NgBlockUI;

    constructor(private storageService: StorageHandlerService, private router: Router) {

    }

    logout(isForcedLogOut?: boolean) {
        this.blockUI.start('Please Wait...');
        this.storageService.removeLocalStorage('id_token');
        this.storageService.removeLocalStorage('refresh_token');
        this.storageService.removeLocalStorage('exp');
        if (isForcedLogOut) {
            this.router.navigate(['./auth', { isForcedLogOut: isForcedLogOut }]);
        }
        else {
            this.router.navigate(['./auth', {}]);
        }
        this.blockUI.stop();
    }

}
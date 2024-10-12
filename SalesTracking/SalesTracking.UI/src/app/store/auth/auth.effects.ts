import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { AuthService } from 'src/open-api-client/api/api';
import { login, loginSuccess, loginFailure } from './auth.actions';
import { catchError, finalize, map, mergeMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Router } from '@angular/router';
import { AuthTokenHandlerService } from 'src/shared/services/global/authenciation-token-handler';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Injectable()
export class AuthEffects {
    @BlockUI() blockUI: NgBlockUI;
    login$ = createEffect(() =>
        this.actions$.pipe(
            ofType(login),
            mergeMap((action) =>
                this.authService.apiAuthLoginPost(action.loginDTO).pipe(
                    map((response) => loginSuccess({ loginResponse: response })),
                    catchError((error) => of(loginFailure({ error }))) ,
                    finalize(()=>{ this.blockUI.stop()})
                )
            )
        )
    );

    loginSuccess$ = createEffect(() =>
        this.actions$.pipe(
            ofType(loginSuccess),
            tap(({ loginResponse }) => {
                if (loginResponse.canLogin) {
                    this.authTokenService.setTokens(loginResponse);
                    this.router.navigate(['/dashboard']);
                }
            })
        ),
        { dispatch: false }
    );

    // logout$ = createEffect(() =>
    //     this.actions$.pipe(
    //         ofType(logout),
    //         tap(() => {
    //             localStorage.removeItem('token');
    //             this.router.navigate(['/login']);
    //         })
    //     ),
    //     { dispatch: false }
    // );

    //     loginSuccess$ = createEffect(() =>
    //         this.actions$.pipe(
    //           ofType(AuthActions.loginSuccess),
    //           tap(({loginResponse}) =>{
    //             //if (loginResponse.canLogin) {
    //                 //this.authTokenService.setTokens(loginResponse);
    //                 this.router.navigate(['/dashboard'])
    //               //}
    //              //this.router.navigate(['/dashboard']))
    // }),
    //         { dispatch: false }
    //       );

    constructor(
        private actions$: Actions,
        private authService: AuthService,
        private router: Router,
        private authTokenService: AuthTokenHandlerService
    ) { }
}

import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { DashboardService } from 'src/open-api-client/api/api';

import { catchError, finalize, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { loadAvailableProdcuts, loadAvailableProdcutsFailure, loadAvailableProdcutsSuccess } from './dashboard.action';

@Injectable()
export class DashboardEffects {
    @BlockUI() blockUI: NgBlockUI;
    loadAvailbleProducts$ = createEffect(() =>
        this.actions$.pipe(
            ofType(loadAvailableProdcuts),
            mergeMap(() =>
                this.service.apiDashboardGetAvailableProdcutsGet().pipe(
                    map((response) => loadAvailableProdcutsSuccess({ prodcuts : response })),
                    catchError((error) => of(loadAvailableProdcutsFailure({ error }))) ,
                    finalize(()=>{ this.blockUI.stop()})
                )
            )
        )
    );

    constructor(
        private actions$: Actions,
        private  service : DashboardService    ) { }
}

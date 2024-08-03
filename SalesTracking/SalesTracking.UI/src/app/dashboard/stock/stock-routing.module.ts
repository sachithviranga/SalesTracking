import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockLandingComponent } from './landing/landing.component';
import { StockCreateComponent } from './create/create.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { PermissionList } from 'src/shared/model/permission.model';

const stockRoutes: Routes =
    [
        {
            path: 'landing',
            component: StockLandingComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.StockLanding, PermissionList.StockView, PermissionList.StockAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        {
            path: 'create',
            component: StockCreateComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.StockView, PermissionList.StockAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        { path: '', redirectTo: 'landing', pathMatch: 'full' },
    ];

@NgModule({
    imports: [RouterModule.forChild(stockRoutes)],
    exports: [RouterModule]
})
export class StockRoutingModule { }
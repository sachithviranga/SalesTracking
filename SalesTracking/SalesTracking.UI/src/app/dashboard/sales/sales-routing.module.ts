import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalesLandingComponent } from './landing/landing.component';
import { SalesCreateComponent } from './create/create.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { PermissionList } from 'src/shared/model/permission.model';

const salesRoutes: Routes =
    [
        {
            path: 'landing',
            component: SalesLandingComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.SalesLanding, PermissionList.SalesView, PermissionList.SalesAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        {
            path: 'create',
            component: SalesCreateComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.SalesView, PermissionList.SalesAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        { path: '', redirectTo: 'landing', pathMatch: 'full' },
    ];

@NgModule({
    imports: [RouterModule.forChild(salesRoutes)],
    exports: [RouterModule]
})
export class SalesRoutingModule { }
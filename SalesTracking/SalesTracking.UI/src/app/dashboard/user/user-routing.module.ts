import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLandingComponent } from './landing/landing.component';
import { UserCreateComponent } from './create/create.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { PermissionList } from 'src/shared/model/permission.model';

const userRoutes: Routes =
    [
        {
            path: 'landing',
            component: UserLandingComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.UserLanding, PermissionList.UserView, PermissionList.UserAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        {
            path: 'create',
            component: UserCreateComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.UserView, PermissionList.UserAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        { path: '', redirectTo: 'landing', pathMatch: 'full' },
    ];

@NgModule({
    imports: [RouterModule.forChild(userRoutes)],
    exports: [RouterModule]
})
export class UserRoutingModule { }
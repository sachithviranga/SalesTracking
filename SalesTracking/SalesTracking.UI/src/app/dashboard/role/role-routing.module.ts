import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleLandingComponent } from './landing/landing.component'
import { RoleCreateComponent } from './create/create.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { PermissionList } from 'src/shared/model/permission.model';

const roleRoutes: Routes =
    [
        {
            path: 'landing',
            component: RoleLandingComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.RoleLanding, PermissionList.RoleView, PermissionList.RoleAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        {
            path: 'create',
            component: RoleCreateComponent,
            canActivate: [NgxPermissionsGuard],
            data: {
                permissions: {
                    only: [PermissionList.RoleView, PermissionList.RoleAdd],
                    redirectTo: 'dashboard'
                }
            }
        },
        { path: '', redirectTo: 'landing', pathMatch: 'full' },
    ];

@NgModule({
    imports: [RouterModule.forChild(roleRoutes)],
    exports: [RouterModule]
})
export class RoleRoutingModule { }
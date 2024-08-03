import { NgModule } from '@angular/core';
import { RoleComponent } from './role.component';
import { RoleRoutingModule } from './role-routing.module';
import { RoleCreateComponent } from './create/create.component';
import { RoleLandingComponent } from './landing/landing.component';
import { SharedModule } from 'src/shared/shared.module';
import { NgxPermissionsModule } from 'ngx-permissions';

@NgModule({
  declarations: [
    RoleComponent ,
    RoleCreateComponent,
    RoleLandingComponent
  ],
  imports: [
    RoleRoutingModule,
    SharedModule,
    NgxPermissionsModule.forChild()
  ]
})
export class RoleModule { }

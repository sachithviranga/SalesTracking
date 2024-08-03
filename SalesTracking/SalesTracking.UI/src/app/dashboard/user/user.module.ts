import { NgModule } from '@angular/core';
import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { SharedModule } from 'src/shared/shared.module';
import { UserCreateComponent } from './create/create.component';
import { UserLandingComponent } from './landing/landing.component';
import { NgxPermissionsModule } from 'ngx-permissions';

@NgModule({
  declarations: [
    UserComponent,
    UserCreateComponent,
    UserLandingComponent,
  ],
  imports: [
    NgxPermissionsModule.forChild(),
    UserRoutingModule,
    SharedModule ,
  ]
})
export class UserModule { }

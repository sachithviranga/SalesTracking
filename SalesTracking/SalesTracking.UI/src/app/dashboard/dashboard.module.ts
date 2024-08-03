import { NgModule } from '@angular/core';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { SharedModule } from 'src/shared/shared.module';
import { NgxPermissionsModule } from 'ngx-permissions';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    DashboardComponent,
    HomeComponent,
  ],
  imports: [
    NgxPermissionsModule.forChild(),
    DashboardRoutingModule ,
    SharedModule
  ]
})
export class DashboardModule { }

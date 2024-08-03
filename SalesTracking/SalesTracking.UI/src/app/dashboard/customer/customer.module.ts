import { NgModule } from '@angular/core';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { SharedModule } from 'src/shared/shared.module';
import { CustomerCreateComponent } from './create/create.component';
import { CustomerLandingComponent } from './landing/landing.component';


@NgModule({
  declarations: [
    CustomerComponent,
    CustomerCreateComponent,
    CustomerLandingComponent
  ],
  imports: [
    CustomerRoutingModule,
    SharedModule
  ]
})
export class CustomerModule { }

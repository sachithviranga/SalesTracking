import { NgModule } from '@angular/core';
import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';
import { NgxPermissionsModule } from 'ngx-permissions';
import { ProductCreateComponent } from './create/create.component';
import { ProductLandingComponent } from './landing/landing.component';
import { SharedModule } from 'src/shared/shared.module';

@NgModule({
  declarations: [
    ProductComponent,
    ProductCreateComponent,
    ProductLandingComponent
  ],
  imports: [
    NgxPermissionsModule.forChild(),
    ProductRoutingModule,
    SharedModule
  ]
})
export class ProductModule { }

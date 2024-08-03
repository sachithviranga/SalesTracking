import { NgModule } from '@angular/core';
import { SalesRoutingModule } from './sales-routing.module';
import { SalesComponent } from './sales.component';
import { SharedModule } from 'src/shared/shared.module';
import { SalesCreateComponent } from './create/create.component';
import { SalesLandingComponent } from './landing/landing.component';
import { NgxPermissionsModule } from 'ngx-permissions';

@NgModule({
    declarations: [
        SalesComponent,
        SalesCreateComponent,
        SalesLandingComponent,
    ],
    imports: [
        NgxPermissionsModule.forChild(),
        SalesRoutingModule,
        SharedModule,
    ]

})

export class SalesModule { }

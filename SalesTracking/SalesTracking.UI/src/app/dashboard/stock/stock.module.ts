import { NgModule } from '@angular/core';
import { StockRoutingModule } from './stock-routing.module';
import { StockComponent } from './stock.component';
import { SharedModule } from 'src/shared/shared.module';
import { StockCreateComponent } from './create/create.component';
import { StockLandingComponent } from './landing/landing.component';
import { NgxPermissionsModule } from 'ngx-permissions';

@NgModule({
    declarations: [
        StockComponent,
        StockCreateComponent,
        StockLandingComponent,
    ],
    imports: [
        NgxPermissionsModule.forChild(),
        StockRoutingModule,
        SharedModule,
    ]

})

export class StockModule { }

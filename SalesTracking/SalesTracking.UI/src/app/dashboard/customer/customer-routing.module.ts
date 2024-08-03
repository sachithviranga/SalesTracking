import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerLandingComponent  } from './landing/landing.component';
import { CustomerCreateComponent } from './create/create.component';
//import { HomeComponent } from './home/home.component';



const customerRoutes: Routes =
    [
         {
             path: 'landing',
             component: CustomerLandingComponent
         },
         {
          path: 'create',
          component: CustomerCreateComponent
      },

         { path: '', redirectTo: 'landing', pathMatch: 'full' },
    ];

@NgModule({
  imports: [RouterModule.forChild(customerRoutes)],
  exports: [RouterModule]   
})
export class CustomerRoutingModule { }
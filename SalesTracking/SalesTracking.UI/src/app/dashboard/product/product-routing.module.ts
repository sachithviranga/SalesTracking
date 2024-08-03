import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductLandingComponent  } from './landing/landing.component'
import { NgxPermissionsGuard } from 'ngx-permissions';
import { PermissionList } from 'src/shared/model/permission.model';
import { ProductComponent } from './product.component';
import { ProductCreateComponent } from './create/create.component';
//import { HomeComponent } from './home/home.component';



const productRoutes: Routes =
    [
      {
        path:'landing',
        component: ProductLandingComponent},

        {
          path: 'create',
          component: ProductCreateComponent
        },
        
        { 
          path: '', redirectTo: 'landing', pathMatch: 'full' },
        // children :[ {
        //      path: 'landing',
        //      component: ProductLandingComponent,
        //      canActivateChild :[NgxPermissionsGuard],
        //      data: {
        //       permissions: {
        //         only: [PermissionList.ProductLanding, PermissionList.ProductAdd, PermissionList.ProductView],
        //         redirectTo: 'auth'
        //       }
        //     }
        //  },
        //  { path: '', redirectTo: 'landing', pathMatch: 'full' },
        // ]}
    ];

@NgModule({
  imports: [RouterModule.forChild(productRoutes)],
  exports: [RouterModule]   
})
export class ProductRoutingModule { }
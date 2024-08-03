import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { PermissionList } from 'src/shared/model/permission.model';
import { DashboardComponent } from './dashboard.component';
import { RouterModule, Routes } from '@angular/router';
const dashboardRoutes: Routes =
  [
    { 
      path: '',
      component: DashboardComponent,
      children: [
        { path: '', redirectTo: 'home', pathMatch: 'full' },
        {
          path: 'home', title: 'Sales Tracking - Dashboard',
          component: HomeComponent
        },
        {
          path: 'customer', title: 'Sales Tracking - Customer',
          loadChildren: () => import('./customer/customer.module').then(m => m.CustomerModule)
        },
        {
          path: 'user', title: 'Sales Tracking - User',
          loadChildren: () => import('./user/user.module').then(m => m.UserModule)
        },
        {
          path: 'role', title: 'Sales Tracking - Role',
          loadChildren: () => import('./role/role.module').then(m => m.RoleModule)
        }, 
        { 
          path: 'product', title: 'Sales Tracking - Product',
          loadChildren: () => import('./product/product.module').then(m => m.ProductModule)
        },
        {
          path: 'stock', title: 'Sales Tracking - Stock',
          loadChildren: () => import('./stock/stock.module').then(m => m.StockModule)
        },
        {
          path: 'sales', title: 'Sales Tracking - Sales',
          loadChildren: () => import('./sales/sales.module').then(m => m.SalesModule)
        }
      ]
    }

  ];

@NgModule({
  imports: [RouterModule.forChild(dashboardRoutes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
import { Routes } from '@angular/router';
import { IndexComponent } from './index';
import { AppLayout } from './admin/layouts/app-layout';
import { AuthLayout } from './admin/layouts/auth-layout';
import { CustomerComponent } from './admin/layouts/components/customer/customer.component';
import { OrderComponent } from './admin/layouts/components/order/order.component';
import { ProductComponent } from './admin/layouts/components/product/product.component';
import { TodoListComponent } from './admin/layouts/components/todo-list/todo-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'admin/dashboard',
    pathMatch: 'full' 
  },
  {
    path: '',
    component: AppLayout,
    children: [
      { path: 'admin/dashboard', component: IndexComponent}, 
      { path: 'admin/dashboard/customer', component: CustomerComponent},
      { path: 'admin/dashboard/order', component: OrderComponent},
      { path: 'admin/dashboard/product', component: ProductComponent},
      { path: 'admin/todo/list', component: TodoListComponent}
    ],
  },
  {
    path: '',
    component: AuthLayout,
    children: [],
  },
];
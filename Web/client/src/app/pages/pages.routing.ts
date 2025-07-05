// src/app/pages/pages.routing.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PagesComponent } from './pages.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UsersComponent } from './users/users.component';
import { HomeComponent } from './home/home.component';

const pageRoutes: Routes = [
  {
    path: 'pages',
    component: PagesComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      {
        path: 'users',
        loadChildren: () => import('./users/users.module').then(m => m.UsersModule)
      },
      {
        path: 'warehouse',
        loadChildren: () => import('./warehouse/warehouse.module').then(m => m.WarehouseModule)
      },
      { path: 'home', component: HomeComponent },
      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(pageRoutes)],
  exports: [RouterModule]
})
export class PagesRoutingModule {}

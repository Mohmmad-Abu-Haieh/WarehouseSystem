// src/app/pages/pages.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutingModule } from './pages.routing';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { PagesComponent } from './pages.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    PagesComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule,         // ضروري لـ routerLink/router-outlet
    PagesRoutingModule ,
    SharedModule,
   // يحتوي على توجيه children
  ]
})
export class PagesModule {}

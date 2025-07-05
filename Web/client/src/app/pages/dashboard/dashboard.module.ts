// This module is no longer needed. Please delete this file.
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DashboardRoutingModule } from './dashboard.routing';
import { DashboardComponent } from './dashboard.component';
import { DashboardService } from './dashboard.service';



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    DashboardRoutingModule,
  ],
  declarations: [
    DashboardComponent,
  ],
  providers: [DashboardService]
})
export class DashboardModule { }

// This module is no longer needed. Please delete this file.
import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DashboardRoutingModule } from './dashboard.routing';
import { DashboardComponent } from './dashboard.component';
import { DashboardService } from './dashboard.service';
import { NgChartsModule } from 'ng2-charts';



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    DashboardRoutingModule,
    NgChartsModule 
  ],
  declarations: [
    DashboardComponent,
  ],
  providers: [DashboardService,DatePipe ]
})
export class DashboardModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatDialogModule } from '@angular/material/dialog';
import { NgSelectModule } from '@ng-select/ng-select';
import { LogsComponent } from './serilog.component';
import { LogsService } from './serilog.service';
import { LogsRoutingModule } from './serilog.routing';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    LogsRoutingModule,
    NgxPaginationModule,
    MatDialogModule,
    NgSelectModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule
  ],
  declarations: [
    LogsComponent,
  ],
  providers: [LogsService]
})
export class LogsModule { }

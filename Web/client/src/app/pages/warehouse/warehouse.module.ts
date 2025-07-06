import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatDialogModule } from '@angular/material/dialog'; 
import { NgSelectModule } from '@ng-select/ng-select';
import { WarehouseComponent } from './warehouse.component';
import { WarehouseRoutingModule } from './warehouse.routing';
import { WarehouseService } from './warehouse.service';
import { CreateWarehouseComponent } from './create-warehouse/create-warehouse.component';
import { ItemsWarehouseComponent } from './items-warehouse/items-warehouse.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    WarehouseRoutingModule,
    NgxPaginationModule,
    MatDialogModule,
    NgSelectModule
  ],
  declarations: [
    WarehouseComponent,
    CreateWarehouseComponent,
    ItemsWarehouseComponent
  ],
  providers: [WarehouseService]
})
export class WarehouseModule { }

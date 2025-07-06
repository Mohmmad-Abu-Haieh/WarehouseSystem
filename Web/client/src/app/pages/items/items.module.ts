import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatDialogModule } from '@angular/material/dialog'; 
import { NgSelectModule } from '@ng-select/ng-select';
import { ItemsRoutingModule } from './items.routing';
import { ItemsService } from './items.service';
import { ItemsComponent } from './items.component';
import { CreateItemComponent } from './create-item/create-item.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ItemsRoutingModule,
    NgxPaginationModule,
    MatDialogModule,
    NgSelectModule
  ],
  declarations: [
    ItemsComponent,
    CreateItemComponent
  ],
  providers: [ItemsService]
})
export class ItemsModule { }

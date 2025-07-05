import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UsersRoutingModule } from './users.routing';

import { UsersComponent } from './users.component';
import { UsersService } from './users.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatDialogModule } from '@angular/material/dialog'; 
import { CreateUserComponent } from './create-user/create-user.component';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    UsersRoutingModule,
    NgxPaginationModule,
    MatDialogModule,
    NgSelectModule
  ],
  declarations: [
    UsersComponent,
    CreateUserComponent
  ],
  providers: [UsersService]
})
export class UsersModule { }

import {Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { WarehouseService } from '../warehouse.service';


@Component({
  selector: 'app-create-warehouse',
  templateUrl: './create-warehouse.component.html',
  styleUrls: ['./create-warehouse.component.css']
})
export class CreateWarehouseComponent implements OnInit, OnDestroy {
  roles : any[] = [];
  userId: any;
  public model: any = {
        id: null,
        fullName: '',
        email: '',
        mobile: '',
        password: '',
        roleId: null
    };
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<CreateWarehouseComponent>, 
    public warehouseService: WarehouseService
    ) {
          this.userId = data.rowId;
          this.warehouseService.GetUsersFormData().then((res: any) => {
            debugger;
            this.roles = res;
          }).catch((err : any) => {
            console.error('Failed to load form data', err);
          });
      }
  ngOnInit(): void {
    if (this.userId) {
      this.loadUserForEdit(this.userId);  
    }
  }
  onCreateUser(form: NgForm) {
    if (form.invalid) {
      return;
    }
    this.warehouseService.CreateUser(this.model).then((success: any) => {
      if (success) {
      } else {
        alert("فشل تسجيل الدخول");
        localStorage.clear();
      }
    });
    console.log('User created:', this.model);
    this.dialogRef.close(this.model);
  }
  loadUserForEdit(userId: number) {
  this.warehouseService.GetUserDetails(userId).then((res: any) => {
    debugger;
    this.model = res;
  }).catch((err : any) => {
    console.error('Failed to load user', err);
  });
}
ngOnDestroy(): void {
  this.model = {
      id: null,
      fullName: '',
      email: '',
      mobile: '',
      password: '',
      roleId: null
  };
}
}




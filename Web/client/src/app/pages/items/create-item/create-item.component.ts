import {Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ItemsService } from '../items.service';


@Component({
  selector: 'app-create-item',
  templateUrl: './create-item.component.html',
  styleUrls: ['./create-item.component.css']
})
export class CreateItemComponent implements OnInit, OnDestroy {
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
    public dialogRef: MatDialogRef<CreateItemComponent>, 
    public itemsService: ItemsService
    ) {
          this.userId = data.rowId;
          this.itemsService.GetItemsFormData().then((res: any) => {
            debugger;
            this.roles = res;
          }).catch((err) => {
            console.error('Failed to load form data', err);
          });
      }
  ngOnInit(): void {
    if (this.userId) {
      this.loadUserForEdit(this.userId);  
    }
  }
  onSubmitItem(form: NgForm) {
    if (form.invalid) {
      return;
    }
    this.itemsService.CreateItem(this.model).then((success: any) => {
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
  this.itemsService.GetItemDetails(userId).then((res: any) => {
    debugger;
    this.model = res;
  }).catch((err) => {
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




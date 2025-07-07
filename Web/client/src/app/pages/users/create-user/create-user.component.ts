import {Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UsersService } from '../users.service';
@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit, OnDestroy {
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
    public dialogRef: MatDialogRef<CreateUserComponent>, 
    public usersService: UsersService
    ) {
          this.userId = data.rowId;
          this.usersService.GetUsersFormData().then((res: any) => {
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
  // onCreateUser(form: NgForm) {
  //   if (form.invalid) {
  //     return;
  //   }
  //   this.usersService.CreateUser(this.model).then((success: any) => {
  //     if (success) {
  //        alert("A new user has been added.");
  //     } else {
  //       alert("Create user error");
  //     }
  //   });
  //   console.log('User created:', this.model);
  //   this.dialogRef.close(this.model);
  // }
onSubmitUser(form: NgForm) {
  if (form.invalid) {
    return;
  }
  const isUpdate = !!this.userId;
  if (isUpdate) {
    this.usersService.UpdateUser(this.model).then((success: any) => {
      if (success) {
        alert("User updated successfully");
    setTimeout(() => {
      debugger;
      location.reload();
    }, 100);
      } else {
        alert("Failed to update user");
      }
    });
  } else {
    this.usersService.CreateUser(this.model).then((success: any) => {
      if (success) {
        alert("User created successfully");
      } else {
        alert("Failed to create user");
      }
    });
  }
  this.dialogRef.close(true);
}
  loadUserForEdit(userId: number) {
  this.usersService.GetUserDetails(userId).then((res: any) => {
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




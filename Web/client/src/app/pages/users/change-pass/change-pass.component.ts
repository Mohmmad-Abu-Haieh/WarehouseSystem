import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UsersService } from '../users.service';
@Component({
  selector: 'app-change-pass',
  templateUrl: './change-pass.component.html',
  styleUrls: ['./change-pass.component.css']
})
export class ChangepassComponent implements OnInit, OnDestroy {
  userId: any;
  public model: any = {
    id: null,
    newPassword: '',
    confirmPassword: ''
  };
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<ChangepassComponent>,
    public usersService: UsersService
  ) {
    this.userId = data.rowId;
  }

  ngOnInit(): void {
  }
  onChangePassword(form: NgForm) {
    if (form.invalid || this.model.newPassword !== this.model.confirmPassword) {
      alert("Not match");
      return;
    }

    const dto = {
      id: this.userId,
      newPassword: this.model.newPassword
    };

    this.usersService.ChangePassword(dto).then(() => {
      alert('Password changed successfully.');
      this.dialogRef.close(true);

      form.resetForm();
    }).catch(err => {
      this.dialogRef.close(true);

      console.error('Error changing password:', err);
      alert('Failed to change password.');
    });


  }

  ngOnDestroy(): void {
    this.model = {
      id: null,
      newPassword: '',
      confirmPassword: ''
    };
  }
}




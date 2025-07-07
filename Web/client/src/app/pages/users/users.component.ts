import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../_guards/auth.service';
import { UsersService } from './users.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CreateUserComponent } from './create-user/create-user.component';
import { ChangepassComponent } from './change-pass/change-pass.component';
declare var $: any;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: [
    "./users.component.scss"
  ]
})
export class UsersComponent implements OnInit, OnDestroy {
dataOfTable = {
    Data: [] as any[],
    PageSize: 2,
    PageIndex: 0,
    keyword: '',
    DataCount: 0
};
  pagesArray: number[] = [];
  constructor(public authService: AuthService,
    public usersService: UsersService,public dialog: MatDialog) {
}
  ngOnInit(): void {
  this.loadUsers();  
}
  onSearch() {
  this.dataOfTable.PageIndex = 0;
  this.loadUsers();
}
loadUsers() {
const filter = {
      keyword: this.dataOfTable.keyword,
      pageIndex: this.dataOfTable.PageIndex,
      pageSize: this.dataOfTable.PageSize
    };
  this.usersService.GetAllUsers(filter).then((response: any) => {
    debugger;
    this.dataOfTable.Data = response.data;
    this.dataOfTable.DataCount = response.count;
    const pageCount = Math.ceil(this.dataOfTable.DataCount / this.dataOfTable.PageSize);
        this.pagesArray = Array(pageCount).fill(0);
  }).catch(error => {
    console.error('Error:', error);
  });
}
changePage(newPageIndex: number) {
    if (newPageIndex < 0 || newPageIndex >= this.pagesArray.length) return;
    this.dataOfTable.PageIndex = newPageIndex;
    this.loadUsers();
}
onOpenCreateUserModal() {
    let dialogRef: MatDialogRef<any> = this.dialog.open(CreateUserComponent, {
        disableClose: false,
        width: '80vw',
        height: '80vh',
        data: {}
    });
    dialogRef.afterClosed().subscribe((result) => {
    window.location.reload();
  });
}
onEditUser(user: any) {
  let dialogRef: MatDialogRef<any> = this.dialog.open(CreateUserComponent, {
      disableClose: false,
      width: '80vw',
      height: '80vh',
      data: { rowId: user.id }
  });
dialogRef.afterClosed().subscribe((result) => {
  if (result === true) {
    console.log("User created or updated, reloading users...");
    window.location.reload();
  } else {
    console.log("Dialog closed without saving.");
  }
});
}
ChangePassword(user: any) {
  let dialogRef: MatDialogRef<any> = this.dialog.open(ChangepassComponent, {
      disableClose: false,
      width: '80vw',
      height: '80vh',
      data: { rowId: user.id }
  });
dialogRef.afterClosed().subscribe((result) => {
  if (result === true) {
    console.log("User created or updated, reloading users...");
    window.location.reload();
  } else {
    console.log("Dialog closed without saving.");
  }
});
}
onDeleteUser(user: any) {
  if (confirm(`Are you sure you want to delete user ${user.fullName}?`)) {
    this.usersService.RemoveUser(user.id).then((res: any) => {
      debugger;
      if(res){
        this.loadUsers();
      }
      console.log('User deleted successfully:', res);
    }).catch((err) => {
      console.error('Failed to delete user', err);
    });
    console.log('Delete user:', user);
  }
}

ngOnDestroy(): void {
}
}


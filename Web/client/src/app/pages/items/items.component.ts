import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../_guards/auth.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ItemsService } from './items.service';
import { CreateItemComponent } from './create-item/create-item.component';
declare var $: any;

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: [
    "./items.component.scss"
  ]
})
export class ItemsComponent implements OnInit, OnDestroy {
dataOfTable = {
    Data: [] as any[],
    PageSize: 2,
    PageIndex: 0,
    keyword: '',
    DataCount: 0
  };
  pagesArray: number[] = [];
  constructor(public authService: AuthService,
    public itemsService: ItemsService,public dialog: MatDialog) {
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
  this.itemsService.GetAllItems(filter).then((response: any) => {
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
    let dialogRef: MatDialogRef<any> = this.dialog.open(CreateItemComponent, {
        disableClose: false,
        width: '80vw',
        height: '80vh',
        data: {}
    });
}
onEditUser(user: any) {
  let dialogRef: MatDialogRef<any> = this.dialog.open(CreateItemComponent, {
      disableClose: false,
      width: '80vw',
      height: '80vh',
      data: { rowId: user.id }
  });
}
onDeleteUser(user: any) {
  if (confirm(`Are you sure you want to delete user ${user.fullName}?`)) {
    this.itemsService.RemoveItem(user.id).then((res: any) => {
      debugger;
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


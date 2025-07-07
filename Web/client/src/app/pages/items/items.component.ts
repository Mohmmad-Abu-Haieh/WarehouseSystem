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
    "./items.component.scss"]
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
  this.loadItems();  
  }
  onSearch() {
  this.dataOfTable.PageIndex = 0;
  this.loadItems();
}
loadItems() {
const filter = {
      keyword: this.dataOfTable.keyword,
      pageIndex: this.dataOfTable.PageIndex,
      pageSize: this.dataOfTable.PageSize
    };
  this.itemsService.GetAllItems(filter).then((response: any) => {
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
    this.loadItems();
  }
onOpenCreateItemModal() {
    let dialogRef: MatDialogRef<any> = this.dialog.open(CreateItemComponent, {
        disableClose: false,
        width: '80vw',
        height: '80vh',
        data: {}
    });
  dialogRef.afterClosed().subscribe((result) => {
    window.location.reload();
  });
}
onEditItem(item: any) {
  let dialogRef: MatDialogRef<any> = this.dialog.open(CreateItemComponent, {
      disableClose: false,
      width: '80vw',
      height: '80vh',
      data: { rowId: item.id }
  });
    dialogRef.afterClosed().subscribe((result) => {
    window.location.reload();
  });
}
onDeleteItem(item: any) {
  if (confirm(`Are you sure you want to delete item ${item.fullName}?`)) {
    this.itemsService.RemoveItem(item.id).then((res: any) => {
      alert('item deleted successfully');
          window.location.reload();
    }).catch((err) => {
      alert('Failed to delete item');
    });
    console.log('Delete item:', item);
  }
}
ngOnDestroy(): void {
}
}


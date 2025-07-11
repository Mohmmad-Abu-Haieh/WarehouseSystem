import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../_guards/auth.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { WarehouseService } from './warehouse.service';
import { CreateWarehouseComponent } from './create-warehouse/create-warehouse.component';
import { Router } from '@angular/router';
declare var $: any;

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: [
    "./warehouse.component.scss"
  ]
})
export class WarehouseComponent implements OnInit, OnDestroy {
  dataOfTable = {
    Data: [] as any[],
    PageSize: 10,
    PageIndex: 0,
    keyword: '',
    DataCount: 0
  };
  pagesArray: number[] = [];
  constructor(public authService: AuthService, private router: Router,
    public warehouseService: WarehouseService, public dialog: MatDialog) {
  }
  ngOnInit(): void {
    this.loadWarehouses();
  }
  onSearch() {
    this.dataOfTable.PageIndex = 0;
    this.loadWarehouses();
  }
  loadWarehouses() {
    const filter = {
      keyword: this.dataOfTable.keyword,
      pageIndex: this.dataOfTable.PageIndex,
      pageSize: this.dataOfTable.PageSize
    };
    this.warehouseService.GetAllWarehouses(filter).then((response: any) => {
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
    this.loadWarehouses();
  }
  onOpenCreateWarehouseModal() {
    let dialogRef: MatDialogRef<any> = this.dialog.open(CreateWarehouseComponent, {
      disableClose: false,
      width: '80vw',
      height: '80vh',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result) => {
      window.location.reload();
    });
  }
  onEditWarehouse(warehouse: any) {
    let dialogRef: MatDialogRef<any> = this.dialog.open(CreateWarehouseComponent, {
      disableClose: false,
      width: '80vw',
      height: '80vh',
      data: { rowId: warehouse.id }
    });
    dialogRef.afterClosed().subscribe((result) => {
      window.location.reload();
    });
  }
  onDeleteWarehouse(warehouse: any) {
    if (confirm(`Are you sure you want to delete warehouse ${warehouse.name}?`)) {
      this.warehouseService.RemoveWarehouse(warehouse.id).then((res: any) => {
        if (res) {
          this.loadWarehouses();
        }
        alert('Warehouse deleted successfully:');
      }).catch((err) => {
        alert('Warehouse deleted error:');
      });
    }
  }
  onDeleteWarehouseItems(warehouse: any) {
    console.log('Delete warehouse items:', warehouse);
  }
  showItems(id: any) {
    const link = 'pages/warehouse/items-warehouse/';
    this.router.navigate(['pages/warehouse/items', id]);
  }
  ngOnDestroy(): void {
  }
}


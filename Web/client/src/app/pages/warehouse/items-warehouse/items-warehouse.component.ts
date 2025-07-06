import {Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { WarehouseService } from '../warehouse.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-items-warehouse',
  templateUrl: './items-warehouse.component.html',
  styleUrls: ['./items-warehouse.component.css']
})
export class ItemsWarehouseComponent implements OnInit, OnDestroy {
dataOfTable = {
    Data: [] as any[],
    PageSize: 2,
    PageIndex: 0,
    keyword: '',
    DataCount: 0
  };
  pagesArray: number[] = [];
  warehouseId =null;
  constructor(
    public warehouseService: WarehouseService,
    private route: ActivatedRoute,
    ) 
    {
      this.route.params.subscribe(params => (this.warehouseId = params['id']));
    }
  ngOnInit(): void {
  this.loadWarehouseItems();  

  }
  onSearch() {
  this.dataOfTable.PageIndex = 0;
  this.loadWarehouseItems();
}
loadWarehouseItems() {
const filter = {
      keyword: this.dataOfTable.keyword,
      pageIndex: this.dataOfTable.PageIndex,
      pageSize: this.dataOfTable.PageSize,
      Id : this.warehouseId
    };
  this.warehouseService.GetWarehouseItems(filter).then((response: any) => {
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
    this.loadWarehouseItems();
  }
ngOnDestroy(): void {

  }
}




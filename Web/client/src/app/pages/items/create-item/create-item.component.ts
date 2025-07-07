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
  warehouses : any[] = [];
  itemId: any;
  public model: any = {
      id: null,
      itemName: '',        
      skuCode: '',         
      qty: 0,              
      costPrice: 0,        
      msrpPrice: 0,        
      warehouseId: null    
  };
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<CreateItemComponent>, 
    public itemsService: ItemsService
    ) {
          this.itemId = data.rowId;
          this.itemsService.GetItemsFormData().then((res: any) => {
            this.warehouses = res;
          }).catch((err) => {
            console.error('Failed to load form data', err);
          });
      }
  ngOnInit(): void {
    if (this.itemId) {
      this.loadItemForEdit(this.itemId);  
    }
  }
  // onSubmitItem(form: NgForm) {
  //   if (form.invalid) {
  //     return;
  //   }
  //   this.itemsService.CreateItem(this.model).then((success: any) => {
  //     if (success) {
  //       alert("Item create successfully");
  //     } else {
  //       alert("Failed to create item");
  //     }
  //   });
  //   this.dialogRef.close(true);
  // }

onSubmitItem(form: NgForm) {
  if (form.invalid) {
    return;
  }
  const isUpdate = !!this.itemId;
  if (isUpdate) {
    this.itemsService.UpdateItem(this.model).then((success: any) => {
      if (success) {
        alert("Item updated successfully");
      } else {
        alert("Failed to update item");
      }
    });
  } else {
    this.itemsService.CreateItem(this.model).then((success: any) => {
      if (success) {
        alert("Item created successfully");
      } else {
        alert("Failed to create item");
      }
    });
  }
  this.dialogRef.close(true);
}



  loadItemForEdit(itemId: number) {
  this.itemsService.GetItemDetails(itemId).then((res: any) => {
    this.model = res;
  }).catch((err) => {
    console.error('Failed to load user', err);
  });
}
ngOnDestroy(): void {
  this.model = {
      id: null,
      fullName: '',        
      skuCode: '',         
      qty: 0,              
      costPrice: 0,        
      msrpPrice: 0,        
      warehouseId: null 
  };
}
}




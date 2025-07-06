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
  countries : any[] = [];
  warehouseId: any;
  public model: any = {
        id: null,
        name: '',
        address: '',
        city: '',
        countryId: null
    };
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<CreateWarehouseComponent>, 
    public warehouseService: WarehouseService
    ) {
          this.warehouseId = data.rowId;
          this.warehouseService.GetWarehousesFormData().then((res: any) => {
            debugger;
            this.countries = res;
          }).catch((err : any) => {
            console.error('Failed to load form data', err);
          });
      }
  ngOnInit(): void {
   if (this.warehouseId) {
      this.loadWarehouseForEdit(this.warehouseId);  
    }
  }

  onSubmitWarehouse(form: NgForm) {
  if (form.invalid) {
    return;
  }
  const isUpdate = !!this.warehouseId;
  if (isUpdate) {
    this.warehouseService.UpdateWarehouse(this.model).then((success: any) => {
      if (success) {
        alert("Warehouse updated successfully");
      } else {
        alert("Failed to update warehouse");
      }
    });
  } else {
    this.warehouseService.CreateWarehouse(this.model).then((success: any) => {
      if (success) {
        alert("Warehouse created successfully");
      } else {
        alert("Failed to create warehouse");
      }
    });
  }

  this.dialogRef.close(this.model); // إغلاق النافذة مع إرجاع النموذج
}

  // onCreateWarehouse(form: NgForm) {
  //   if (form.invalid) {
  //     return;
  //   }
  //   this.warehouseService.CreateWarehouse(this.model).then((success: any) => {
  //     debugger;
  //     if (success) {
  //       alert("Successfully created warehouse");
  //     } else {
  //       alert("Failed to create warehouse");
  //     }
  //   });
  //   this.dialogRef.close(this.model);
  // }
  loadWarehouseForEdit(warehouseId: number) {
  this.warehouseService.GetWarehouseDetails(warehouseId).then((res: any) => {
    debugger;
    this.model = res;
  }).catch((err : any) => {
    console.error('Failed to load warehouse', err);
  });
}
ngOnDestroy(): void {
  this.model = {
      id: null,
      name: '',
      address: '',
      city: '',
      countryId: null
  };
}
}




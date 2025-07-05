import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

export interface WarehouseStatusDto {
  warehouseName: string;
  inventoryCount: number;
}

export interface ItemQuantityDto {
  itemName: string;
  quantity: number;
}

@Injectable({
    providedIn: 'root'
})
export class DashboardService {
    constructor(public http: HttpClient,
        public datePipe: DatePipe,
    ) { }

    
    warehouseStatus: WarehouseStatusDto[] = [];
    topHighItems: ItemQuantityDto[] = [];
    topLowItems: ItemQuantityDto[] = [];

    GetWarehouseStatus() {
        debugger;
        return new Promise((resolve, reject) => {
            this.http.get('Dashboard/GetWarehouseStatus')
                .subscribe({
                    next: (response: any) => {
                        this.warehouseStatus = response;
                        if (this.warehouseStatus) {
                            resolve(this.warehouseStatus);
                        } else {
                            resolve(false);
                        }
                    },
                    error: () => {
                        resolve(false);
                    }
                });
        });
    }

    GetTopHighItems() {
        debugger;
        return new Promise((resolve, reject) => {
            this.http.get('Dashboard/GetTopHighItems')
                .subscribe({
                    next: (response: any) => {
                        const topHighItems = response;
                        if (topHighItems) {
                            resolve(topHighItems);
                        } else {
                            resolve(false);
                        }
                    },
                    error: () => {
                        resolve(false);
                    }
                });
        });
    }


    GetTopLowItems() {
        debugger;
        return new Promise((resolve, reject) => {
            this.http.get('Dashboard/GetTopLowItems')
                .subscribe({
                    next: (response: any) => {
                        const lowItems = response;
                        if (lowItems) {
                            this.topLowItems = lowItems;
                            resolve(this.topLowItems);
                        } else {
                            resolve(false);
                        }
                    },
                    error: () => {
                        resolve(false);
                    }
                });
        });
    }
}

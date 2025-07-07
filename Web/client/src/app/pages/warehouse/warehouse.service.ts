import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
    providedIn: 'root'
})
export class WarehouseService {
constructor(public http: HttpClient) {}
GetAllWarehouses(model: any) {
  return new Promise((resolve, reject) => {
    this.http.post('Warehouse/GetWarehouseDataTable', model)
      .subscribe({
        next: (response: any) => {
          const user = response;
          if (user) {
            resolve(response);
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
GetWarehouseItems(model: any) {
  return new Promise((resolve, reject) => {
    this.http.post('Warehouse/GetWarehouseItemsDataTable', model)
      .subscribe({
        next: (response: any) => {
          const items = response;
          if (items) {
            resolve(items);
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
CreateWarehouse(model: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.post('Warehouse/CreateWarehous', model)
      .subscribe({
        next: (response: any) => {
          if (response.isSuccessfull) {
            resolve(true);
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
UpdateWarehouse(model: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.put('Warehouse/UpdateWarehouse', model)
      .subscribe({
        next: (response: any) => {
          if (response.isSuccessfull) {
            resolve(true);
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
GetWarehouseDetails(id: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.get('Warehouse/GetWarehouseDetails/' + id)
      .subscribe({
        next: (response: any) => {
          const user = response.result;
          if (user) {
            resolve(user);
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
GetWarehousesFormData() {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.get('Warehouse/GetWarehouseFormData')
      .subscribe({
        next: (response: any) => {
          const data = response.countries;
          if (data) {
            resolve(data);
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
RemoveWarehouse(id: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.delete('Warehouse/DeleteWarehouse/' + id)
      .subscribe({
        next: (response: any) => {
          if (response.isSuccessfull) {
            resolve(true);
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


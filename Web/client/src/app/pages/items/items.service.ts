import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
    providedIn: 'root'
})
export class ItemsService {
constructor(public http: HttpClient) {}
GetAllItems(model: any) {
  return new Promise((resolve, reject) => {
    this.http.post('Items/GetItemsDataTable', model)
      .subscribe({
        next: (response: any) => {
          if (response) {
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
CreateItem(model: any) {
  return new Promise((resolve, reject) => {
    this.http.post('Items/CreateItems', model)
      .subscribe({
        next: (response: any) => {
          const res = response;
          if (res.isSuccessfull) {
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
GetItemDetails(id: any) {
  return new Promise((resolve, reject) => {
    this.http.get('Items/GetItemDetails/' + id)
      .subscribe({
        next: (response: any) => {
          const item = response.result;
          if (item) {
            resolve(item);
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
GetItemsFormData() {
  return new Promise((resolve, reject) => {
    this.http.get('Items/GetItemsFormData')
      .subscribe({
        next: (response: any) => {
          const data = response.warehouses;
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
// RemoveItem(id: any) {
//   return new Promise((resolve, reject) => {
//     this.http.delete('Items/DeleteUser/' + id)
//       .subscribe({
//         next: (response: any) => {
//           const user = response.result;
//           if (user) {
//             resolve(user);
//           } else {
//             resolve(false);
//           }
//         },
//         error: () => {
//           resolve(false);       
//         }
//       });
//   });
// }
UpdateItem(model: any) {
  return new Promise((resolve, reject) => {
    this.http.put('Items/UpdateItem', model)
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
RemoveItem(id: any) {
  return new Promise((resolve, reject) => {
    this.http.delete('Items/DeleteItem/' + id)
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


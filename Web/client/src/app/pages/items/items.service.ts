import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
    providedIn: 'root'
})
export class ItemsService {
constructor(public http: HttpClient) {
}
GetAllItems(model: any) {
  return new Promise((resolve, reject) => {
    this.http.post('User/GetUsersDataTable', model)
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
CreateItem(model: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.post('User/CreateAccount', model)
      .subscribe({
        next: (response: any) => {
          const user = response;
          if (user) {
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
  debugger;
  return new Promise((resolve, reject) => {
    this.http.get('User/GetUserDetails/' + id)
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
GetItemsFormData() {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.get('User/GetUsersFormData')
      .subscribe({
        next: (response: any) => {
          const data = response.roles;
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
RemoveItem(id: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.delete('User/DeleteUser/' + id)
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
}


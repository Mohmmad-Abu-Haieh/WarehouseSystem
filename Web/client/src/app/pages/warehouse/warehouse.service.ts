import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
    providedIn: 'root'
})
export class WarehouseService {
constructor(public http: HttpClient) {
}
GetAllUsers(model: any) {
  return new Promise((resolve, reject) => {
    this.http.post('http://localhost:5017/api/User/GetUsersDataTable', model)
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
CreateUser(model: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.post('http://localhost:5017/api/User/CreateAccount', model)
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
GetUserDetails(id: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.get('http://localhost:5017/api/User/GetUserDetails/' + id)
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
GetUsersFormData() {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.get('http://localhost:5017/api/User/GetUsersFormData')
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
RemoveUser(id: any) {
  debugger;
  return new Promise((resolve, reject) => {
    this.http.delete('http://localhost:5017/api/User/DeleteUser/' + id)
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


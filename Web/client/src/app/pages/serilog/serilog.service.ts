import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
    providedIn: 'root'
})
export class LogsService {
constructor(public http: HttpClient) {
}
GetAllLogs(model: any) {
  return new Promise((resolve, reject) => {
    this.http.post('Logs/GetLogsDataTable', model)
      .subscribe({
        next: (response: any) => {
          const logs = response;
          if (logs) {
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
}


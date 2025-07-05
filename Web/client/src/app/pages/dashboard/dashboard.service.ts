import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Injectable({
    providedIn: 'root'
})
export class DashboardService {
    constructor(public http: HttpClient,
        public datePipe: DatePipe,
        ) {
    }
}

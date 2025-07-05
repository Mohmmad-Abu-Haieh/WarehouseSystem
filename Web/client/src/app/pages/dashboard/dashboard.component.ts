import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../_guards/auth.service';
import { DashboardService } from './dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';

declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {

  constructor(
    public authService: AuthService,
    public dashboardService: DashboardService,
    public route: ActivatedRoute,
    public datePipe: DatePipe,
    private router: Router
  ) {}

  ngOnInit(): void {
    // ضع كود التهيئة هنا
    console.log("Dashboard initialized");
  }

  ngOnDestroy(): void {
    // ضع كود التنظيف هنا (مثل unsubscribes)
    console.log("Dashboard destroyed");
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../_guards/auth.service';
import { DashboardService } from './dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { ChartOptions, ChartType, ChartData } from 'chart.js';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: []
  };
  constructor(
    public authService: AuthService,
    public dashboardService: DashboardService,
    public route: ActivatedRoute,
    public datePipe: DatePipe,
    private router: Router
  ) {
    this.dashboardService.GetWarehouseStatus().then((status: any) => {
      if (status) {
        this.dashboardService.warehouseStatus = status;
        this.barChartData = {
          labels: this.dashboardService.warehouseStatus.map(item => item.warehouseName),
          datasets: [
            {
              data: this.dashboardService.warehouseStatus.map(item => item.inventoryCount),
              label: 'Inventory Count',
              backgroundColor: ['#42A5F5', '#66BB6A']
            }
          ]
        };
      }
    });
    this.dashboardService.GetTopHighItems().then((items: any) => {
      if (items) {
        this.dashboardService.topHighItems = items;
      }
    });

    this.dashboardService.GetTopLowItems().then((items: any) => {
      if (items) {
        this.dashboardService.topLowItems = items;
      }
    });
  }
  ngOnInit(): void {
    console.log("Dashboard initialized");
  }

  ngOnDestroy(): void {
    console.log("Dashboard destroyed");
  }

  barChartOptions: ChartOptions = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'top'
      }
    }
  };

  barChartType: ChartType = 'bar';
  topHighItems = this.dashboardService.topHighItems;
  topLowItems = this.dashboardService.topLowItems;

}
export interface WarehouseStatusDto {
  warehouseName: string;
  inventoryCount: number;
}

export interface ItemQuantityDto {
  itemName: string;
  quantity: number;
}
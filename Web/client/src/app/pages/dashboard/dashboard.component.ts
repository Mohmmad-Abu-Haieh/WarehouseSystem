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
        debugger;
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

  // إعدادات الشارت
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



  // أعلى 10 عناصر من حيث الكمية
  // topHighItems = [
  //   { itemName: 'Paper Rolls', qty: 500 },
  //   { itemName: 'USB-C Cables', qty: 420 },
  //   { itemName: 'Notebooks', qty: 380 },
  //   { itemName: 'Pens', qty: 340 },
  //   { itemName: 'Markers', qty: 320 },
  //   { itemName: 'Toners', qty: 300 },
  //   { itemName: 'Cables', qty: 280 },
  //   { itemName: 'Mouse', qty: 250 },
  //   { itemName: 'Keyboards', qty: 240 },
  //   { itemName: 'Folders', qty: 230 },
  // ];
  topHighItems = this.dashboardService.topHighItems;
  topLowItems = this.dashboardService.topLowItems;
  // أدنى 10 عناصر من حيث الكمية
  // topLowItems = [
  //   { itemName: 'Mouse Pads', qty: 2 },
  //   { itemName: 'HDMI Adapters', qty: 5 },
  //   { itemName: 'Ethernet Cables', qty: 7 },
  //   { itemName: 'USB Drives', qty: 8 },
  //   { itemName: 'Laminators', qty: 10 },
  //   { itemName: 'Binders', qty: 12 },
  //   { itemName: 'Scissors', qty: 14 },
  //   { itemName: 'Stamps', qty: 15 },
  //   { itemName: 'Folders A4', qty: 16 },
  //   { itemName: 'Staplers', qty: 17 },
  // ];
}
export interface WarehouseStatusDto {
  warehouseName: string;
  inventoryCount: number;
}

export interface ItemQuantityDto {
  itemName: string;
  quantity: number;
}
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../_guards/auth.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LogsService } from './serilog.service';
import { PageEvent } from '@angular/material/paginator';
declare var $: any;

@Component({
  selector: 'app-serilog',
  templateUrl: './serilog.component.html',
  styleUrls: [
    "./serilog.component.scss"
  ]
})
export class LogsComponent implements OnInit, OnDestroy {
dataOfTable = {
    Data: [] as any[],
    PageSize: 10,
    PageIndex: 0,
    keyword: '',
    DataCount: 0
  };
  columnsToDisplay: string[] = ['Id', 'Timestamp', 'Level', 'Exception', 'RenderedMessage'];
  totalPages = 0;
  pagesArray: number[] = [];
  constructor(
    public authService: AuthService,
    public logsService: LogsService,
    public dialog: MatDialog) {
  }
  ngOnInit(): void {
  this.loadLogs();  
  }
  onSearch() {
  this.dataOfTable.PageIndex = 0;
  this.loadLogs();
}
loadLogs() {
  const filter = {
    keyword: this.dataOfTable.keyword,
    pageIndex: this.dataOfTable.PageIndex,
    pageSize: this.dataOfTable.PageSize
  };
  this.logsService.GetAllLogs(filter).then((response: any) => {
    this.dataOfTable.Data = response.data;
    this.dataOfTable.DataCount = response.count;
  }).catch(error => {
    console.error('Error:', error);
  });
}
onPageChange(event: PageEvent) {
    this.dataOfTable.PageIndex = event.pageIndex;
    this.dataOfTable.PageSize = event.pageSize;
    this.loadLogs();
}
ngOnDestroy(): void {
}
}


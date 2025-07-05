import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [
    "./home.component.scss"
  ]
})
export class HomeComponent implements OnInit, OnDestroy {
  timer = null;
  displayedColumns: string[] = ['name', 'username' , 'role', 'email', 'mobile', "actions"];
  
  constructor( private router: Router,
    ) {
  }
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  
}

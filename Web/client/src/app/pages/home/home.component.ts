import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../_guards/auth.service';
declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [
    "./home.component.scss"
  ]
})
export class HomeComponent implements OnInit, OnDestroy {
  currentUserRole: string
  constructor(private router: Router,
    public authService: AuthService
  ) {
    this.currentUserRole = authService.GetUserRole();
  }
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }


}

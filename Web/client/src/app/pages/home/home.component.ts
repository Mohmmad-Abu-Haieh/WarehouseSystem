import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../_guards/auth.service';
import { Role } from '../../shared/enum/enums';
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
  currentUserRole: string
  systemRoles = Role;
  
  constructor( private router: Router,
    public authService: AuthService
    ) {
      debugger;
      this.currentUserRole = authService.GetUserRole();
      debugger;
      
  }
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  
}

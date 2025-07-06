import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../_guards/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(private router: Router, private authService: AuthService,) {}
  public model: any = {
    password: '',
    email: ''
  };
  isValidFormSubmitted = false;
  onLogin(form: NgForm) {
    this.isValidFormSubmitted = true;
    if (form.invalid) {
      this.isValidFormSubmitted = false;
      return;
    }
    this.authService.login(this.model).then((success => {
      if (success as boolean) {
        alert("You have successfully logged in.");
        this.router.navigateByUrl('/pages/home');
      } else {
        alert("Login error");
        localStorage.clear();
      }
    }));
  }
}

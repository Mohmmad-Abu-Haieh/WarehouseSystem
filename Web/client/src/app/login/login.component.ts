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
  SendEmail = false;
  constructor(private router: Router, private authService: AuthService,
  ) {
  }
  public model: any = {
    password: '',
    email: ''
  };
  showForgotForm = false;
  isValidFormSubmitted = false;
  isValidForgotFormSubmitted = false;
  forgotEmail = '';


  onLogin(form: NgForm) {
    this.isValidFormSubmitted = true;
    if (form.invalid) {
      this.isValidFormSubmitted = false;
      return;
    }

    this.authService.login(this.model).then((success => {
      if (success as boolean) {
        this.router.navigateByUrl('/pages/home'); // Redirect to Home after login
      } else {
        alert("فشل تسجيل الدخول");
        localStorage.clear();
      }
    }));
  }

  onLogin2(form: NgForm){
    console.log('Login form submitted:', this.model);
    if (this.model.email && this.model.password) {
      localStorage.setItem('token', 'dummy-token');
      this.router.navigateByUrl('/pages/home'); // Redirect to Home after login, if both fields are filled
    }
  }
}

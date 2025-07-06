import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { Role } from '../shared/enum/enums';


@Injectable()
export class AuthService {
  private currentUserStr = localStorage.getItem('currentUserToken');
  userToken: any = localStorage.getItem('token');
  decodedToken: any;
  isLoading: any = false;
  isMobilePlatforem: boolean = false;


  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
  ) {
  }

  login(model: any) {
    return new Promise((resolve, reject) => {
      this.http.post('Auth/login', model)
        .subscribe({
          next: (response: any) => {
            const user = response;
            if (user && user.accessToken) {
              localStorage.setItem('token', user.accessToken);
              resolve(true);
            } else {
              resolve(false);
            }
          },
          error: () => {
            resolve(false);
          }
        });
    });
  }

  GetUserRole(): string {
    const userToken:any = localStorage.getItem('token');
    this.decodedToken = jwtDecode<JwtPayload>(userToken);

    return this.decodedToken.role
  }

}

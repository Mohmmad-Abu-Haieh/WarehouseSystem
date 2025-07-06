import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode, JwtPayload } from 'jwt-decode';


@Injectable()
export class AuthService {
  userToken: any = localStorage.getItem('token');
  decodedToken: any;
  constructor(private http: HttpClient) {
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

// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { PagesModule } from './pages/pages.module'; // أضف هذا

import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthService } from './_guards/auth.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HttpClientInterceptor, HTTPStatus } from './httpClient.interceptor';

const RxJS_Services = [HttpClientInterceptor, HTTPStatus];

@NgModule({
  declarations: [ 
    
    AppComponent,
    LoginComponent,
    NotFoundComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    PagesModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    AuthService, 
    ...RxJS_Services,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpClientInterceptor,
      multi: true
    },
    provideAnimationsAsync()],
  bootstrap: [AppComponent]
})
export class AppModule {}

import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  token?:string | null;

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    // this.accountService.currentUser$.pipe(take(1)).subscribe(
    //   {next: u => this.token =  u?.token}
    //   )
      this.token = localStorage.getItem('token');
      if(this.token){
        request = request.clone({
          setHeaders:{
            Authorization: 'Bearer '+ this.token
          }
        })
      }
    return next.handle(request);
  }
}

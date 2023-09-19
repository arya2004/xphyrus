import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IUser } from '../shared/models/IUser';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IResponse } from '../shared/models/IResponse';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.authApiUrl;
  private currentUserSource = new ReplaySubject<IUser | null>(1);
  currentUser$ = this.currentUserSource.asObservable();
    
  constructor(private http: HttpClient, private router: Router) { }

  login(values: any){
    return this.http.post<IResponse<IUser>>(this.baseUrl + 'login', values).pipe(
      map(user => {
        localStorage.removeItem('token');
        localStorage.setItem('token', user.result.token);
        this.currentUserSource.next(user.result);
        
      })
    )
  }

  register(values: any){
    return this.http.post<IResponse<any>>(this.baseUrl + 'register', values).pipe(
      map(user => {
        this.router.navigateByUrl('/account/login');
      })
    )
  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExist(email:string){
    return this.http.post<IResponse<boolean>>(this.baseUrl+ 'emailExists', email)
  }

  loadCurrentUser(token: string | null){
    if(token === null){
      this.currentUserSource.next(null);
      return of(null);
    }


    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);
    console.log(this.baseUrl);
    console.log(headers);
    
    return this.http.get<IResponse<IUser>>(this.baseUrl, {headers}).pipe(
      map(user => {
        if(user){
          console.log(user);
          
          localStorage.setItem('token', user.result.token);
          this.currentUserSource.next(user.result);
          return user.result;
        }
        else{
          return null;
        }
      })
    )
  }

}

import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
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
  private currentUserSource = new BehaviorSubject<IUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
    
  constructor(private http: HttpClient, private router: Router) { }

  login(values: any){
    return this.http.post<IResponse<IUser>>(this.baseUrl + 'login', values).pipe(
      map(user => {
        localStorage.setItem('token', user.result.token);
        this.currentUserSource.next(user.result);
        
      })
    )
  }

  register(values: any){
    return this.http.post<IResponse<any>>(this.baseUrl + 'register', values).pipe(
      map(user => {
        localStorage.setItem('token', user.result.token);
        this.currentUserSource.next(user.result);
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

  loadCurrentUser(token: string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);
    return this.http.get<IResponse<IUser>>(this.baseUrl + '', {headers}).pipe(
      map(user => {
        localStorage.setItem('token', user.result.token);
        this.currentUserSource.next(user.result);
      })
    )
  }

}

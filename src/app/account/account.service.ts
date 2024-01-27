import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IUser } from '../shared/models/IUser';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IResponse } from '../shared/models/IResponse';
import { IToken } from '../shared/models/IToken';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.authApiUrl;
  private currentUserSource = new ReplaySubject<IUser | null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  Token!: IToken;

  
  constructor(private http: HttpClient, private router: Router)
  {
    const t = localStorage.getItem('token');
    if (t) {
      this. Token = this.getUser(localStorage.getItem('token')!);
    }
   
  }

  login(values: any){
    return this.http.post<IResponse<IUser>>(this.baseUrl + 'login', values).pipe(
      map(user => {
        localStorage.removeItem('token');
        localStorage.setItem('token', user.result.token);
        this.currentUserSource.next(user.result);
        this.Token = this.getUser(user.result.token);
      })
    )
  }

  register(values: any) {
    return this.http.post<IUser>(this.baseUrl + 'register', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
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

  private getUser(token: string) : IToken{
    return JSON.parse(atob(token.split('.')[1])) as IToken;
  }

}

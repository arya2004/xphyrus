import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IUser } from '../shared/models/IUser';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IResponse } from '../shared/models/IResponse';
import { IToken } from '../shared/models/IToken';

/**
 * Service to handle user account operations such as login, registration, and authentication.
 */
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private baseUrl = environment.authApiUrl;
  private currentUserSource = new ReplaySubject<IUser | null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  token!: IToken;

  /**
   * Constructor to inject necessary services.
   * @param http HttpClient to perform HTTP requests.
   * @param router Router to navigate between routes.
   */
  constructor(private http: HttpClient, private router: Router) {
    const storedToken = localStorage.getItem('token');
    if (storedToken) {
      this.token = this.parseToken(storedToken);
    }
  }

  /**
   * Method to handle user login.
   * @param values The login credentials.
   * @returns Observable of the user data.
   */
  login(values: any) {
    return this.http.post<IResponse<IUser>>(this.baseUrl + 'login', values).pipe(
      map(user => {
        localStorage.removeItem('token');
        localStorage.setItem('token', user.result.token);
        this.currentUserSource.next(user.result);
        this.token = this.parseToken(user.result.token);
      })
    );
  }

  /**
   * Method to handle user registration.
   * @param values The registration data.
   * @returns Observable of the user data.
   */
  register(values: any) {
    return this.http.post<IUser>(this.baseUrl + 'register', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  /**
   * Method to handle user logout.
   */
  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  /**
   * Method to check if an email already exists.
   * @param email The email to check.
   * @returns Observable of the response.
   */
  checkEmailExist(email: string) {
    return this.http.post<IResponse<boolean>>(this.baseUrl + 'emailExists', email);
  }

  /**
   * Method to load the current user using a token.
   * @param token The authentication token.
   * @returns Observable of the user data or null.
   */
  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<IUser>(this.baseUrl + "GetCurrentUser", { headers }).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        } else {
          this.currentUserSource.next(null);
          return null;
        }
      })
    );
  }

  /**
   * Method to parse a JWT token and extract user information.
   * @param token The JWT token.
   * @returns The parsed token as an IToken object.
   */
  private parseToken(token: string): IToken {
    return JSON.parse(atob(token.split('.')[1])) as IToken;
  }
}

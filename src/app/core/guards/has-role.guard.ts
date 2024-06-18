import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
  providedIn: 'root'
})
export class HasRoleGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const isAuthorized =  this.accountService.token.role.includes(route.data?.['role']);

    if (!isAuthorized) {
      window.alert('You are not authorized to access this area');
      this.router.navigateByUrl('/');
    }
    return isAuthorized;
  }
  
}

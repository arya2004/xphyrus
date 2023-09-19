import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
  })

  returnUrl: string = "";

  constructor(private accountService: AccountService, private activatedRoot: ActivatedRoute, private router: Router) {
    this.returnUrl = this.activatedRoot.snapshot.queryParams['returnUrl'] || '/'
  }

  onSubmit(){
    console.log(this.loginForm.value);
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => this.router.navigateByUrl(this.returnUrl)
      
    })
    
  }
  
}

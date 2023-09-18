import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
  })


  constructor(private accountService: AccountService) {}

  onSubmit(){
    console.log(this.loginForm.value);
    this.accountService.login(this.loginForm.value).subscribe({
      next: uset => console.log(uset)
      
    })
    
  }
  
}

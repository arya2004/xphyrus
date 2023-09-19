import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

    constructor(private fb: FormBuilder, private router: Router, private accService: AccountService) {}
    selectedOption: string = "student";
    complexPasswd = "(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"

    registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(this.complexPasswd)]],
      role: ['', Validators.required]
    })

    onSubmit(){
      console.log(this.registerForm.value);
      
      this.accService.register(this.registerForm.value).subscribe({
        next: () => this.router.navigateByUrl('/account/login')
      })
    }

}

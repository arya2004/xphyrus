import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ExamineeService } from '../examinee.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  constructor(private fb: FormBuilder, private router: Router, private accService: ExamineeService) {}
    selectedOption: string = "student";
    complexPasswd = "(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"

    registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(this.complexPasswd)]],
      website: [''],
      employeeCount: [''],
      about: [''],
      LinkedIn: [''],
      Twitter: ['']
    })

    onSubmit(){
      console.log(this.registerForm.value);
      
      // this.accService.register(this.registerForm.value).subscribe({
      //   next: () => this.router.navigateByUrl('/account/login')
      // })
    }
}

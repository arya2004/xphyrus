import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Subject, takeUntil } from 'rxjs';

/**
 * RegisterComponent handles the user registration functionality.
 */
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup;
  isStudentRole: boolean = false;
  private unsubscribe$ = new Subject<void>();
  // Uncomment and use if complex password validation is required
  // complexPasswd: string = "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/>,<.])(?!.*\\s).*$";

  /**
   * Constructor for RegisterComponent
   * @param fb FormBuilder to create reactive forms
   * @param router Router to navigate within the application
   * @param accService Service for handling account operations
   */
  constructor(private fb: FormBuilder, private router: Router, private accService: AccountService) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required],
      displayName: ['', Validators.required],
      userRole: ['', Validators.required],
      prn: [''],
      division: [''],
      batch: [''],
      bio: [''],
    }, { validator: this.passwordMatchValidator });

    this.onRoleChange(); // Ensure correct initial state
  }

  /**
   * Updates the form based on the selected role.
   */
  onRoleChange(): void {
    const userRole = this.registerForm.get('userRole')?.value;
    this.isStudentRole = userRole === '1'; // Assuming '1' represents the Student role

    if (this.isStudentRole) {
      this.registerForm.get('prn')?.setValidators([Validators.required]);
      this.registerForm.get('division')?.setValidators([Validators.required]);
      this.registerForm.get('batch')?.setValidators([Validators.required]);
    } else {
      this.registerForm.get('prn')?.clearValidators();
      this.registerForm.get('division')?.clearValidators();
      this.registerForm.get('batch')?.clearValidators();
    }

    this.registerForm.get('prn')?.updateValueAndValidity();
    this.registerForm.get('division')?.updateValueAndValidity();
    this.registerForm.get('batch')?.updateValueAndValidity();
  }

  /**
   * Validates if the password and confirm password fields match.
   * @param formGroup The FormGroup containing the password controls.
   */
  passwordMatchValidator(formGroup: FormGroup): any {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;

    if (password !== confirmPassword) {
      formGroup.get('confirmPassword')?.setErrors({ mismatch: true });
    } else {
      formGroup.get('confirmPassword')?.setErrors(null);
    }
  }

  /**
   * Handles form submission for user registration.
   */
  onSubmit(): void {
    if (this.registerForm.valid) {
      console.log('Registration form submitted with values:', this.registerForm.value);

      this.accService.register(this.registerForm.value)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe({
          next: () => {
            console.log('Registration successful, navigating to login page.');
            this.router.navigateByUrl('/account/login');
          },
          error: (error: HttpErrorResponse) => {
            console.error('Registration failed:', error);
            this.handleError(error);
          }
        });
    } else {
      this.logValidationErrors();
      this.markAllAsTouched();
    }
  }

  /**
   * Logs detailed validation errors.
   */
  private logValidationErrors(): void {
    console.warn('Registration form is invalid:');

    Object.keys(this.registerForm.controls).forEach(key => {
      const control: AbstractControl = this.registerForm.get(key)!;
      if (control.invalid) {
        const errors = control.errors;
        if (errors) {
          console.warn(`Field "${key}" has validation errors:`, errors);
        }
      }
    });
  }

  /**
   * Handles HTTP errors.
   * @param error The HTTP error response
   */
  private handleError(error: HttpErrorResponse): void {
    if (error.error instanceof ErrorEvent) {
      // Client-side or network error
      console.error('Client-side error:', error.error.message);
    } else {
      // Server-side error
      console.error(`Server-side error: ${error.status} - ${error.message}`);
    }
    alert('Registration failed. Please try again.');
  }

  /**
   * Marks all form controls as touched to trigger validation messages.
   */
  private markAllAsTouched(): void {
    Object.values(this.registerForm.controls).forEach(control => {
      control.markAsTouched();
    });
  }

  /**
   * Clean up subscriptions on component destroy to prevent memory leaks.
   */
  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}

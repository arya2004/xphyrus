import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

/**
 * LoginComponent handles the user login functionality.
 */
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  returnUrl: string = '';

  /**
   * Constructor for LoginComponent
   * @param accountService Service for handling account operations
   * @param activatedRoute ActivatedRoute to get query parameters
   * @param router Router to navigate within the application
   */
  constructor(
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    });

    // Get the return URL from query parameters or default to '/'
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
  }

  /**
   * Handles form submission for user login.
   */
  onSubmit(): void {
    if (this.loginForm.valid) {
      console.log('Login form submitted with values:', this.loginForm.value);

      this.accountService.login(this.loginForm.value).subscribe({
        next: () => {
          console.log('Login successful, navigating to:', this.returnUrl);
          this.router.navigateByUrl(this.returnUrl);
        },
        error: (error: HttpErrorResponse) => {
          console.error('Login failed:', error);
          this.handleError(error);
        }
      });
    } else {
      console.warn('Login form is invalid:', this.loginForm);
      this.markAllAsTouched();
    }
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
    alert('Login failed. Please try again.');
  }

  /**
   * Marks all form controls as touched to trigger validation messages.
   */
  private markAllAsTouched(): void {
    Object.values(this.loginForm.controls).forEach(control => {
      control.markAsTouched();
    });
  }
}

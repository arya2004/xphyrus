import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { IUser } from 'src/app/shared/models/IUser';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  applicationUserForm: FormGroup;
  passwordChangeForm: FormGroup;
  companySizes = [
    { value: 1, label: 'Small (1-10)' },
    { value: 2, label: 'Small (11-50)' },
    { value: 3, label: 'Medium (51-200)' },
    { value: 4, label: 'Medium (201-500)' },
    { value: 5, label: 'Large (501-1000)' },
    { value: 6, label: 'Large (1001-5000)' },
    { value: 7, label: 'Enterprise (5000+)' }
  ];

  constructor(private fb: FormBuilder, public userService: AccountService) {}

  ngOnInit(): void {
    this.initializeForm();

    this.userService.currentUser$.subscribe(
      (user: IUser | null) => {
        if (user) {
          console.log('User data:', user);
          this.applicationUserForm.patchValue(user);
        } else {
          this.applicationUserForm.reset();
        }
      },
      error => {
        console.error('Error fetching user data:', error);
        this.applicationUserForm.reset();
      }
    );
  }

  initializeForm(): void {
    this.applicationUserForm = this.fb.group({
      displlayname: ['', Validators.required],
      companyName: [''],
      email: [''],
      workEmail: ['', [Validators.required, Validators.email]],
      location: [''],
      market: [''],
      oneLinePitch: [''],
      companySize: [null, Validators.required],
      websiteUrl: ['', [ Validators.required]],
      twitterUrl: [''],
      facebookUrl: [''],
      linkedinUrl: ['', [ Validators.required]],
      role: ['']
    });

    this.passwordChangeForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      reEnterNewPassword: ['', Validators.required]}, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const newPassword = form.get('newPassword')?.value;
    const reEnterNewPassword = form.get('reEnterNewPassword')?.value;
    return newPassword === reEnterNewPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.applicationUserForm.valid) {
      const user = this.applicationUserForm.value;
      if (user.id) {
        // Update existing user
        console.log('Updating user:', user);
      } else {
        // Create new user
        console.log('Creating user:', user);
      }
    }
  }

  onSubmitPasswordForm() {
    if (this.passwordChangeForm.valid) {
      console.log('Password change request:', this.passwordChangeForm.value);
      // Handle the password change logic here
    }
  }

}

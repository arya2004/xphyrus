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
          console.log('User data:', this.applicationUserForm.value);
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
      prn: ['', Validators.required],
      division: ['', Validators.required],
      batch: ['', Validators.required],
      bio: ['', Validators.required],
      type: ['', Validators.required]
    });

    this.passwordChangeForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      reEnterNewPassword: ['', Validators.required]}, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(formGroup: FormGroup) : any{
    const password = formGroup.get('newPassword').value;
    const confirmPassword = formGroup.get('reEnterNewPassword').value;
  
    if (password !== confirmPassword) {
      formGroup.get('reEnterNewPassword').setErrors({ mismatch: true });
    } else {
      return null;
    }
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

  userRoles = [
    { value: 'Student', label: 'Student' },
    { value: 'Teacher', label: 'Teacher' },
    { value: 'Admin', label: 'Admin' }
  ];

  applicationUser = {
    displayName: 'John Doe',
    prn: '123456',
    division: 'A',
    batch: '2021',
    bio: 'A brief bio about John Doe.',
    type: 'Student'
  };
}

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
  applicationUser: IUser | null = null; // Define the applicationUser property

  constructor(private fb: FormBuilder, public userService: AccountService) {}

  ngOnInit(): void {
    this.initializeForm();

    this.userService.currentUser$.subscribe(
      (user: IUser | null) => {
        if (user) {
          this.applicationUser = user; // Assign the user data to applicationUser
          this.applicationUserForm.patchValue({
            email: user.email,
            displayName: user.displayName,
            name: user.name,
            prn: user.prn,
            division: user.division,
            batch: user.batch,
            bio: user.bio,
            role: user.role,
          });
        } else {
          this.applicationUserForm.reset();
          this.applicationUser = null; // Reset applicationUser if no user data
        }
      },
      error => {
        console.error('Error fetching user data:', error);
        this.applicationUserForm.reset();
        this.applicationUser = null;
      }
    );
  }

  initializeForm(): void {
    this.applicationUserForm = this.fb.group({
      email: [{ value: '', disabled: true }, Validators.required], // Disabled field
      displayName: ['', Validators.required],
      name: ['', Validators.required],
      prn: [''],
      division: [''],
      batch: [''],
      bio: [''],
      role: [{ value: '', disabled: true }, Validators.required] // Disabled field
    });

    this.passwordChangeForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      reEnterNewPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(formGroup: FormGroup): any {
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
      const updatedUser = this.applicationUserForm.getRawValue(); // getRawValue to include disabled fields
      console.log('Updating user:', updatedUser);

      // Call your userService to update the user profile here
    
      
    }
  }

  onSubmitPasswordForm() {
    if (this.passwordChangeForm.valid) {
      const passwordData = this.passwordChangeForm.value;
      console.log('Password change request:', passwordData);

      // Call your userService to change the password here
      
    }
  }
}
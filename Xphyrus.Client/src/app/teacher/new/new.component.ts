import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent {
  name = 'Angular';
  public userForm: FormGroup;
  
  constructor(private _fb: FormBuilder) {
    this.userForm = this._fb.group({
      firstName: ['', Validators.minLength(3)],
      lastName: [''],
      address: this._fb.array([this.addAddressGroup()])
    });
  }

  private addAddressGroup(): FormGroup {
    return this._fb.group({
      street: [],
      city: [],
      state: [],
      contacts: this._fb.array([])
    });
  }

  private contactsGroup(): FormGroup {
    return this._fb.group({
      contactPerson: ['', Validators.required],
      phoneNumber: ['', [Validators.maxLength(10)]], 
    });
  }

  get addressArray(): FormArray {
    return <FormArray>this.userForm.get('address');
  }
 
  get contactsArray(): FormArray {
    return <FormArray>this.addressArray.value.get('contacts');
  }

  addAddress(): void {
    this.addressArray.push(this.addAddressGroup());
  }

  removeAddress(index: number): void {
    this.addressArray.removeAt(index);
  }

  addContact(index: number): void {
    (<FormArray>(<FormGroup>this.addressArray.controls[index]).controls['contacts']).push(this.contactsGroup());
  }

  deletePhoneNumber(control: any, index: number) {
    control.removeAt(index)
  }
  getControls() {
    return (this.userForm.get('contacts') as FormArray).controls;
  }
  get moew() { return <FormArray>this.addressArray.get('contacts'); }
}

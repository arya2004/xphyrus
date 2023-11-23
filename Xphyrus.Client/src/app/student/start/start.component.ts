import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {
  id: number;
  private sub: any;
  editorOptions = {theme: 'vs-dark', language: 'javascript'};
  code: string[]= ['function x() {\nconsole.log("Hello world!");\n}', "sd", "fg", 'gf'];
  options = {
    theme: 'vs-dark'
  };
  constructor(private _fb: FormBuilder,private route: ActivatedRoute) {
    this.userForm = this._fb.group({
      firstName: ['', Validators.minLength(3)],
      lastName: [''],
      address: this._fb.array([this.addAddressGroup()])
    });
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
       this.id = params['id']; // (+) converts string 'id' to a number

       // In a real app: dispatch action to load the details here.
    });
  }
  name = 'Angular';
  public userForm: FormGroup;
  
 

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

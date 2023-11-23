import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TeacherService } from '../teacher.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent {
  name = 'Angular';
  public userForm: FormGroup;
  
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  constructor(private _fb: FormBuilder, private router: Router, private teacherService: TeacherService) {
    this.userForm = this._fb.group({
      name: ['', Validators.minLength(3)],
      description: [''],
      isStrict: false,
      start: new FormControl(),
      end: new FormControl(),
      duration: 1,
      codings: this._fb.array([this.addAddressGroup()])
    });
  }

  private addAddressGroup(): FormGroup {
    return this._fb.group({
      title: [],
      prompt: [],
      language: [],
      inputFormat: [],
      outputFormat: [],
      constrain1: [],
      constrain2:[],
      constrain3: [],
      evliationCases: this._fb.array([])
    });
  }

  private contactsGroup(): FormGroup {
    return this._fb.group({
      inputCase: ['', Validators.required],
      outputCase: ['', [Validators.maxLength(10)]], 
    });
  }

  get addressArray(): FormArray {
    return <FormArray>this.userForm.get('codings');
  }
 
  get contactsArray(): FormArray {
    return <FormArray>this.addressArray.value.get('evliationCases');
  }

  addAddress(): void {
    this.addressArray.push(this.addAddressGroup());
  }

  removeAddress(index: number): void {
    this.addressArray.removeAt(index);
  }

  addContact(index: number): void {
    (<FormArray>(<FormGroup>this.addressArray.controls[index]).controls['evliationCases']).push(this.contactsGroup());
  }

  deletePhoneNumber(control: any, index: number) {
    control.removeAt(index)
  }
  getControls() {
    return (this.userForm.get('evliationCases') as FormArray).controls;
  }
  createTest(){
    console.log(this.userForm.value);
    this.teacherService.postAssignment(this.userForm.value).subscribe({
      next: () => this.router.navigateByUrl('/teacher/')
    })
  }
    
  
}

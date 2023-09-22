import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { CodingDto } from '../shared/models/IAssesmentCreate';

@Component({
  selector: 'app-newassesment',
  templateUrl: './newassesment.component.html',
  styleUrls: ['./newassesment.component.scss']
})
export class NewassesmentComponent {
  codingForm: FormGroup = new FormGroup({});

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    // Create a form group for CodingDto
    this.codingForm = this.formBuilder.group({
      Title: [''],
      Prompt: [''],
      Language: [''],
      InputFormat: [''],
      OutputFormat: [''],
      Constrain1: [''],
      Constrain2: [''],
      Constrain3: [''],
      EvliationCases: this.formBuilder.array([]), // Form array for EvliationCases
    });
  }

  // Helper method to add a new EvliationCaseDto form group to the array
  addEvaluationCase() {
    const evaluationCase = this.formBuilder.group({
      InputCase: [''],
      OutputCase: [''],
    });
    this.getEvaluationCasesArray().push(evaluationCase);
  }

  // Helper method to get the EvliationCases form array
  getEvaluationCasesArray() {
    return this.codingForm.get('EvliationCases') as FormArray;
  }

  // Submit the form
  onSubmit() {
    const formData = this.codingForm.value as CodingDto;
    // Handle form submission here
    console.log(formData);
    
  }
}

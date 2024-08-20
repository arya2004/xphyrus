import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-coding-question-dashboard',
  templateUrl: './coding-question-dashboard.component.html',
  styleUrls: ['./coding-question-dashboard.component.scss']
})
export class CodingQuestionDashboardComponent implements OnInit {
  testCaseForm: FormGroup;

  constructor(private fb: FormBuilder) {
    // Initialize the form
    this.testCaseForm = this.fb.group({
      inputCase: ['', Validators.required],
      outputCase: ['', Validators.required],
      description: [''],
      isHidden: [false],
      marks: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    // You can add any additional initialization logic here if needed.
  }

  onSubmit(): void {
    if (this.testCaseForm.valid) {
      const testCaseData = this.testCaseForm.value;
      // Handle the save logic here, e.g., send the data to a server or log it
      console.log('Test Case Data:', testCaseData);
      
      // Reset the form after saving if needed
      this.testCaseForm.reset({
        inputCase: '',
        outputCase: '',
        description: '',
        isHidden: false,
        marks: 0
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
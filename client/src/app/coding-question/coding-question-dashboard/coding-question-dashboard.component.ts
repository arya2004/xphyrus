import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { CodingQuestionService, CreateTestCase, TestCase } from '../coding-question.service';

@Component({
  selector: 'app-coding-question-dashboard',
  templateUrl: './coding-question-dashboard.component.html',
  styleUrls: ['./coding-question-dashboard.component.scss']
})
export class CodingQuestionDashboardComponent implements OnInit {
  testCaseForm: FormGroup;
  testCases: TestCase[] = [];
  codingQuestionId: string;

  constructor(
    private fb: FormBuilder,
    private testCaseService: CodingQuestionService,
    private route: ActivatedRoute
  ) {}

  /**
   * Lifecycle hook that is called after data-bound properties are initialized.
   */
  ngOnInit(): void {
    this.initializeForm();
    this.extractRouteParams();
    this.fetchTestCases();
  }

  /**
   * Initializes the test case form with validation rules.
   */
  private initializeForm(): void {
    this.testCaseForm = this.fb.group({
      inputCase: [''],
      outputCase: ['', Validators.required],
      description: [''],
      isHidden: [false],
      marks: ['', [Validators.required, this.nonNegativeValidator]]
    });
  }

  /**
   * Extracts the coding question ID from the route parameters.
   */
  private extractRouteParams(): void {
    this.codingQuestionId = this.route.snapshot.paramMap.get('questionId') || '';
    if (!this.codingQuestionId) {
      console.warn('Coding question ID is missing from the route.');
    }
  }

  /**
   * Validator to ensure the marks field is not negative.
   * @param control - The form control to validate.
   * @returns An object containing the validation error or null if valid.
   */
  private nonNegativeValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const value = control.value;
    if (value && +value < 0) {
      return { negativeMarks: true };
    }
    return null;
  }

  /**
   * Fetches the list of test cases associated with the current coding question.
   */
  fetchTestCases(): void {
    this.testCaseService.getTestCasesByCodingQuestion(this.codingQuestionId).subscribe({
      next: data => {
        if (data.isSuccess) {
          this.testCases = data.result;
          console.log('Test cases fetched successfully:', this.testCases);
        } else {
          console.error('Failed to fetch test cases:', data.message);
        }
      },
      error: err => {
        console.error('Error fetching test cases:', err);
      }
    });
  }

  /**
   * Handles the submission of the test case form.
   * Submits the form data if the form is valid.
   */
  onSubmit(): void {
    if (this.testCaseForm.invalid) {
      console.warn('Form is invalid:', this.testCaseForm.errors);
      return;
    }

    const testCase: CreateTestCase = {
      ...this.testCaseForm.value,
      codingQuestionId: this.codingQuestionId
    };

    console.log('Creating test case:', testCase);

    this.testCaseService.createTestCase(testCase).subscribe({
      next: data => {
        if (data.isSuccess) {
          console.log('Test case created successfully:', data.result);
          this.testCaseForm.reset();
          this.fetchTestCases(); // Refresh the list of test cases after creating a new one
        } else {
          console.error('Failed to create test case:', data.message);
        }
      },
      error: err => {
        console.error('Creation failed:', err);
      }
    });
  }
}
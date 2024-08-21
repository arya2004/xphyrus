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
  constructor(private fb: FormBuilder,    private testCaseService: CodingQuestionService,
    private route: ActivatedRoute) {
    // Initialize the form
    this.testCaseForm = this.fb.group({
      inputCase: ['',],
      outputCase: ['', Validators.required],
      description: ['', ],
      isHidden: [false],
      marks: ['', [Validators.required, this.nonNegativeValidator]],
    });
    this.codingQuestionId = this.route.snapshot.paramMap.get('questionId') || '';
 
  }

  ngOnInit(): void {

    this.fetchTestCases();
  
  }

  nonNegativeValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const value = control.value;
    if (value && +value < 0) {
      return { negativeMarks: true };
    }
    return null;
  }

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
  onSubmit(): void {
    if (this.testCaseForm.valid) {
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
    } else {
      console.log('Form is invalid');
    }
  }
}
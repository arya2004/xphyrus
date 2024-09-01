import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Test } from 'src/app/shared/models/Test';

import { AssessmentService, CodingQuestion, TeacherTestMetadataDto } from '../assessment.service';


@Component({
  selector: 'app-assessment-dashboard',
  templateUrl: './assessment-dashboard.component.html',
  styleUrls: ['./assessment-dashboard.component.scss']
})
export class AssessmentDashboardComponent implements OnInit {
  testForm: FormGroup;
  classroomId: string;
  testId: string;
  questions: CodingQuestion[] = [];
  testMetadata: TeacherTestMetadataDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private codingQuestionService: AssessmentService
  ) {}

  /**
   * Lifecycle hook that is called after data-bound properties are initialized.
   */
  ngOnInit(): void {
    this.initializeForm();
    this.extractRouteParams();
    this.fetchCodingQuestions();

    this.codingQuestionService.getTestMetadata(this.testId).subscribe(
      (response) => {
        if (response.isSuccess) {
          this.testMetadata = response.result;
        } else {
          console.error(response.message);
        }
      },
      (error) => {
        console.error('Error fetching test metadata', error);
      }
    );
  }

  /**
   * Initializes the test form with validation rules.
   */
  private initializeForm(): void {
    this.testForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      duration: [0, Validators.required]
    });
  }

  /**
   * Extracts classroomId and testId from the route parameters.
   */
  private extractRouteParams(): void {
    this.classroomId = this.route.snapshot.paramMap.get('classroomId') || '';
    this.testId = this.route.snapshot.paramMap.get('testId') || '';

    if (!this.classroomId) {
      console.warn('Classroom ID is missing from the route.');
    }

    if (!this.testId) {
      console.warn('Test ID is missing from the route.');
    }
  }

  /**
   * Handles the creation of a new test.
   * Submits the form data if the form is valid.
   */
  onTestCreate(): void {
    if (this.testForm.invalid) {
      console.warn('Form is invalid:', this.testForm.errors);
      return;
    }

    const formData: Test = {
      title: this.testForm.value.title,
      description: this.testForm.value.description,
      startDate: new Date(this.testForm.value.startDate),
      endDate: new Date(this.testForm.value.endDate),
      duration: this.testForm.value.duration
    };

    console.log('Test created:', formData);

    // Reset the form after creation
    this.testForm.reset();
  }

  /**
   * Generates a dynamic link to navigate to a specific coding question.
   * @param questionId - The ID of the question to navigate to.
   * @returns The generated link as a string.
   */
  generateLink(questionId: string): string {
    return `/classroom/${this.classroomId}/test/${this.testId}/question/${questionId}`;
  }

  /**
   * Navigates to the specified link.
   * @param link - The route to navigate to.
   */
  navigateTo(link: string): void {
    this.router.navigateByUrl(link);
  }

  /**
   * Fetches coding questions associated with the current test.
   */
  fetchCodingQuestions(): void {
    this.codingQuestionService.getCodingQuestionsByTest(this.testId).subscribe({
      next: data => {
        if (data.isSuccess) {
          this.questions = data.result;
          console.log('Coding questions fetched successfully:', this.questions);
        } else {
          console.error('Failed to fetch coding questions:', data.message);
        }
      },
      error: err => {
        console.error('Error fetching coding questions:', err);
      }
    });
  }
}
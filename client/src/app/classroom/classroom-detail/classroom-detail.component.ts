import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ClassroomService, Test, TestDto } from '../classroom.service';


@Component({
  selector: 'app-classroom-detail',
  templateUrl: './classroom-detail.component.html',
  styleUrls: ['./classroom-detail.component.scss']
})
export class ClassroomDetailComponent implements OnInit {
  classroomId: string;
  testForm: FormGroup;
  tests: Test[] = [];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private testService: ClassroomService
  ) {}

  /**
   * Initializes the component, sets up the form group, and fetches initial data.
   */
  ngOnInit(): void {
    this.initializeForm();
    this.subscribeToRouteChanges();
    this.fetchTests();
  }

  /**
   * Initializes the test form with validation rules.
   */
  private initializeForm(): void {
    this.testForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      startDate: ['', [Validators.required, this.startDateValidator]],
      endDate: ['', [Validators.required, this.endDateValidator.bind(this)]],
      duration: ['', [Validators.required, this.durationValidator.bind(this)]],
    });

    // Revalidate the duration and endDate whenever startDate or endDate changes
    this.testForm.get('startDate')?.valueChanges.subscribe(() => {
      this.testForm.get('duration')?.updateValueAndValidity();
      this.testForm.get('endDate')?.updateValueAndValidity();
    });

    this.testForm.get('endDate')?.valueChanges.subscribe(() => {
      this.testForm.get('duration')?.updateValueAndValidity();
    });
  }

  /**
   * Subscribes to route changes to capture the classroom ID.
   */
  private subscribeToRouteChanges(): void {
    this.route.url.subscribe({
      next: (urlSegments) => {
        this.classroomId = urlSegments[0]?.path || '';
        if (!this.classroomId) {
          console.warn('Classroom ID not found in the route.');
        } else {
          console.log('Classroom ID:', this.classroomId);
        }
      },
      error: (err) => {
        console.error('Error reading route parameters:', err);
      }
    });
  }

  /**
   * Handles the creation of a new test.
   * Submits the form data to the server if the form is valid.
   */
  onTestCreate(): void {
    if (this.testForm.invalid) {
      console.warn('Form is invalid:', this.testForm.errors);
      return;
    }

    const test: TestDto = {
      ...this.testForm.value,
      classroomId: this.classroomId
    };

    console.log('Creating test:', test);

    this.testService.createTest(test).subscribe({
      next: (data) => {
        if (data.isSuccess) {
          console.log('Test created successfully:', data.result);
          this.refreshTests();
        } else {
          console.error('Failed to create test:', data.message);
        }
      },
      error: (err) => {
        console.error('Creation failed:', err);
      }
    });
  }

  /**
   * Fetches the list of tests for the current classroom.
   */
  private fetchTests(): void {
    this.testService.getTestsByClassroom(this.classroomId).subscribe({
      next: (data) => {
        if (data.isSuccess) {
          this.tests = data.result;
          console.log('Tests fetched successfully:', this.tests);
        } else {
          console.error('Failed to fetch tests:', data.message);
        }
      },
      error: (err) => {
        console.error('Error fetching tests:', err);
      }
    });
  }

  /**
   * Refreshes the list of tests after a new test is created.
   */
  private refreshTests(): void {
    this.fetchTests();
  }

  /**
   * Redirects to the test details page for the selected test.
   * @param testId - The ID of the test to navigate to.
   */
  redirectToTest(testId: string): void {
    this.router.navigate(['/classroom', this.classroomId, 'test', testId]);
  }

  /**
   * Validator to ensure the start date is not in the past.
   * @param control - The form control to validate.
   * @returns An object containing the validation error or null if valid.
   */
  startDateValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const startDate = control.value ? new Date(control.value) : null;
    const currentDate = new Date();
    if (startDate && startDate < currentDate) {
      return { invalidStartDate: true };
    }
    return null;
  }

  /**
   * Validator to ensure the end date is not before the start date or in the past.
   * @param control - The form control to validate.
   * @returns An object containing the validation error or null if valid.
   */
  endDateValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const startDateControl = this.testForm?.get('startDate');
    const endDate = control.value ? new Date(control.value) : null;
    const startDate = startDateControl?.value ? new Date(startDateControl.value) : null;
    const currentDate = new Date();

    if (endDate && (endDate < currentDate || (startDate && endDate < startDate))) {
      return { invalidEndDate: true };
    }
    return null;
  }

  /**
   * Validator to ensure the duration is not greater than the difference between start and end dates.
   * @param control - The form control to validate.
   * @returns An object containing the validation error or null if valid.
   */
  durationValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const startDateControl = this.testForm?.get('startDate');
    const endDateControl = this.testForm?.get('endDate');
    const duration = control.value;

    const startDate = startDateControl?.value ? new Date(startDateControl.value) : null;
    const endDate = endDateControl?.value ? new Date(endDateControl.value) : null;

    if (startDate && endDate) {
      const differenceInMinutes = (endDate.getTime() - startDate.getTime()) / (1000 * 60);
      if (duration > differenceInMinutes) {
        return { invalidDuration: true };
      }
    }
    return null;
  }
}
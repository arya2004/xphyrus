import { Component, OnInit } from '@angular/core';
import { StudentCodingQuestionDto, StudentService, StudentTestDto } from '../student.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {
  testId: string;
  testDetails: StudentTestDto | null = null;
  classrooms: any[] = []; // Assume you fetch classrooms related to this test
  testCases: StudentCodingQuestionDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private studentService: StudentService
  ) {}

  /**
   * Lifecycle hook that is called after data-bound properties are initialized.
   */
  ngOnInit(): void {
    this.initializeTestId();
    this.fetchTestDetails();
  }

  /**
   * Initializes the test ID from the route parameters.
   */
  private initializeTestId(): void {
    this.testId = this.route.snapshot.paramMap.get('testId') || '';
    if (!this.testId) {
      console.warn('No test ID found in route parameters.');
    }
  }

  /**
   * Fetches the test details and related data based on the test ID.
   */
  private fetchTestDetails(): void {
    if (!this.testId) {
      console.error('Cannot fetch test details without a test ID.');
      return;
    }

    this.studentService.getExamDetails(this.testId).subscribe({
      next: data => {
        if (data.isSuccess) {
          this.testDetails = data.result.test;
          this.testCases = this.testDetails.codingQuestions;
          console.log('Test details fetched successfully:', this.testDetails);
        } else {
          console.error('Failed to fetch test details:', data.message);
        }
      },
      error: err => {
        console.error('Error fetching test details:', err);
      }
    });
  }

  /**
   * Navigates to the classroom details page.
   * @param classroomId - The ID of the classroom to navigate to.
   */
  redirectToClassroom(classroomId: string): void {
    if (!classroomId) {
      console.warn('Cannot navigate to classroom without a classroom ID.');
      return;
    }

    this.router.navigate(['/classroom', classroomId]).catch(err => {
      console.error('Navigation error:', err);
    });
  }

  /**
   * Returns the course type name based on the type value.
   * @param type - The course type as a number.
   * @returns The name of the course type.
   */
  getCourseTypeName(type: number): string {
    switch (type) {
      case 1:
        return 'Theory';
      case 2:
        return 'Tutorial';
      case 3:
        return 'Lab';
      default:
        console.warn('Unknown course type:', type);
        return 'Unknown';
    }
  }
}

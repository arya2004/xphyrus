import { Component } from '@angular/core';
import { StudentCodingQuestionDto, StudentService, StudentTestDto } from '../student.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent {
  testId: string;
  testDetails: StudentTestDto | null = null;
  classrooms: any[] = []; // Assume you fetch classrooms related to this test
  testCases: StudentCodingQuestionDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private studentDashboardService: StudentService
  ) {}

  ngOnInit(): void {
    // Get testId from the active route
    this.testId = this.route.snapshot.paramMap.get('testId') || '';

    // Fetch the test details using the testId
    this.fetchTestDetails();
  }

  fetchTestDetails(): void {
    this.studentDashboardService.getExamDetails(this.testId).subscribe({
      next: data => {
        if (data.isSuccess) {
          this.testDetails = data.result.test;
          this.testCases = this.testDetails.codingQuestions;
          // Assume classrooms are related to the test, and fetched similarly
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

  // Method to navigate to a classroom details page
  redirectToClassroom(classroomId: string): void {
    // Add logic to navigate to classroom
  }

  // Method to get the course type name
  getCourseTypeName(type: number): string {
    switch (type) {
      case 1:
        return 'Theory';
      case 2:
        return 'Tutorial';
      case 3:
        return 'Lab';
      default:
        return 'Unknown';
    }
  }
}

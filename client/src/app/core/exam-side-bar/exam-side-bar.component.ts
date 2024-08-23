import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ExamService, StartTestResponseDto } from 'src/app/exam/exam.service';
import { Exam } from 'src/app/shared/examSidebar';
import { CoreService } from '../core.service';

@Component({
  selector: 'app-exam-side-bar',
  templateUrl: './exam-side-bar.component.html',
  styleUrls: ['./exam-side-bar.component.scss']
})
export class ExamSideBarComponent implements OnInit {

  exam$: Observable<StartTestResponseDto | null>;

  constructor(private examService: ExamService) { }

  ngOnInit(): void {
    this.exam$ = this.examService.getTest();
    this.exam$.subscribe(exam => {
      console.log('Exam data:', exam);
    });
  }

   // Generate a router link for navigation to the test and specific questions
   generateRouterLink(examId: string, questionId: string): string {
    return `/exam/${examId}/q/${questionId}`;
  }

  generateRouterLinkDashboard(examId: string): string {
    return `/exam/${examId}`;
  }

  onSubmitTest(): void {
    if (this.exam$) {
      this.exam$.subscribe(exam => {
        if (exam) {
          this.examService.submitTest(exam.test.testId).subscribe({
            next: data => {
              if (data.isSuccess) {
                console.log('Test submitted successfully');
                // You can add additional actions here, like redirecting or showing a success message
              } else {
                console.error('Failed to submit test:', data.message);
              }
            },
            error: err => {
              console.error('Test submission failed:', err);
            }
          });
        }
      });
    }
  }
}

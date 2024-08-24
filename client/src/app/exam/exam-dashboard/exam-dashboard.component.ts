import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ExamService, StartTestResponseDto } from '../exam.service';



@Component({
  selector: 'app-exam-dashboard',
  templateUrl: './exam-dashboard.component.html',
  styleUrls: ['./exam-dashboard.component.scss']
})
export class ExamDashboardComponent   implements OnInit {

  test$: Observable<StartTestResponseDto | null>;
  markdown: string = '';

  constructor(
    private examService: ExamService,
    private cdRef: ChangeDetectorRef
  ) {}

  /**
   * Lifecycle hook that is called after data-bound properties are initialized.
   * Subscribes to test data and updates the markdown content.
   */
  ngOnInit(): void {
    this.loadTestData();
  }

  /**
   * Loads test data by subscribing to the service and handling the response.
   * Updates the markdown content based on the test description.
   */
  private loadTestData(): void {
    this.test$ = this.examService.getTest().pipe(
      catchError(err => {
        console.error('Error fetching test data:', err);
        return []; // Return an empty observable if an error occurs
      })
    );

    this.test$.subscribe({
      next: (testData) => {
        if (testData) {
          this.markdown = testData.test.description;
          this.cdRef.detectChanges(); // Trigger change detection to rerender
          console.log('Test data loaded successfully.');
        } else {
          console.warn('No test data available.');
        }
      },
      error: (err) => {
        console.error('Subscription error:', err);
      }
    });
  }

}

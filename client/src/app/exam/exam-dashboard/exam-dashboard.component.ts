import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ExamService, StartTestResponseDto } from '../exam.service';



@Component({
  selector: 'app-exam-dashboard',
  templateUrl: './exam-dashboard.component.html',
  styleUrls: ['./exam-dashboard.component.scss']
})
export class ExamDashboardComponent   implements OnInit {

  test$: Observable<StartTestResponseDto | null>;
  markdown: string = '';
  constructor(private examService: ExamService,private cdRef: ChangeDetectorRef) {
    

   }

  ngOnInit() {
    // Subscribe to the test data from the service
  
    this.test$ = this.examService.getTest();

    // Update markdown content based on the test description
    this.test$.subscribe(testData => {
      if (testData) {
        this.markdown = testData.test.description;
        this.cdRef.detectChanges(); // Trigger change detection to rerender
      }
    });
  }

}

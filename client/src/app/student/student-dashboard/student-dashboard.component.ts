import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ExamService } from 'src/app/exam/exam.service';
import { StudentAnswer } from 'src/app/shared/models/StudentAnswer';
import { StudentExamOverviewDto, StudentService } from '../student.service';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.scss']
})
export class StudentDashboardComponent implements OnInit {
  joinTestForm: FormGroup;
  exams: StudentExamOverviewDto[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
 

  constructor(private fb: FormBuilder, 
    private studentDashboardService: StudentService,
    private router: Router, private examService: ExamService) {
    this.joinTestForm = this.fb.group({
      testCode: ['', Validators.required]
    });
  }
 

  onJoinTest(): void {
    if (this.joinTestForm.valid) {
      const testId: string = this.joinTestForm.value.testCode;
      console.log('Starting test:', testId);

      this.examService.startTest(testId).subscribe({
        next: data => {
          if (data.isSuccess) {
            console.log('Test started successfully:', data.result);
            this.router.navigate([`/exam/${testId}`]);
          } else {
            console.error('Failed to start test:', data.message);
          }
        },
        error: err => {
          console.error('Test start failed:', err);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
 
  ngOnInit(): void {
   
    this.dtOptions = {
      pagingType: 'full_numbers',
      stateSave: true
    };
 

    this.fetchExamsTaken();
  }

  
  ngOnDestroy(): void {
    // Complete the DataTable trigger to avoid memory leaks
    this.dtTrigger.unsubscribe();
  }

  /**
   * Navigates to a specific page based on the selected exam.
   * @param examId - The ID of the exam to navigate to.
   */
  redirectToExam(examId: string): void {
    this.router.navigate(['/student/result', examId]);
  }


  private fetchExamsTaken(): void {
    this.studentDashboardService.getExamsTaken().subscribe({
      next: data => {
        if (data.isSuccess) {
          this.exams = data.result;
          this.dtTrigger.next(null); // Trigger DataTable rendering
          console.log('Exams fetched successfully:', this.exams);
        } else {
          console.error('Failed to fetch exams:', data.message);
        }
      },
      error: err => {
        console.error('Error fetching exams:', err);
      }
    });
  }



 
}

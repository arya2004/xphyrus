import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ExamService } from 'src/app/exam/exam.service';
import { StudentExamOverviewDto, StudentService } from '../student.service';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.scss']
})
export class StudentDashboardComponent implements OnInit, OnDestroy {
  joinTestForm: FormGroup;
  exams: StudentExamOverviewDto[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    private router: Router,
    private examService: ExamService
  ) {
    this.joinTestForm = this.fb.group({
      testCode: ['', Validators.required]
    });
  }

  /**
   * Lifecycle hook that is called after data-bound properties are initialized.
   */
  ngOnInit(): void {
    this.initializeDataTableOptions();
    this.fetchExamsTaken();
  }

  /**
   * Lifecycle hook that is called when the component is destroyed.
   * Unsubscribes from DataTable trigger to prevent memory leaks.
   */
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  /**
   * Initializes DataTable options.
   */
  private initializeDataTableOptions(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      stateSave: true
    };
  }

  /**
   * Handles the logic for joining a test.
   * Navigates to the test page if the test code is valid.
   */
  onJoinTest(): void {
    if (this.joinTestForm.invalid) {
      console.warn('Join test form is invalid:', this.joinTestForm.errors);
      return;
    }

    const testId: string = this.joinTestForm.value.testCode;
    console.log('Starting test with ID:', testId);

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
  }

  /**
   * Fetches the exams that the student has taken.
   * Triggers DataTable rendering after successful data retrieval.
   */
  private fetchExamsTaken(): void {
    this.studentService.getExamsTaken().subscribe({
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

  /**
   * Navigates to the exam result page for a specific exam.
   * @param examId - The ID of the exam to navigate to.
   */
  redirectToExam(examId: string): void {
    if (!examId) {
      console.warn('Cannot navigate to exam without an exam ID.');
      return;
    }

    this.router.navigate(['/student/result', examId]).catch(err => {
      console.error('Navigation error:', err);
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ExamService } from 'src/app/exam/exam.service';
import { StudentAnswer } from 'src/app/shared/models/StudentAnswer';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.scss']
})
export class StudentDashboardComponent implements OnInit {
  joinTestForm: FormGroup;

  testResults: StudentAnswer[] = [];
 

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  newNexusForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private examService: ExamService) {
    this.joinTestForm = this.fb.group({
      testCode: ['', Validators.required]
    });
  }
  // onJoinTest(): void {
  //   const testCode = this.joinTestForm.value.testCode;
  //   if (this.joinTestForm.valid) {
    
  //     // Redirect to the exam page with the test code
  //     this.router.navigate([`/exam/${testCode}`]);
  //   }else{
  //     console.log('Form is invalid');
  //     this.router.navigate([`/exam/${testCode}`]);
  //   }
  // }

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
      pagingType: 'full_numbers'
    };
    this.getAllCompany();

    this.testResults = [
      { test: 'Test 1', marksAwarded: 85, submittedDate: new Date(), numberOfCodingQuestion: 5 },
      { test: 'Test 2', marksAwarded: 90, submittedDate: new Date(), numberOfCodingQuestion: 3 },
      // More test results
    ];
  }

  
  ngOnDestroy(): void {
    // Complete the DataTable trigger to avoid memory leaks
    this.dtTrigger.unsubscribe();
  }


  getAllCompany(): void {
  
  }



 
}

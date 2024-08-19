import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
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

  constructor(private fb: FormBuilder, private router: Router) {
    this.joinTestForm = this.fb.group({
      testCode: ['', Validators.required]
    });
  }
  onJoinTest(): void {
    const testCode = this.joinTestForm.value.testCode;
    if (this.joinTestForm.valid) {
    
      // Redirect to the exam page with the test code
      this.router.navigate([`/exam/${testCode}`]);
    }else{
      console.log('Form is invalid');
      this.router.navigate([`/exam/${testCode}`]);
    }
  }
  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
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

  /**
   * Lifecycle hook that is called when the component is destroyed.
   */
  ngOnDestroy(): void {
    // Complete the DataTable trigger to avoid memory leaks
    this.dtTrigger.unsubscribe();
  }

  /**
   * Fetch all companies from the service.
   */
  getAllCompany(): void {
  
  }



 
}

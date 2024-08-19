import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

import { CodingQuestion, Difficulty } from 'src/app/shared/models/CodingQuestion';
import { INexusDashboard } from 'src/app/shared/models/INexus';
import { Test } from 'src/app/shared/models/Test';
import { NexusService } from '../../nexus.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent  implements OnInit {
  testForm: FormGroup;
  onTestCreate(): void {
    if (this.testForm.valid) {
      const formData: Test = {
        title: this.testForm.value.title,
        description: this.testForm.value.description,
        startDate: new Date(this.testForm.value.startDate),
        endDate: new Date(this.testForm.value.endDate),
        duration: this.testForm.value.duration
      };
   
      console.log('Test created:', formData);

      // Reset the form and close the modal if needed
      this.testForm.reset();
    } else {
      console.log('Form is invalid');
    }
  }

  deleteQuestion(title: string): void {
    this.codingQuestions = this.codingQuestions.filter(q => q.title !== title);
}
 
    codingQuestions: CodingQuestion[] = [
      {
          title: 'Two Sum',
          difficulty: Difficulty.Easy,
          totalTestCases: 10,
          totalMarks: 100
      },
      {
          title: 'Binary Search',
          difficulty: Difficulty.Medium,
          totalTestCases: 15,
          totalMarks: 150
      },
      {
          title: 'Merge Intervals',
          difficulty: Difficulty.Hard,
          totalTestCases: 20,
          totalMarks: 200
      }
  ];
  classroomId = 123;
  testId = 1;



 
  
  nexus: INexusDashboard[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  newNexusForm: FormGroup;

  constructor(
    private companyService: NexusService, 
    private fb: FormBuilder, 
    private router: Router
  ) {

  }


  ngOnInit(): void {
    this.testForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      duration: [0, Validators.required]
    });
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
    this.getAllCompany();
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
    this.companyService.getNexus().subscribe({
      next: res => {
        this.nexus = res.result.map((c : INexusDashboard) => {
          // Convert the creationDate to the desired format
          const date = new Date(c.creationDate);
          const formattedDate = date.toISOString().split('T')[0];
          return { ...c, creationDate: formattedDate };
        });
        this.dtTrigger.next(null);
      },
      error: err => {
        console.error('Error fetching companies:', err);
        alert('There was an error fetching the companies. Please try again later.');
      }
    });
  }

  /**
   * Delete a company by its ID.
   * @param id - The ID of the company to delete
   */
  deleteTest(id: string): void {
    if (confirm('Are you sure you want to delete this company?')) {
      this.companyService.deleteNexus(id).subscribe({
        next: res => {
          console.log('Company deleted:', res);
          window.location.reload();  
        },
        error: err => {
          console.error('Error deleting company:', err);
          alert('There was an error deleting the company. Please try again later.');
        }
      });
    }
  }

 
}

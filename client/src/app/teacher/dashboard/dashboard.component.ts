import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { NexusService } from 'src/app/nexus/nexus.service';
import { ICompany } from 'src/app/shared/models/ICompany';
import { INexusDashboard } from 'src/app/shared/models/INexus';
import { Classroom } from 'src/app/shared/models/teacher/Classroom';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent  implements OnInit {
  classroomForm: FormGroup;
  courseTypes = [
    { value: 1, viewValue: 'Theory' },
    { value: 2, viewValue: 'Tutorial' },
    { value: 3, viewValue: 'Lab' }
  ];

  classrooms: Classroom[] = [
    {
      classroomId: '1',
      name: 'Math 101',
      number: 'A1',
      description: 'Basic Mathematics',
      type: 1, // Theory
      creationDate: '2024-08-19T01:30:00.000-05:00'
    },
    {
      classroomId: '2',
      name: 'Physics Lab',
      number: 'B2',
      description: 'Introduction to Physics Lab',
      type: 3, // Lab
      creationDate: '2024-08-19T09:00:00.000-05:00'
    },
    {
      classroomId: '3',
      name: 'English 201',
      number: 'C3',
      description: 'Advanced English Literature',
      type: 1, // Theory
      creationDate: '2024-08-20T11:00:00.000-05:00'
    },
    {
      classroomId: '4',
      name: 'Chemistry Tutorial',
      number: 'D4',
      description: 'Chemistry Problem Solving Session',
      type: 2, // Tutorial
      creationDate: '2024-08-21T14:00:00.000-05:00'
    },
    {
      classroomId: '5',
      name: 'Computer Science 101',
      number: 'E5',
      description: 'Introduction to Computer Science',
      type: 1, // Theory
      creationDate: '2024-08-22T16:30:00.000-05:00'
    }
  ];

  onClassroomCreate(): void {
    if (this.classroomForm.valid) {
      console.log('Classroom created:', this.classroomForm.value);
    } else {
      console.log('Form is invalid');
    }
  }
 




 
  company: ICompany[] = [];
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
  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
    this.classroomForm = this.fb.group({
      name: ['', Validators.required],
      number: ['', Validators.required],
      description: [''], // Description is now optional
      type: ['', Validators.required],
    
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
  deleteClassroom(id: string): void {
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';


import { ClassroomService } from '../classroom.service';
import { Classroom } from 'src/app/shared/models/teacher/Classroom';



export interface createClassroom {
  name: string;
  number: string;
  description: string;
  type: number;
}



@Component({
  selector: 'app-classroom-dashboard',
  templateUrl: './classroom-dashboard.component.html',
  styleUrls: ['./classroom-dashboard.component.scss']
})
export class ClassroomDashboardComponent implements OnInit {
  classroomForm: FormGroup;
  courseTypes = [
    { value: 1, viewValue: 'Theory' },
    { value: 2, viewValue: 'Tutorial' },
    { value: 3, viewValue: 'Lab' }
  ];

  classrooms: Classroom[] = []

  
  constructor(

    private fb: FormBuilder, 
    private router: Router,
    private classroomService: ClassroomService
  ) {

  }
  
  ngOnInit(): void {
    this.classroomForm = this.fb.group({
      name: ['', Validators.required],
      number: ['', Validators.required],
      description: [''], // Description is now optional
      type: ['', Validators.required],
    
    });

    this.fetchClassrooms();
  }

  responseOutput: any;
  onClassroomCreate(): void {
    if (this.classroomForm.valid) {
      const classroom: createClassroom = {
        name: this.classroomForm.value.name,
        number: this.classroomForm.value.number,
        description: this.classroomForm.value.description,
        type: parseInt(this.classroomForm.value.type,10)
      };
      console.log('Creating classroom:', classroom);

      this.classroomService.createClassroom(classroom).subscribe({
        next: data => {
          this.responseOutput = data.result;
          window.location.reload()
          console.log('Classroom created successfully:', data.result);
        },
        error: err => {
          console.error('Creation failed:', err);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
 


  redirectToClassroom(classroomId: string) {
    this.router.navigate(['/classroom', classroomId]);
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
 



  fetchClassrooms(): void {
    this.classroomService.getMyClassrooms().subscribe({
      next: data => {
        if (data.isSuccess) {
          this.classrooms = data.result;
          console.log('Classrooms fetched successfully:', this.classrooms);
        } else {
          console.error('Failed to fetch classrooms:', data.message);
        }
      },
      error: err => {
        console.error('Error fetching classrooms:', err);
      }
    });
  }

  /**
   * Delete a company by its ID.
   * @param id - The ID of the company to delete
   */
  deleteClassroom(id: string): void {
    // if (confirm('Are you sure you want to delete this company?')) {
    //   this.companyService.deleteNexus(id).subscribe({
    //     next: res => {
    //       console.log('Company deleted:', res);
    //       window.location.reload();  
    //     },
    //     error: err => {
    //       console.error('Error deleting company:', err);
    //       alert('There was an error deleting the company. Please try again later.');
    //     }
    //   });
    // }
  }

}

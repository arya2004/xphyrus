import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
  classrooms: Classroom[] = [];
  responseOutput: any;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private classroomService: ClassroomService
  ) {}

  /**
   * Initializes the component, sets up the form group, and fetches the list of classrooms.
   */
  ngOnInit(): void {
    this.initializeForm();
    this.fetchClassrooms();
  }

  /**
   * Initializes the classroom form with validation rules.
   */
  private initializeForm(): void {
    this.classroomForm = this.fb.group({
      name: ['', Validators.required],
      number: ['', Validators.required],
      description: [''], // Description is optional
      type: ['', Validators.required]
    });
  }

  /**
   * Handles the creation of a new classroom.
   * Submits the form data to the server if the form is valid.
   */
  onClassroomCreate(): void {
    if (this.classroomForm.invalid) {
      console.warn('Form is invalid:', this.classroomForm.errors);
      return;
    }

    const classroom: createClassroom = {
      name: this.classroomForm.value.name,
      number: this.classroomForm.value.number,
      description: this.classroomForm.value.description,
      type: parseInt(this.classroomForm.value.type, 10)
    };

    console.log('Creating classroom:', classroom);

    this.classroomService.createClassroom(classroom).subscribe({
      next: data => {
        this.responseOutput = data.result;
        console.log('Classroom created successfully:', data.result);
        this.refreshClassrooms(); // Refetch the classrooms after creation
      },
      error: err => {
        console.error('Failed to create classroom:', err);
      }
    });
  }

  /**
   * Navigates to the classroom details page for the selected classroom.
   * @param classroomId - The ID of the classroom to navigate to.
   */
  redirectToClassroom(classroomId: string): void {
    this.router.navigate(['/classroom', classroomId]);
  }

  /**
   * Fetches the list of classrooms from the server.
   */
  private fetchClassrooms(): void {
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
   * Refreshes the list of classrooms by fetching the data from the server.
   */
  private refreshClassrooms(): void {
    this.fetchClassrooms();
  }

  /**
   * Returns the name of the course type based on the provided type value.
   * @param type - The numerical value representing the course type.
   * @returns The name of the course type.
   */
  getCourseTypeName(type: number): string {
    switch (type) {
      case 1:
        return 'Theory';
      case 2:
        return 'Tutorial';
      case 3:
        return 'Lab';
      default:
        console.warn('Unknown course type:', type);
        return 'Unknown';
    }
  }
}
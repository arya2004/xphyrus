import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ClassroomService, Test, TestDto } from '../classroom.service';


@Component({
  selector: 'app-classroom-detail',
  templateUrl: './classroom-detail.component.html',
  styleUrls: ['./classroom-detail.component.scss']
})
export class ClassroomDetailComponent {
  classroomId: string;
  testForm: FormGroup;
  tests: Test[] = [];





  constructor(  
    private route: ActivatedRoute,
    private fb: FormBuilder, 
    private router: Router,
    private testService: ClassroomService,
  ) {

  }

  ngOnInit(): void {
    this.testForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      startDate: ['', [Validators.required, this.startDateValidator]],
      endDate: ['', [Validators.required, this.endDateValidator.bind(this)]],
      duration: ['', [Validators.required, this.durationValidator.bind(this)]],
    });

    // Revalidate the duration whenever startDate or endDate changes
    this.testForm.get('startDate')?.valueChanges.subscribe(() => {
      this.testForm.get('duration')?.updateValueAndValidity();
      this.testForm.get('endDate')?.updateValueAndValidity();
    });

    this.testForm.get('endDate')?.valueChanges.subscribe(() => {
      this.testForm.get('duration')?.updateValueAndValidity();
    });
   


    this.route.url.subscribe(urlSegments => {
      this.classroomId = urlSegments[0].path;
      console.log(this.classroomId);
  
    });

    this.fetchTests();
  }



  onTestCreate(): void {
    if (this.testForm.valid) {
      const test: TestDto = {
        ...this.testForm.value,
        classroomId: this.classroomId
      };

      this.testService.createTest(test).subscribe({
        next: data => {
          if (data.isSuccess) {
            console.log('Test created successfully:', data.result);
            window.location.reload() // Refresh the list of tests after creating a new one
          } else {
            console.error('Failed to create test:', data.message);
          }
        },
        error: err => {
          console.error('Creation failed:', err);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  
  }

  
 
 



    fetchTests(): void {
      this.testService.getTestsByClassroom(this.classroomId).subscribe({
        next: data => {
          if (data.isSuccess) {
            this.tests = data.result;
            console.log('Tests fetched successfully:', this.tests);
          } else {
            console.error('Failed to fetch tests:', data.message);
          }
        },
        error: err => {
          console.error('Error fetching tests:', err);
        }
      });
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
  

  startDateValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const startDate = control.value ? new Date(control.value) : null;
    const currentDate = new Date();
    if (startDate && startDate < currentDate) {
      return { invalidStartDate: true };
    }
    return null;
  }

  endDateValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const startDateControl = this.testForm?.get('startDate');
    const endDate = control.value ? new Date(control.value) : null;
    const startDate = startDateControl?.value ? new Date(startDateControl.value) : null;
    const currentDate = new Date();

    if (endDate && (endDate < currentDate || (startDate && endDate < startDate))) {
      return { invalidEndDate: true };
    }
    return null;
  }

  durationValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const startDateControl = this.testForm?.get('startDate');
    const endDateControl = this.testForm?.get('endDate');
    const duration = control.value;

    const startDate = startDateControl?.value ? new Date(startDateControl.value) : null;
    const endDate = endDateControl?.value ? new Date(endDateControl.value) : null;

    if (startDate && endDate) {
      const differenceInMinutes = (endDate.getTime() - startDate.getTime()) / (1000 * 60);
      if (duration > differenceInMinutes) {
        return { invalidDuration: true };
      }
    }
    return null;
  }
  /**
   * Fetch all companies from the service.
   */
  getAllCompany(): void {
    // this.companyService.getNexus().subscribe({
    //   next: res => {
    //     this.nexus = res.result.map((c : INexusDashboard) => {
    //       // Convert the creationDate to the desired format
    //       const date = new Date(c.creationDate);
    //       const formattedDate = date.toISOString().split('T')[0];
    //       return { ...c, creationDate: formattedDate };
    //     });
    //     this.dtTrigger.next(null);
    //   },
    //   error: err => {
    //     console.error('Error fetching companies:', err);
    //     alert('There was an error fetching the companies. Please try again later.');
    //   }
    // });
  }

  redirectToTest(testId: any) {
    this.router.navigate(['/classroom',this.classroomId,'test', testId]);
  }
  
  
  /**
   * Delete a company by its ID.
   * @param id - The ID of the company to delete
   */
  deleteTest(id: string): void {
  //   if (confirm('Are you sure you want to delete this company?')) {
  //     this.companyService.deleteNexus(id).subscribe({
  //       next: res => {
  //         console.log('Company deleted:', res);
  //         window.location.reload();  
  //       },
  //       error: err => {
  //         console.error('Error deleting company:', err);
  //         alert('There was an error deleting the company. Please try again later.');
  //       }
  //     });
  //   }
  }

}

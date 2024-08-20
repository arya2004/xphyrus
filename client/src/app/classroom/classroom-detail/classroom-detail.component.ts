import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { INexusDashboard } from 'src/app/shared/models/INexus';
import { Test } from 'src/app/shared/models/Test';


@Component({
  selector: 'app-classroom-detail',
  templateUrl: './classroom-detail.component.html',
  styleUrls: ['./classroom-detail.component.scss']
})
export class ClassroomDetailComponent {
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
      this.tests.push(formData);
      console.log('Test created:', formData);

      // Reset the form and close the modal if needed
      this.testForm.reset();
    } else {
      console.log('Form is invalid');
    }
  }

    tests: Test[] = [
      {
        title: 'Math Test 1',
        description: 'First math test covering algebra and geometry',
        startDate: new Date(Date.now()),
        endDate: new Date(Date.now() + 2 * 60 * 60 * 1000), // 2 hours later
        duration: 120 // Duration in minutes
      },
      {
        title: 'Physics Test 1',
        description: 'Basic physics test including motion and forces',
        startDate: new Date(Date.now() + 24 * 60 * 60 * 1000), // 1 day later
        endDate: new Date(Date.now() + 25.5 * 60 * 60 * 1000), // 1.5 hours later
        duration: 90
      },
      {
        title: 'Chemistry Test 1',
        description: 'Introductory test on chemical reactions',
        startDate: new Date(Date.now() + 48 * 60 * 60 * 1000), // 2 days later
        endDate: new Date(Date.now() + 49.5 * 60 * 60 * 1000), // 1.5 hours later
        duration: 90
      },
      {
        title: 'English Literature Test',
        description: 'Test on English literature covering 18th-century poets',
        startDate: new Date(Date.now() + 72 * 60 * 60 * 1000), // 3 days later
        endDate: new Date(Date.now() + 73.5 * 60 * 60 * 1000), // 1.5 hours later
        duration: 90
      },
      {
        title: 'Biology Test 1',
        description: 'First biology test on cell biology and genetics',
        startDate: new Date(Date.now() + 96 * 60 * 60 * 1000), // 4 days later
        endDate: new Date(Date.now() + 98 * 60 * 60 * 1000), // 2 hours later
        duration: 120
      }
    ];
 
 




 
  company: ICompany[] = [];
  nexus: INexusDashboard[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  newNexusForm: FormGroup;

  constructor(
 
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

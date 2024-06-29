import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INexusDashboard } from 'src/app/shared/models/INexus';

/**
 * Component for managing the Nexus dashboard.
 */
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  nexusForm: FormGroup;

  typeOfPositions = [
    { value: 0, viewValue: 'Full-Time Employee' },
    { value: 1, viewValue: 'Contractor' },
    { value: 2, viewValue: 'Cofounder' },
    { value: 3, viewValue: 'Intern' }
  ];

  remotePolicies = [
    { value: 0, viewValue: 'In Office/WFH Flexible' },
    { value: 1, viewValue: 'In Office Not WFH Flexible' },
    { value: 2, viewValue: 'Onsite or Remote' },
    { value: 3, viewValue: 'Remote Only' }
  ];

  currencies = [
    { value: 'USD', viewValue: 'United States Dollar' },
    { value: 'EUR', viewValue: 'Euro' },
    { value: 'GBP', viewValue: 'British Pound Sterling' },
    { value: 'JPY', viewValue: 'Japanese Yen' },
    { value: 'AUD', viewValue: 'Australian Dollar' },
    { value: 'CAD', viewValue: 'Canadian Dollar' },
    { value: 'CHF', viewValue: 'Swiss Franc' },
    { value: 'CNY', viewValue: 'Chinese Yuan' },
    { value: 'INR', viewValue: 'Indian Rupee' },
    { value: 'RUB', viewValue: 'Russian Ruble' },
    { value: 'BRL', viewValue: 'Brazilian Real' },
    { value: 'ZAR', viewValue: 'South African Rand' },
    { value: 'SEK', viewValue: 'Swedish Krona' },
    { value: 'NZD', viewValue: 'New Zealand Dollar' },
    { value: 'MXN', viewValue: 'Mexican Peso' },
    { value: 'SGD', viewValue: 'Singapore Dollar' },
    { value: 'HKD', viewValue: 'Hong Kong Dollar' },
    { value: 'NOK', viewValue: 'Norwegian Krone' },
    { value: 'KRW', viewValue: 'South Korean Won' },
    { value: 'TRY', viewValue: 'Turkish Lira' },
    { value: 'SAR', viewValue: 'Saudi Riyal' },
    { value: 'AED', viewValue: 'United Arab Emirates Dirham' },
    { value: 'ARS', viewValue: 'Argentine Peso' },
    { value: 'COP', viewValue: 'Colombian Peso' },
    { value: 'IDR', viewValue: 'Indonesian Rupiah' },
    { value: 'ILS', viewValue: 'Israeli Shekel' },
    { value: 'PLN', viewValue: 'Polish Zloty' },
    { value: 'THB', viewValue: 'Thai Baht' },
    { value: 'VND', viewValue: 'Vietnamese Dong' }
  ];
  




  onSubmit(): void {
    if (this.nexusForm.valid) {
      console.log(this.nexusForm.value);
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
    // Initialize form
    this.newNexusForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', [Validators.required]],
    });

    this.nexusForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      typeOfPosition: [0, Validators.required],
      primaryRole: [''],
      workExperience: [0, [Validators.required, Validators.min(0)]],
      skills: [''],
      location: [''],
      acceptApplicantsWhoNeedToRelocate: [false],
      relocationAssistance: [false],
      remotePolicy: [0, Validators.required],
      currency: ['USD', Validators.required],
      annualSalaryMin: [null, Validators.min(0)],
      annualSalaryMax: [null, Validators.min(0)],
      equity: [false],
      equityMin: [null, Validators.min(0)],
      equityMax: [null, Validators.min(0)],
    
  
    });
  }

  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
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
  deleteCompany(id: string): void {
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

  /**
   * Handle the creation of a new Nexus.
   */
  onNewNexusCreate(): void {
    if (this.newNexusForm.valid) {
      console.log('Form Value:', this.newNexusForm.value);

      this.companyService.postNexus(this.newNexusForm.value).subscribe({
        next: () =>  window.location.reload(),
        error: err => {
          console.error('Error creating Nexus:', err);
          alert('There was an error creating the Nexus. Please try again later.');
        }
      });
    } else {
      alert('Please fill out all required fields correctly.');
    }
  }
}

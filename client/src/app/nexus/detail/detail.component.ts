import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { ICompany } from 'src/app/shared/models/ICompany';
import { NexusService } from '../nexus.service';
import { IAssessmentIndex } from 'src/app/shared/models/IAssessment';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

/**
 * Component for displaying the details of a specific company and its associated assessments.
 */
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
  providers: [DatePipe]
})
export class DetailComponent implements OnInit, OnDestroy {
  id!: string;
  company: ICompany[] = [];
  assessment: IAssessmentIndex[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  private sub: Subscription | null = null;
  url: string = '';
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
  


  constructor(
    private route: ActivatedRoute,
    public companyService: NexusService,
    private router: Router,
    private datePipe: DatePipe,
    private fb: FormBuilder, 
  ) {
    

    this.nexusForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      typeOfPosition: [0, Validators.required],
      primaryRole: [''],
      workExperience: [0, [Validators.required, Validators.min(0)]],
      skills: [''],
      location: [''],
      acceptApplicantsWhoNeedToRelocate: [false, Validators.required],
      relocationAssistance: [false],
      remotePolicy: [0, Validators.required],
      currency: ['USD', Validators.required],
      annualSalaryMin: [null, [Validators.min(0), Validators.required]],
      annualSalaryMax: [null, [Validators.min(0), Validators.required]],
      equity: [false],
      equityMin: [null, [Validators.min(0),Validators.max(100), Validators.required]],
      equityMax: [null, [Validators.min(0), Validators.max(100), Validators.required]],
    
  
    }, { validator: [this.salaryValidator, this.equityValidator ]});
  }

  /**
   * Lifecycle hook that is called after data-bound properties of a directive are initialized.
   */
  ngOnInit(): void {
    // Subscribe to route parameters
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log('Company ID:', this.id);
    });

    // Initialize DataTable options
    this.dtOptions = {
      pagingType: 'full_numbers'
    };

    // Fetch associated assessments
    this.getAllAssessments();

    this.url = window.location.origin;
  }

  salaryValidator(formGroup: FormGroup) : any{
    const annualSalaryMin = formGroup.get('annualSalaryMin').value;
    const annualSalaryMax = formGroup.get('annualSalaryMax').value;
  
    if (annualSalaryMin > annualSalaryMax) {
      formGroup.get('annualSalaryMax').setErrors({ mismatch: true });
    } else {
      return null;
    }
  }

  equityValidator(formGroup: FormGroup) : any{
    const annualSalaryMin = formGroup.get('equityMin').value;
    const annualSalaryMax = formGroup.get('equityMax').value;
  
    if (annualSalaryMin > annualSalaryMax) {
      formGroup.get('equityMax').setErrors({ equityError: true });
    } else {
      return null;
    }
  }
  

  copyToClipboard(url: string) {
    const inputElement = document.createElement('textarea');
    inputElement.value = url;
    document.body.appendChild(inputElement);
    inputElement.select();
    document.execCommand('copy');
    document.body.removeChild(inputElement);

   
  }

  /**
   * Lifecycle hook that is called when the component is destroyed.
   */
  ngOnDestroy(): void {
    // Unsubscribe from route params to avoid memory leaks
    if (this.sub) {
      this.sub.unsubscribe();
    }

    // Complete the DataTable trigger to avoid memory leaks
    this.dtTrigger.unsubscribe();
  }

  /**
   * Fetch all assessments associated with the company.
   */
  getAllAssessments(): void {
    this.companyService.getAssociatedAssessment(this.id).subscribe({
      next: res => {
        if (res && res.result) {
          this.assessment = res.result.map((assessment: IAssessmentIndex) => {
            assessment.startDate = this.datePipe.transform(assessment.startDate, 'MMMM d, y, h:mm a') || assessment.startDate;
            assessment.endDate = this.datePipe.transform(assessment.endDate, 'MMMM d, y, h:mm a') || assessment.endDate;
            return assessment;
          });
        } else {
          // Handle case where res.result is null or undefined
          console.error('Empty response or result array in response:', res);
          // Optionally show an alert or message to the user
          alert('No assessments found or an error occurred while fetching assessments.');
        }
        this.dtTrigger.next(null);
      },
      error: err => {
        console.error('Error fetching assessments:', err);
        alert('There was an error fetching the assessments. Please try again later.');
      }
    });
  }

  /**
   * Handle the creation of a new Nexus.
   */
  onNewNexusCreate(): void {
    if (this.nexusForm.valid) {
      console.log('Form Value:', this.nexusForm.value);

      // this.companyService.postNexus(this.newNexusForm.value).subscribe({
      //   next: () =>  window.location.reload(),
      //   error: err => {
      //     console.error('Error creating Nexus:', err);
      //     alert('There was an error creating the Nexus. Please try again later.');
      //   }
      // });
    } else {
      // alert('Please fill out all required fields correctly.');
      console.log('Form Value:', this.nexusForm.value);

    }
  }
}
